using Panda;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEngine.AI;

public class Feedback : MonoBehaviour
{
    public ShopBotStateManager _stateManager; 

    public Button _positiveBtn;
    public Button _negativeBtn;

    public NavMeshAgent _shopBotAgent;
    public GameObject _botStand;

    [SerializeField] private int positiveCount = 0;
    [SerializeField] private int negativeCount = 0;

    private bool positiveClick= false;
    private bool negativeClick = false; 
    private bool reachedBotStand = false;

    public void Initialize(ShopBotStateManager stateManager, Button positiveButton, Button negativeButton, GameObject botStand, NavMeshAgent shopBotAgent)
    {
        _stateManager = stateManager; 

        _positiveBtn = positiveButton;
        _negativeBtn = negativeButton; 
         
        _botStand = botStand; 
        _shopBotAgent = shopBotAgent;

        _stateManager.ResetUI();
    }

    [Task]
    public bool IsFeedbackState()
    {
        return _stateManager.currentStateName == "Feedback";

        //Follows same logic as previous states (IdleState, etc.)
    }

    [Task]
    void FeedbackDialogue()
    {
        _stateManager.dialogueText.text = "Please provide feedback.";
        _stateManager.dialogueText.gameObject.SetActive(true);

        Task.current.Succeed();

        //Follows same logic as previous states (IdleState, etc.)
    }

    [Task]
    void GetFeedback()
    {
        _positiveBtn.onClick.RemoveAllListeners();
        _negativeBtn.onClick.RemoveAllListeners();

        _positiveBtn.onClick.AddListener(Positive);
        _negativeBtn.onClick.AddListener(Negative);

        _positiveBtn.gameObject.SetActive(true);  
        _negativeBtn.gameObject.SetActive(true);
         
        if(positiveClick || negativeClick)
        {
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

        //Follows same logic as previous states (PaymentState, etc.)
    }

    void Positive()
    { 
        _positiveBtn.onClick.RemoveAllListeners();
        positiveClick = true;
        positiveCount++;   
         
        //Follows same logic except that it increases the count of variable positiveCount 
        //This is so that the feedback given by the customer/player is kept track by the shop bot 
        //Same logic is followed by the 'Negative' function below
    } 
     
    void Negative()
    {  
        _negativeBtn.onClick.RemoveAllListeners();
        negativeClick = true;
        negativeCount++;
    }

    [Task]
    void BackToBotStand() //After completing feedback, shop bot goes back to its bot stand
    {
        if (positiveClick || negativeClick)
        {
            _shopBotAgent.SetDestination(_botStand.transform.position);

            if (_shopBotAgent.velocity.magnitude < 0.01f)
            {
                reachedBotStand = true;

                Task.current.Succeed();
            }
            else
            {
                Task.current.Fail();
            }
        }

        //Follows same logic as previous states (PackingState, etc.)
    }

    [Task]
    void SwitchToIdle()
    {
        _stateManager.ResetUI(); //Prevent any interaction until it is back to idle

        if (reachedBotStand)
        { 
            reachedBotStand = false;
            positiveClick = false; 
            negativeClick = false;

            _stateManager.SetCurrentState("Idle");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

        //Follows same logic as previous states (PaymentState, etc.)
    }
}
