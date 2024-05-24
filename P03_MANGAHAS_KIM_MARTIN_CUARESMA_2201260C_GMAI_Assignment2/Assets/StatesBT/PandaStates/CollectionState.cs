using Panda;
using UnityEngine;
using UnityEngine.UI;

public class CollectionState : MonoBehaviour
{
    public ShopBotStateManager _stateManager;
    public Button _collectBtn;
     
    private bool itemsCollected = false;

    public void Initialize(ShopBotStateManager stateManager, Button collectBtn)
    {
        _stateManager = stateManager;
        _collectBtn = collectBtn;

        _stateManager.ResetUI();
    }

    [Task]
    public bool IsCollectionState()
    {
        return _stateManager.currentStateName == "CollectionState";
    }

    [Task]
    void CollectionDialogue()
    {
        if (!_stateManager.dialogueText.gameObject.activeSelf)
        {
            _stateManager.dialogueText.text = "I have packed your items. Please collect them.";
            _stateManager.dialogueText.gameObject.SetActive(true);
        }
        Task.current.Succeed();
    }

    [Task]
    void WaitForCollectionClick()
    {
        _collectBtn.onClick.AddListener(OnClick);
        _collectBtn.gameObject.SetActive(true);

        if (itemsCollected)
        {
            itemsCollected = true;
            Task.current.Succeed();
        }
    }

    void OnClick()
    {
        itemsCollected = true;
    }

    [Task]
    void SwitchToRating()
    {
        if (itemsCollected)
        {
            itemsCollected = false;
            _stateManager.SetCurrentState("FeedbackState");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
}
