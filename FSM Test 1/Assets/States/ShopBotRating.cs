using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotRating : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;

    private bool positiveClick = false; 
    private bool negativeClick = false;

    public ShopBotRating()
    {
        stateName = "RATING";
        stateDescription = "Please rate your experience.";
    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.PackingState)
        {
            Debug.Log($"{stateName}: {stateDescription}");

            ShopBot.ResetButtons();
            ShopBot.positiveButton.gameObject.SetActive(true);
            ShopBot.negativeButton.gameObject.SetActive(true);
        }

        ShopBot.positiveButton.onClick.AddListener(Positive);
        ShopBot.negativeButton.onClick.AddListener(Negative);
    }
    void Positive()
    {
        positiveClick = true;
    }
    void Negative()
    {
        negativeClick = true;
    }
    public override void UpdateState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (positiveClick == true)
        {
            ShopBot.SwitchState(ShopBot.PositiveState);
            positiveClick = false;
        }
        else if (negativeClick == true)
        {
            ShopBot.SwitchState(ShopBot.NegativeState);
            negativeClick = false;
        }
    }
}