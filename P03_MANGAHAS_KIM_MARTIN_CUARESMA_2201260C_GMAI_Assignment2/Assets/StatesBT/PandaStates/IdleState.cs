using Panda;
using UnityEngine;
using UnityEngine.UI;

public class IdleState : MonoBehaviour
{
    public ShopBotStateManager _stateManager;
    public Button _interactBtn;

    private bool interactClicked = false;

    public void Initialize(ShopBotStateManager stateManager, Button interactBtn)
    {
        _stateManager = stateManager;
        _interactBtn = interactBtn;

        _stateManager.ResetUI(); 
    }

    [Task]
    public bool IsIdleState()
    {
        return _stateManager.currentStateName == "IdleState";
    }

    [Task]
    void IdleDialogue()
    {
        if (!_stateManager.dialogueText.gameObject.activeSelf)
        {
            _stateManager.dialogueText.text = "Press 'Interact' to start shopping!";
            _stateManager.dialogueText.gameObject.SetActive(true);
        }
        Task.current.Succeed();
    }

    [Task]
    void WaitForInteractClick()
    {
        _interactBtn.onClick.AddListener(OnClick);
        _interactBtn.gameObject.SetActive(true);

        if (interactClicked)
        {
            interactClicked = true;
            Task.current.Succeed();
        }
    }

    void OnClick()
    {
        interactClicked = true;
    }

    [Task]
    void SwitchToShoplist()
    {
        if (interactClicked)
        {
            interactClicked = false;
            _stateManager.SetCurrentState("ShoplistState");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
}
