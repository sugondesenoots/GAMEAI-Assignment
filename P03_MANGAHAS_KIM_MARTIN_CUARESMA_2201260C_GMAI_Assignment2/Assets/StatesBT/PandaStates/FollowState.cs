using System.Collections;
using Panda;
using UnityEngine;
using UnityEngine.UI;

public class FollowState : MonoBehaviour
{
    public ShopBotStateManager _stateManager;
    public Button _followBtn;

    public GameObject _counter;
    public GameObject _shopBot;

    private bool followClicked = false;
    private bool reachCounter = false;

    [SerializeField] private float walkSpeed = 2f;

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
    }

    [Task]
    void WalkToCounter()
    {
        if (followClicked == true)
        {
            //Calculate direction towards the counter
            Vector3 counterPos = _counter.transform.position;
            Vector3 direction = counterPos - _shopBot.transform.position;

            //Move the ShopBot towards the counter
            _shopBot.transform.position += direction.normalized * walkSpeed * Time.deltaTime;

            //Check if the ShopBot has reached the counter
            float distanceToCounter = direction.magnitude;

            if (distanceToCounter < 3f)
            {
                reachCounter = true; 

                Task.current.Succeed();
            }
            else
            {
                Task.current.Fail();
            }
        }
        else if (reachCounter)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    void SwitchToPayment()
    {
        if (reachCounter == true)
        {
            _stateManager.SetCurrentState("PaymentState");

            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
}
