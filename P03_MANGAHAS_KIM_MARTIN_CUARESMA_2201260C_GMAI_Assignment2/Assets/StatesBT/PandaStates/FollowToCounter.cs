using System.Collections;
using Panda;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FollowToCounter : MonoBehaviour
{
    public ShopBotStateManager _stateManager;
    public Button _followBtn;

    public GameObject _counter;
    public GameObject _shopBot;
    public NavMeshAgent _shopBotAgent;

    private bool followClicked = false;
    private bool reachCounter = false;

    public void Initialize(ShopBotStateManager stateManager, Button followBtn, GameObject counter, GameObject shopBot, NavMeshAgent shopBotAgent)
    {
        _stateManager = stateManager;
        _followBtn = followBtn;
        _counter = counter;
        _shopBot = shopBot;
        _shopBotAgent = shopBotAgent;

        _stateManager.ResetUI(); 
    }

    [Task]
    public bool IsFollowState()
    {
        return _stateManager.currentStateName == "FollowToCounter";

        //Follows same logic as previous states (IdleState, etc.)
    }

    [Task]
    void FollowDialogue()
    {
        _stateManager.dialogueText.text = "Thank you for confirming. Now, follow me to the counter for payment.";
        _stateManager.dialogueText.gameObject.SetActive(true);

        Task.current.Succeed();

        //Follows same logic as previous states (IdleState, etc.)
    }

    [Task]
    void WaitForFollowClick()
    {
        _followBtn.onClick.AddListener(FollowClick);
        _followBtn.gameObject.SetActive(true);

        if (followClicked)
        {
            _followBtn.onClick.RemoveListener(FollowClick);
            followClicked = true;
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

        //Follows same logic as previous states (IdleState, etc.)
    }

    void FollowClick()
    {
        followClicked = true;
        _followBtn.gameObject.SetActive(false); 
    }

    [Task]
    void WalkToCounter()
    {
        if (followClicked)
        {
            if (!_shopBotAgent.hasPath || _shopBotAgent.remainingDistance < _shopBotAgent.stoppingDistance)
            {
                _shopBotAgent.SetDestination(_counter.transform.position); 
            }
            if (_shopBotAgent.remainingDistance <= _shopBotAgent.stoppingDistance && !_shopBotAgent.pathPending)
            {
                _shopBotAgent.isStopped = true; 
                reachCounter = true; 
                Task.current.Succeed();
            }
            else
            {
                Task.current.Fail();
            }
        }

        //Follows same logic as previous states (RetrieveState, etc.)
    }

    [Task]
    void SwitchToPayment()
    {
        if (reachCounter)
        {
            reachCounter = false; 
            followClicked = false; 

            _stateManager.SetCurrentState("Payment");

            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

        //Follows same logic as previous states (IdleState, etc.)
    }
}
