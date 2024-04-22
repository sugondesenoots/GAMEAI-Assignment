using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotFollow : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;

    private bool followClick = false;
    private bool backClick = false;

    public ShopBotFollow()
    {
        stateName = "FOLLOW";
        stateDescription = "Items have been confirmed. Please follow me for payment.";
    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.FollowState)
        {
            Debug.Log($"{stateName}: {stateDescription}");

            ShopBot.ResetButtons();
            ShopBot.followButton.gameObject.SetActive(true);
            ShopBot.backButton.gameObject.SetActive(true);
        }

        ShopBot.backButton.onClick.RemoveAllListeners();
        ShopBot.backButton.onClick.AddListener(Back);
        ShopBot.followButton.onClick.AddListener(Follow);
    }
    void Follow()
    {
        followClick = true;
    }

    void Back()
    {
        backClick = true;
    }

    public override void UpdateState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (followClick == true)
        {
            ShopBot.SwitchState(ShopBot.PaymentState);
            followClick = false;
        }
        else if (backClick == true)
        {
            Debug.Log("Cancelled. Re-confirming items.");
            ShopBot.SwitchState(ShopBot.CollectionState);
            backClick = false;
        }
    }
}