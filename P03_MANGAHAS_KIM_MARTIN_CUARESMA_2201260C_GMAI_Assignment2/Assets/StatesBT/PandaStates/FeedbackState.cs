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
    }

    [Task]
    void FeedbackDialogue()
    {
        _stateManager.dialogueText.text = "Please provide feedback.";
        _stateManager.dialogueText.gameObject.SetActive(true);

        Task.current.Succeed();
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
    } 
     
    void Positive()
    { 
        _positiveBtn.onClick.RemoveAllListeners();
        positiveClick = true;
        positiveCount++;  
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
    }
}
