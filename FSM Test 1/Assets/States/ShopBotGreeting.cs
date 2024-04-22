using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShopBotGreeting : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager; 
    public ShopBotShowShopList shopBotShowShopList;

    private bool yesClick = false; 
    private bool noClick = false;   

    public ShopBotGreeting()
    {
        stateName = "GREETING";
        stateDescription = "Welcome to the store! Would you like to take a look at our list of items?";
    } 

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.GreetingState)
        {
            Debug.Log($"{stateName}: {stateDescription}");

            ShopBot.ResetButtons();
            ShopBot.yesButton.gameObject.SetActive(true);
            ShopBot.noButton.gameObject.SetActive(true);
        }

        ShopBot.yesButton.onClick.AddListener(Yes);
        ShopBot.noButton.onClick.AddListener(No);
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
            ShopBot.SwitchState(ShopBot.ShoppingState);
            yesClick = false;
        }  
        else if (noClick == true)
        {
            Debug.Log("Going back to Idle State.");
            ShopBot.SwitchState(ShopBot.IdleState);
            noClick = false;
        }
    }
}