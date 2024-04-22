using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotShowShopList : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;

    private bool yesClick = false;
    private bool noClick = false; 

    public ShopBotShowShopList()
    {
        stateName = "Show Shop List";
        stateDescription = "Here are the list of items available in the store. *Shows shop list*";
    }

    void Update()
    {
        
    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot; 

        if(ShopBot.currentState == ShopBot.ShoppingState)
        {
            Debug.Log($"{stateName}: {stateDescription}");

            shopBotStateManager.ResetButtons();
            ShopBot.goCartButton.gameObject.SetActive(true);
            ShopBot.addCartButton.gameObject.SetActive(true);
        } 

        shopBotStateManager.addCartButton.onClick.AddListener(Yes);
        shopBotStateManager.goCartButton.onClick.AddListener(No);  
    }

    void Yes()
    {
        yesClick = true;
    }

    void No()
    {
        noClick = true;
    }

    public override void UpdateState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (yesClick == true)
        {
            Debug.Log("Added item to cart.");
            yesClick = false;
        }
        else if (noClick == true)
        {
            shopBotStateManager.SwitchState(ShopBot.CartState);
            noClick = false;
        }
    }
}