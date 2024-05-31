using Panda;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FeedbackState : MonoBehaviour
{
    public ShopBotStateManager _stateManager; 

    public Button _positiveBtn;
    public Button _negativeBtn;

    [SerializeField] private int positiveCount = 0;
    [SerializeField] private int negativeCount = 0;

    private bool positiveClick= false;
    private bool negativeClick = false;

    public void Initialize(ShopBotStateManager stateManager, Button positiveButton, Button negativeButton)
    {
        _stateManager = stateManager; 

        _positiveBtn = positiveButton;
        _negativeBtn = negativeButton;

        _stateManager.ResetUI();
    }

    [Task]
    public bool IsFeedbackState()
    {
        return _stateManager.currentStateName == "FeedbackState";

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
    void Feedback()
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
    void SwitchToIdle()
    { 
        if(positiveClick || negativeClick)
        {
            positiveClick = false; 
            negativeClick = false;

            _stateManager.SetCurrentState("IdleState");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

        //Follows same logic as previous states (PaymentState, etc.)
    }
}
