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

    public NavMeshAgent _shopBot;
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

    public void Initialize(ShopBotStateManager stateManager, Button plasticBagBtn, Button ownBagBtn, NavMeshAgent shopBot, GameObject counter, GameObject plasticBagHolder, GameObject player )
    {
        _stateManager = stateManager;
        _plasticBagBtn = plasticBagBtn;
        _ownBagBtn = ownBagBtn; 
         
        _counter = counter; 
        _player = player; 
        _shopBot = shopBot; 
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
            _shopBot.SetDestination(_plasticBagHolder.transform.position);
            isWalking = true;
        }
        else if (_shopBot.remainingDistance <= pBagHolderReachDistance)
        {
            isWalking = false;
            reachBagHolder = true;

            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void WalkToOBagHolder()
    {
        if (!isWalking && !reachBagHolder && ownBagClicked)
        {
            _shopBot.SetDestination(_plasticBagHolder.transform.position);
            isWalking = true;
        }
        else if (_shopBot.remainingDistance <= playerReachDistance)
        {
            isWalking = false;
            reachBagHolder = true;

            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void WalkBackToCounter()
    {
        if (reachBagHolder)
        {
            _shopBot.SetDestination(_counter.transform.position); 
            isWalking = true;

            if (isWalking && reachBagHolder && _shopBot.remainingDistance < counterReachDistance)
            {
                if (_shopBot.velocity.magnitude < 0.01f)
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
            }
        }
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
