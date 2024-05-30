using System.Collections;
using Panda;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FollowState : MonoBehaviour
{
    public ShopBotStateManager _stateManager;
    public Button _followBtn;

    public GameObject _counter;
    public GameObject _shopBot;

    private bool followClicked = false;
    private bool reachCounter = false;

    public void Initialize(ShopBotStateManager stateManager, Button followBtn, GameObject counter, GameObject shopBot)
    {
        _stateManager = stateManager;
        _followBtn = followBtn;
        _counter = counter; 

        _shopBot = shopBot;

        _stateManager.ResetUI();
    }

    [Task]
    public bool IsFollowState()
    {
        return _stateManager.currentStateName == "FollowState";
    }

    [Task]
    void FollowDialogue()
    {
        _stateManager.dialogueText.text = "Thank you for confirming. Now, follow me to the counter for payment.";
        _stateManager.dialogueText.gameObject.SetActive(true);

        Task.current.Succeed();
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
            NavMeshAgent agent = _shopBot.GetComponent<NavMeshAgent>();

            if (!agent.hasPath || agent.remainingDistance < agent.stoppingDistance)
            {
                agent.SetDestination(_counter.transform.position);
            }
            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                agent.isStopped = true;
                reachCounter = true;

                Task.current.Succeed();
            }
            else
            {
                Task.current.Fail();
            }
        }
    }

    [Task]
    void SwitchToPayment()
    {
        if (reachCounter)
        { 
            reachCounter = false;
            _stateManager.SetCurrentState("PaymentState");

            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
}
