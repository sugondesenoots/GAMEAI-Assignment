using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotConfirm : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;

    private bool confirmClick = false;
    private bool backClick = false;

    public ShopBotConfirm()
    {
        stateName = "CONFIRM";
        stateDescription = "I have collected your selected items. Please confirm items.";
    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.ConfirmState)
        {
            Debug.Log($"{stateName}: {stateDescription}");

            ShopBot.ResetButtons();
            ShopBot.confirmButton.gameObject.SetActive(true);
            ShopBot.backButton.gameObject.SetActive(true);
        }

        ShopBot.backButton.onClick.RemoveAllListeners();
        ShopBot.backButton.onClick.AddListener(Back);
        ShopBot.confirmButton.onClick.AddListener(Confirm);
    }
    void Confirm()
    {
        confirmClick = true;
    }
    void Back()
    {
        backClick = true;
    }

    public override void UpdateState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (confirmClick == true)
        {
            ShopBot.SwitchState(ShopBot.FollowState);
            confirmClick = false;
        }
        else if (backClick == true)
        {
            Debug.Log("Items collected are not correct, going back for collection.");
            ShopBot.SwitchState(ShopBot.RetrieveState);
            backClick = false;
        }
    }
}