using System.Collections;
using Panda;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RetrieveState : MonoBehaviour
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
        return _stateManager.currentStateName == "RetrieveState";
    }

    [Task]
    void RetrieveDialogue()
    {
        _stateManager.dialogueText.text = "Please move away, I will retrieve your items.";
        _stateManager.dialogueText.gameObject.SetActive(true);

        Task.current.Succeed();
    }

    [Task]
    void WalkToShelf()
    {
        if (!isWalking && !reachShelf) //Checks if the shop bot's agent is not moving towards something already
        {
            _shopBotAgent.SetDestination(_shelf.transform.position); //Sets the path towards shelf position
            isWalking = true;
        }
        else if (_shopBotAgent.remainingDistance <= shelfReachDistance) //Checks if bot has reached the shelf
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
        if (!isWalking && reachShelf) //If it has already reached the shelf and is not pathing/moving towards something, set to customer position
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
            _stateManager.SetCurrentState("ConfirmState");
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

                isWalking = false;
                reachShelf = false;
            } 

            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
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
            _stateManager.SetCurrentState("CartState");

            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
}
