using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotFeedback : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;
    private float elapsedTime = 0f;

    public ShopBotFeedback()
    {
        stateName = "FEEDBACK";
        stateDescription = "We appreciate your feedback. Thank you for shopping! *Goes back to Idle State*";
    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.PackingState)
        {
            Debug.Log($"{stateName}: {stateDescription}");

            ShopBot.ResetButtons();
        }
    }

    public override void UpdateState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 5.0f)
        {
            //Change to follow state after 5 seconds
            ShopBot.SwitchState(ShopBot.IdleState);

            //Reset timer
            elapsedTime = 0f;
        }
    }
}