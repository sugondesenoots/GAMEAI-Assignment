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
    public NavMeshAgent _agent;

    private bool backClicked = false;

    private bool isWalking = false;
    private bool reachShelf = false;


    [SerializeField] private float playerReachDistance = 10f;
    [SerializeField] private float shelfReachDistance = 10f;

    public void Initialize(ShopBotStateManager stateManager, Button backBtn, GameObject shelf, GameObject shopBot, GameObject player, NavMeshAgent agent)
    {
        _stateManager = stateManager;
        _backBtn = backBtn; 
        _shelf = shelf; 
        _shopBot = shopBot; 
        _player = player;
        _agent = agent;

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
        NavMeshAgent agent = _shopBot.GetComponent<NavMeshAgent>();

        if (!isWalking && !reachShelf)
        {
            agent.SetDestination(_shelf.transform.position);
            isWalking = true;
        }
        else if (agent.remainingDistance < shelfReachDistance)
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
        NavMeshAgent agent = _shopBot.GetComponent<NavMeshAgent>();

        if (!isWalking && reachShelf)
        {
            _stateManager.dialogueText.text = "Bringing retrieved items to customer...";
            agent.SetDestination(_player.transform.position);
            isWalking = true;
        }

        if (isWalking && reachShelf && agent.remainingDistance < playerReachDistance)
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
        NavMeshAgent agent = _shopBot.GetComponent<NavMeshAgent>();

        _backBtn.onClick.AddListener(BackClick);
        _backBtn.gameObject.SetActive(true);

        if (backClicked)
        {
            _backBtn.onClick.RemoveListener(BackClick);
            backClicked = true;

            if (agent != null)
            {
                agent.destination = _shopBot.transform.position;

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
