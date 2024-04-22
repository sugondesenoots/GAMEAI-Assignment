using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotRetrieve : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;

    private bool removeClick = false;
    private bool retrieveClick = false;
    private bool backClick = false;

    public ShopBotRetrieve()
    {
        stateName = "Retrieve";
        stateDescription = "Please wait a moment, I will be retrieving your items.";
    }

    void Update()
    {

    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.RetrieveState)
        {
            Debug.Log($"{stateName}: {stateDescription}");

            shopBotStateManager.ResetButtons();
            
        }

        shopBotStateManager.retrieveButton.onClick.AddListener(retrieve);
        shopBotStateManager.backCartButton.onClick.AddListener(backToList);
        shopBotStateManager.removeItemButton.onClick.AddListener(removeItem);
    }

    void retrieve()
    {
        retrieveClick = true;
    }

    void backToList()
    {
        backClick = true;
    }

    void removeItem()
    {
        removeClick = true;
    }

    public override void UpdateState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (retrieveClick == true)
        {
            //shopBotStateManager.SwitchState(ShopBot.Retrieve);
            retrieveClick = false;
        }
        else if (removeClick == true)
        {
            Debug.Log("Removed item.");
            removeClick = false;
        }
        else if (backClick == true)
        {
            shopBotStateManager.SwitchState(ShopBot.ShoppingState);
            backClick = false;
        }
    }
}