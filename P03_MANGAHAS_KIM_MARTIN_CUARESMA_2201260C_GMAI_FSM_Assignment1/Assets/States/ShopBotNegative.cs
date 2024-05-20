using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotNegative : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;
    private bool feedbackClick = false;

    public ShopBotNegative()
    {
        stateName = "NEGATIVE";
        stateDescription = "We are sorry to hear that! Could you provide more details as to why you did not enjoy it?";
    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.NegativeState)
        {
            Debug.Log($"{stateName}: {stateDescription}");

            ShopBot.ResetButtons();
            ShopBot.feedbackButton.gameObject.SetActive(true);
        }

        ShopBot.feedbackButton.onClick.AddListener(Feedback);
    }
     
    void Feedback()
    {
        feedbackClick = true;
    }
    public override void UpdateState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (feedbackClick == true)
        {
            ShopBot.SwitchState(ShopBot.FeedbackState);
        }
    }
}