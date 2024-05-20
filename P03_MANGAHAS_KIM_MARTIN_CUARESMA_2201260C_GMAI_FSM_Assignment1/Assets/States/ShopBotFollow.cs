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

    public override void GetDialogue(Text DialogueText)
    {
        DialogueText.text = "Items have been confirmed. Please follow me for payment.";
    }

    public override void EnterState(ShopBotStateManager ShopBot, ShopCart Cart)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.FollowState)
        {
            Debug.Log($"{stateName}: {stateDescription}");
            ShopBot.UpdateDialogue();

            ShopBot.ResetButtons();
            ShopBot.followButton.gameObject.SetActive(true);
            ShopBot.backButton.gameObject.SetActive(true);

            ShopBot.DialogueText.gameObject.SetActive(true);
            ShopBot.Background.gameObject.SetActive(true);
            ShopBot.Avatar.gameObject.SetActive(true);
        }

        ShopBot.backButton.onClick.RemoveAllListeners();
        ShopBot.backButton.onClick.AddListener(Back);
        ShopBot.followButton.onClick.AddListener(Follow);
    }
    void Follow()
    {
        followClick = true;
        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
    }

    void Back()
    {
        backClick = true;
        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
    }

    public override void UpdateState(ShopBotStateManager ShopBot, ShopCart Cart)
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
            ShopBot.SwitchState(ShopBot.ConfirmState);
            backClick = false;
        }
    }

    public override void OnTriggerEnter(ShopBotStateManager ShopBot, ShopCart Cart, Collider other)
    {
        shopBotStateManager = ShopBot;
        ShopBot.UI.gameObject.SetActive(true);

        EnterState(ShopBot, Cart);

        //Loads in the specific buttons needed for the current state 
        //In this case, it is the Interact button    

        ShopBot.UpdateDialogue();

        shopBotStateManager.DialogueText.gameObject.SetActive(true);
        shopBotStateManager.Background.gameObject.SetActive(true);
        shopBotStateManager.Avatar.gameObject.SetActive(true);
    }

    public override void OnTriggerExit(ShopBotStateManager ShopBot, ShopCart Cart, Collider other)
    {
        shopBotStateManager = ShopBot;
        ShopBot.ResetButtons();
        ShopBot.UI.gameObject.SetActive(false);
    }
}