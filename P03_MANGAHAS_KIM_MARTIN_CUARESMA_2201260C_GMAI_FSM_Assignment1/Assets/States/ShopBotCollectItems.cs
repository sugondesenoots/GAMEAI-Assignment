using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotCollectItems : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;

    private bool collectClick = false;

    public ShopBotCollectItems()
    {
        stateName = "COLLECT ITEMS";
        stateDescription = "Finished packing. Please collect your items.";
    }

    public override void GetDialogue(Text DialogueText)
    {
        DialogueText.text = "Finished packing. Please collect your items.";
    }

    public override void EnterState(ShopBotStateManager ShopBot, ShopCart Cart)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.CollectItemsState)
        {
            Debug.Log($"{stateName}: {stateDescription}");
            ShopBot.UpdateDialogue();

            ShopBot.ResetButtons(); 

            ShopBot.collectButton.gameObject.SetActive(true);

            ShopBot.DialogueText.gameObject.SetActive(true);
            ShopBot.Background.gameObject.SetActive(true);
            ShopBot.Avatar.gameObject.SetActive(true);
        }

        ShopBot.collectButton.onClick.RemoveAllListeners();
        ShopBot.collectButton.onClick.AddListener(Collect);
    }
    void Collect()
    {
        collectClick = true;

        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
    }
    public override void UpdateState(ShopBotStateManager ShopBot, ShopCart Cart)
    {
        shopBotStateManager = ShopBot;

        if (collectClick == true)
        {
            Debug.Log("Items collected.");
            ShopBot.SwitchState(ShopBot.RatingState);
            collectClick = false;
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