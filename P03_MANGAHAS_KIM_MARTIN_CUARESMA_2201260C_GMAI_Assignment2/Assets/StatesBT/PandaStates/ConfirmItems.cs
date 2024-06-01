using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Panda;

public class ConfirmItems : MonoBehaviour
{
    public ShopBotStateManager _stateManager;
    private PandaBehaviour _bt;

    public Button _confirmBtn;
    public Button _backBtn;

    private bool confirmClicked = false;
    private bool backClicked = false;

    public void Initialize(ShopBotStateManager stateManager, ShopCart shopCart, PandaBehaviour bt, Button confirmBtn, Button backBtn)
    {
        _stateManager = stateManager;
        _bt = bt;

        _confirmBtn = confirmBtn;
        _backBtn = backBtn;

        _stateManager.ResetUI();
    } 

    [Task]
    public bool IsConfirmState()  
    {   
        return _stateManager.currentStateName == "ConfirmItems";

        //Follows same logic as previous states (IdleState, etc.)
    }

    [Task]
    void WaitForConfirmClick() //ConfirmState is basically just used to double-check items, follows same button logic as previous states
    {
        _stateManager.dialogueText.text = "I have retrieved your items. Please confirm them.";
        _confirmBtn.onClick.AddListener(ConfirmClick);
        _confirmBtn.gameObject.SetActive(true);

        if (confirmClicked)
        {
            confirmClicked = true;
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    void ConfirmClick()
    {
        confirmClicked = true;
    }

    [Task]
    void SwitchToFollow()
    {
        if (confirmClicked)
        {
            confirmClicked = false;
            _stateManager.SetCurrentState("FollowToCounter");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

        //Follows same logic as previous states (IdleState, etc.)
    }

    [Task]
    void WaitForBackClickCS()
    {
        _backBtn.onClick.AddListener(BackClick);
        _backBtn.gameObject.SetActive(true);

        if (backClicked)
        {
            _backBtn.onClick.RemoveListener(BackClick);
            backClicked = true;
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

        //Follows same logic as previous states (IdleState, etc.)
    }


    void BackClick()
    {
        backClicked = true;
    }

    [Task]
    void BackToRetrieve()
    {
        if (backClicked)
        {
            backClicked = false;
            _stateManager.SetCurrentState("RetrieveItems");

            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

        //Follows same logic as previous states (IdleState, etc.)
    }
}
