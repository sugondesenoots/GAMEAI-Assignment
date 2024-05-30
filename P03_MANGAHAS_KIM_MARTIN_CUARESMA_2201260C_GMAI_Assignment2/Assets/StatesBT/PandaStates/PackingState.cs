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
    private bool reachedBagHolder = false;
    private bool reachedCounter = false;

    [SerializeField] private float playerReachDistance = 3f;
    [SerializeField] private float plasticBagHolderReachDistance = 3f;
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
    void WalkToPlasticBagHolder()
    {
        if (!isWalking && !reachedBagHolder && plasticBagClicked)
        {
            Vector3 target = _plasticBagHolder.transform.position;
            _shopBot.SetDestination(target);

            isWalking = true;
        } 

        if (isWalking && _shopBot.remainingDistance <= plasticBagHolderReachDistance)
        {
            isWalking = false;
            reachedBagHolder = true;

            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void WalkToOwnBagHolder()
    {
        if (!isWalking && !reachedBagHolder && ownBagClicked)
        {
            Vector3 target = _player.transform.position;
            _shopBot.SetDestination(target);

            isWalking = true;
        }

        if (isWalking && _shopBot.remainingDistance <= playerReachDistance)
        {
            isWalking = false;
            reachedBagHolder = true;

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
        if (reachedBagHolder)
        {
            _shopBot.SetDestination(_counter.transform.position);
            isWalking = true; 
        }

        if (isWalking && _shopBot.remainingDistance <= counterReachDistance)
        {
            isWalking = false;
            reachedBagHolder = false;
            reachedCounter = true;

            Task.current.Succeed();
        }
        else 
        {
            Task.current.Fail();
        }
    }

    [Task]
    void SwitchToCollection()
    {  
        if (reachedCounter)
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
