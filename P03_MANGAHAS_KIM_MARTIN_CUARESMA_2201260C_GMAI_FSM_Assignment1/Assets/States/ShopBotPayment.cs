using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotPayment : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;

    private bool cardClick = false;
    private bool cashClick = false;

    public ShopBotPayment()
    {
        stateName = "PAYMENT";
        stateDescription = "We have reached the counter. Will you pay by Cash or Card?";
    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.PaymentState)
        {
            Debug.Log($"{stateName}: {stateDescription}");

            ShopBot.ResetButtons();
            ShopBot.cashButton.gameObject.SetActive(true);
            ShopBot.cardButton.gameObject.SetActive(true);
        }

        ShopBot.cashButton.onClick.AddListener(Cash);
        ShopBot.cardButton.onClick.AddListener(Card);
    }
    void Cash()
    {
        cashClick = true;
    }

    void Card()
    {
        cardClick = true;
    }

    public override void UpdateState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (cashClick == true)
        {
            Debug.Log("Cash selected. Thank you for your purchase.");
            ShopBot.SwitchState(ShopBot.ReceiptState);
            cashClick = false;
        }
        else if (cardClick == true)
        {
            Debug.Log("Card selected. Thank you for your purchase.");
            ShopBot.SwitchState(ShopBot.ReceiptState);
            cardClick = false;
        }
    }
}