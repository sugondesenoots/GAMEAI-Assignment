using Panda;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackState : MonoBehaviour
{
    public ShopBotStateManager _stateManager;
    public Button _feedbackBtn;

    private bool feedbackDone = false;

    public void Initialize(ShopBotStateManager stateManager, Button feedbackBtn)
    {
        _stateManager = stateManager;
        _feedbackBtn = feedbackBtn;

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
        if (!_stateManager.dialogueText.gameObject.activeSelf)
        {
            _stateManager.dialogueText.text = "Before you leave, could you provide some feedback on your experience?";
            _stateManager.dialogueText.gameObject.SetActive(true);
        }
        Task.current.Succeed();
    }

    [Task]
    void WaitForFeedbackInput()
    {
        _feedbackBtn.onClick.AddListener(OnClick);
        _feedbackBtn.gameObject.SetActive(true);

        if (feedbackDone)
        {
            feedbackDone = true;
            Task.current.Succeed();
        }
    }

    void OnClick()
    {
        feedbackDone = true;
    }

    [Task]
    void SwitchToIdle()
    {
        if (feedbackDone)
        {
            feedbackDone = false;
            _stateManager.SetCurrentState("IdleState");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
}
