using Panda;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEngine.AI;

public class PackingState : MonoBehaviour
{
    public ShopBotStateManager _stateManager;
    public Button _plasticBagBtn;
    public Button _ownBagBtn; 

    public NavMeshAgent _shopBotAgent;
    public GameObject _counter;
    public GameObject _plasticBagHolder;
    public GameObject _player;

    private bool plasticBagClicked = false;
    private bool ownBagClicked = false; 

    private bool isWalking = false;
    private bool reachBagHolder = false;
    private bool reachCounter = false;

    [SerializeField] private float playerReachDistance = 3f;
    [SerializeField] private float pBagHolderReachDistance = 3f;
    [SerializeField] private float counterReachDistance = 3f;

    public void Initialize(ShopBotStateManager stateManager, Button plasticBagBtn, Button ownBagBtn, NavMeshAgent shopBotAgent, GameObject counter, GameObject plasticBagHolder, GameObject player )
    {
        _stateManager = stateManager;
        _plasticBagBtn = plasticBagBtn;
        _ownBagBtn = ownBagBtn; 
         
        _counter = counter; 
        _player = player; 
        _shopBotAgent = shopBotAgent; 
        _plasticBagHolder = plasticBagHolder;

        _stateManager.ResetUI();
    }

    [Task]
    public bool IsPackingState()
    {
        return _stateManager.currentStateName == "PackingState";
    }

    [Task]
    void PackingDialogue()
    {
        if (!_stateManager.dialogueText.gameObject.activeSelf)
        {
            _stateManager.dialogueText.text = "Please choose your bag type.";
            _stateManager.dialogueText.gameObject.SetActive(true);
        }
        Task.current.Succeed();
    }

    [Task]
    void WaitForPlasticBagClick()
    {
        _plasticBagBtn.onClick.AddListener(PlasticBagClick);
        _plasticBagBtn.gameObject.SetActive(true);

        if (plasticBagClicked)
        { 
            plasticBagClicked = true;
            _plasticBagBtn.onClick.RemoveListener(PlasticBagClick); 

            _stateManager.dialogueText.text = "Packing your items into the plastic bag...";
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

        //Follows same logic as previous states (IdleState, etc.)
    }

    void PlasticBagClick()
    {
        plasticBagClicked = true;
    }

    [Task]
    void WaitForOwnBagClick()
    {
        _ownBagBtn.onClick.AddListener(OwnBagClick);
        _ownBagBtn.gameObject.SetActive(true);

        if (ownBagClicked)
        {
            ownBagClicked = true;
            _ownBagBtn.onClick.RemoveListener(OwnBagClick); 

            _stateManager.dialogueText.text = "Packing your items into your bag...";
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

        //Follows same logic as previous states (IdleState, etc.)
    }

    void OwnBagClick()
    {
        ownBagClicked = true;
    }

    [Task]
    void WalkToPBagHolder()
    {
        if (!isWalking && !reachBagHolder && plasticBagClicked)
        {
            _shopBotAgent.SetDestination(_plasticBagHolder.transform.position);
            isWalking = true;
        }
        else if (_shopBotAgent.remainingDistance <= pBagHolderReachDistance)
        {
            isWalking = false;
            reachBagHolder = true;

            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

        //Follows same logic as previous states (RetrieveState, FollowState, etc.)
    }

    [Task]
    void WalkToOBagHolder()
    {
        if (!isWalking && !reachBagHolder && ownBagClicked)
        {
            _shopBotAgent.SetDestination(_player.transform.position);
            isWalking = true;
        }
        else if (_shopBotAgent.remainingDistance <= playerReachDistance)
        {
            isWalking = false;
            reachBagHolder = true;

            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        } 
         
        //Follows same logic as previous states (RetrieveState, FollowState, etc.)
    }

    [Task]
    void WalkBackToCounter()
    {
        if (reachBagHolder)
        {
            _shopBotAgent.SetDestination(_counter.transform.position); 
            isWalking = true;

            if (isWalking && reachBagHolder && _shopBotAgent.remainingDistance < counterReachDistance)
            {
                if (_shopBotAgent.velocity.magnitude < 0.01f) //Ensures that completes the task after the bot has fully stopped/completed pathing
                {
                    isWalking = false;
                    reachBagHolder = false;
                    reachCounter = true;

                    Task.current.Succeed();
                }
                else
                {
                    Task.current.Fail();
                } 
                 
                //Added condition due to issue where the 'Groceries' object would spawn in too early (Which is a method in the CollectionState)
                //When the shop bot's agent has only just reached the plastic bag holder, it spawns 
                //It would make more sense that it would finish packing after it has gotten the plastic bag from the holder...
                //Reaches the counter and puts the items onto the counter
            }
        }

        //Follows same logic as previous states (RetrieveState, FollowState, etc.)
    }

    [Task]
    void SwitchToCollection()
    {  
        if (reachCounter)
        {
            _stateManager.SetCurrentState("CollectionState");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
}
