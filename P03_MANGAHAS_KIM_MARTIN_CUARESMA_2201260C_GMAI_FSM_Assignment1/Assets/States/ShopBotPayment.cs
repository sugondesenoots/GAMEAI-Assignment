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

    public override void GetDialogue(Text DialogueText)
    {
        DialogueText.text = "We have reached the counter. Will you pay by Cash or Card?";
    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.PaymentState)
        {
            Debug.Log($"{stateName}: {stateDescription}");
            ShopBot.UpdateDialogue();

            ShopBot.ResetButtons(); 

            ShopBot.cashButton.gameObject.SetActive(true);
            ShopBot.cardButton.gameObject.SetActive(true);

            ShopBot.DialogueText.gameObject.SetActive(true);
            ShopBot.Background.gameObject.SetActive(true);
            ShopBot.Avatar.gameObject.SetActive(true);
        }

        ShopBot.cashButton.onClick.AddListener(Cash);
        ShopBot.cardButton.onClick.AddListener(Card);
    }
    void Cash()
    {
        cashClick = true;

        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
    }

    void Card()
    {
        cardClick = true;

        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
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

    public override void OnTriggerEnter(ShopBotStateManager ShopBot, Collider other)
    {
        shopBotStateManager = ShopBot;
        ShopBot.UI.gameObject.SetActive(true);

        EnterState(ShopBot);

        //Loads in the specific buttons needed for the current state 
        //In this case, it is the Interact button    

        ShopBot.UpdateDialogue();

        shopBotStateManager.DialogueText.gameObject.SetActive(true);
        shopBotStateManager.Background.gameObject.SetActive(true);
        shopBotStateManager.Avatar.gameObject.SetActive(true);
    }

    public override void OnTriggerExit(ShopBotStateManager ShopBot, Collider other)
    {
        shopBotStateManager = ShopBot;
        ShopBot.ResetButtons();
        ShopBot.UI.gameObject.SetActive(false);
    }
}