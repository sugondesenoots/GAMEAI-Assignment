using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShopBotIdle : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;
    public ShopBotShowShopList shopBotShowShopList;

    public ShopBotIdle()
    {
        stateName = "IDLE";
        stateDescription = "Interact to Start.";
    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.IdleState)
        {
            Debug.Log($"{stateName}: {stateDescription}");
            ShopBot.ResetButtons();
            ShopBot.interactButton.gameObject.SetActive(true);
        }

        ShopBot.interactButton.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        shopBotStateManager.SwitchState(shopBotStateManager.GreetingState);
    }

    public override void UpdateState(ShopBotStateManager ShopBot)
    {

    }
}