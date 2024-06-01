using System.Collections;
using Panda;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RetrieveItems : MonoBehaviour
{
    public ShopBotStateManager _stateManager;
    public Button _backBtn;  

    public GameObject _shelf;
    public GameObject _shopBot;
    public GameObject _player;
    public NavMeshAgent _shopBotAgent;

    private bool backClicked = false;

    private bool isWalking = false;
    private bool reachShelf = false;


    [SerializeField] private float playerReachDistance = 3f;
    [SerializeField] private float shelfReachDistance = 3f; 
     
    //These float values control how far the bot needs to be away from the object to succeed in the task below  

    public void Initialize(ShopBotStateManager stateManager, Button backBtn, GameObject shelf, GameObject shopBot, GameObject player, NavMeshAgent shopBotAgent)
    {
        _stateManager = stateManager;
        _backBtn = backBtn; 
        _shelf = shelf; 
        _shopBot = shopBot; 
        _player = player;
        _shopBotAgent = shopBotAgent;

        _stateManager.ResetUI();
    }

    [Task]
    public bool IsRetrieveState()
    {
        return _stateManager.currentStateName == "RetrieveItems";

        //Follows same logic as previous states (IdleState, etc.)
    }

    [Task]
    void RetrieveDialogue()
    {
        _stateManager.dialogueText.text = "Please move away, I will retrieve your items.";
        _stateManager.dialogueText.gameObject.SetActive(true);

        Task.current.Succeed();

        //Follows same logic as previous states (IdleState, etc.)
    }

    [Task]
    void WalkToShelf()
    {
        if (!isWalking && !reachShelf) //Checks if the shop bot's agent is not moving towards something already
        {
            _shopBotAgent.SetDestination(_shelf.transform.position); //Sets the path towards shelf position
            isWalking = true;
        }
        else if (_shopBotAgent.remainingDistance <= shelfReachDistance) //Checks if shop bot has reached the shelf
        {
            isWalking = false;
            reachShelf = true; 

            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void WalkToPlayerAndConfirm()
    {
        if (!isWalking && reachShelf) //If it has already reached the shelf & is not pathing/moving towards something, set to customer position
        {
            _stateManager.dialogueText.text = "Bringing retrieved items to customer...";
            _shopBotAgent.SetDestination(_player.transform.position);
            isWalking = true;
        }

        if (isWalking && reachShelf && _shopBotAgent.remainingDistance <= playerReachDistance) //Checks if it has reached customer/player, afterwards sets it to ConfirmState
        {
            _stateManager.dialogueText.text = "I have retrieved your items. Please confirm them.";
            isWalking = false;
            reachShelf = false;
            _stateManager.SetCurrentState("ConfirmItems");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }


    [Task]
    void WaitForBackClickRS()
    {
        _backBtn.onClick.AddListener(BackClick);
        _backBtn.gameObject.SetActive(true);

        if (backClicked)
        {
            _backBtn.onClick.RemoveListener(BackClick);
            backClicked = true;

            if (_shopBotAgent != null)
            {
                _shopBotAgent.destination = _shopBot.transform.position;  
                 
                //If back click is pressed while the shop bot agent is moving/retrieving items... 
                //It will simply stop on the spot and call the BackToCart function

                isWalking = false;
                reachShelf = false;  

                //Resets bools for next time
            } 

            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

        //Other than interrupting the shop bot's movements...
        //It follows same logic as previous states (IdleState, etc.)
    }

    void BackClick()
    {
        backClicked = true;
    }

    [Task]
    void BackToCart()
    {
        if (backClicked)
        {
            backClicked = false;

            Debug.Log("Cancelling items retrieving.");
            _stateManager.SetCurrentState("ShowCart");

            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

        //Follows same logic as previous states (IdleState, etc.)
    }
}
