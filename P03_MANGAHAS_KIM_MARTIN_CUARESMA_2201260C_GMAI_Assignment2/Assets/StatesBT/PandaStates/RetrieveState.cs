using System.Collections;
using Panda;
using UnityEngine;
using UnityEngine.UI;

public class RetrieveState : MonoBehaviour
{
    public ShopBotStateManager _stateManager;
    public Button _backBtn;  

    public GameObject _shelf;
    public GameObject _shopBot;
    public GameObject _player;

    private bool backClicked = false;
    private bool isWalking = false; 
    private bool reachShelf = false;

    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float playerVicinityDistance = 2f;

    public void Initialize(ShopBotStateManager stateManager, Button backBtn, GameObject shelf, GameObject shopBot, GameObject player)
    {
        _stateManager = stateManager;
        _backBtn = backBtn; 
        _shelf = shelf; 
        _shopBot = shopBot; 
        _player = player;

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
        if (!isWalking & !reachShelf)
        {
            //Calculate direction towards the shelf
            Vector3 shelfPos = _shelf.transform.position;
            Vector3 direction = shelfPos - _shopBot.transform.position;

            //Move the ShopBot towards the shelf
            _shopBot.transform.position += direction.normalized * walkSpeed * Time.deltaTime;

            //Check if the ShopBot has reached the shelf
            float distanceToShelf = direction.magnitude; 

            if (distanceToShelf < 2f)
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
        else if (reachShelf)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    void WalkToPlayerAndConfirm()
    {
        if (!isWalking & reachShelf == true)
        {
            _stateManager.dialogueText.text = "I have retrieved your items. Please confirm them.";

            //Calculate direction towards the player
            Vector3 playerPos = _player.transform.position;
            Vector3 direction = playerPos - _shopBot.transform.position;

            //Move the ShopBot towards the player
            _shopBot.transform.position += direction.normalized * walkSpeed * Time.deltaTime;

            //Check if the ShopBot has reached the player's vicinity
            float distanceToPlayer = direction.magnitude; 

            if (distanceToPlayer < playerVicinityDistance && reachShelf == true)
            {
                //Stop walking and go to ConfirmState
                isWalking = false;
                reachShelf = false;
                _stateManager.SetCurrentState("ConfirmState");
                Task.current.Succeed();
            }
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
