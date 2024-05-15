using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotReceipt: ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;

    private bool collectClick = false;
    private bool discardClick = false;

    public ShopBotReceipt()
    {
        stateName = "RECEIPT";
        stateDescription = "Here is your receipt. *Gives receipt*";
    }

    public override void GetDialogue(Text DialogueText)
    {
        DialogueText.text = "Here is your receipt. Would you like to keep it or discard it?";
    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.ReceiptState)
        {
            Debug.Log($"{stateName}: {stateDescription}");
            ShopBot.UpdateDialogue();

            ShopBot.ResetButtons(); 

            ShopBot.collectButton.gameObject.SetActive(true);
            ShopBot.discardButton.gameObject.SetActive(true);

            ShopBot.DialogueText.gameObject.SetActive(true);
            ShopBot.Background.gameObject.SetActive(true);
            ShopBot.Avatar.gameObject.SetActive(true);
        }

        ShopBot.collectButton.onClick.RemoveAllListeners();
        ShopBot.collectButton.onClick.AddListener(Collect);
        ShopBot.discardButton.onClick.AddListener(Discard);
    }
    void Collect()
    {
        collectClick = true;

        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
    }
    void Discard()
    {
        discardClick = true;

        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
    }
    public override void UpdateState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (collectClick == true)
        {
            ShopBot.SwitchState(ShopBot.PackingState);
            collectClick = false;
        }
        else if (discardClick == true)
        {
            ShopBot.SwitchState(ShopBot.PackingState);
            discardClick = false;
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