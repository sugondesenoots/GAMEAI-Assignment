using Panda;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PackingState : MonoBehaviour
{
    public ShopBotStateManager _stateManager;
    public Button _plasticBagBtn;
    public Button _ownBagBtn;

    private bool plasticBagClicked = false;
    private bool ownBagClicked = false;

    public void Initialize(ShopBotStateManager stateManager, Button plasticBagBtn, Button ownBagBtn)
    {
        _stateManager = stateManager;
        _plasticBagBtn = plasticBagBtn;
        _ownBagBtn = ownBagBtn;

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
        _plasticBagBtn.onClick.AddListener(OwnBagClick);
        _plasticBagBtn.gameObject.SetActive(true);

        if (plasticBagClicked)
        { 
            plasticBagClicked = true;
            _plasticBagBtn.onClick.RemoveListener(OwnBagClick); 

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
    void SwitchToCollection()
    {
        if (plasticBagClicked)
        {
            plasticBagClicked = false;
            _stateManager.SetCurrentState("CollectionState");
            Task.current.Succeed();
        }
        else if (ownBagClicked)
        {
            ownBagClicked = false;
            _stateManager.SetCurrentState("CollectionState");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
}
