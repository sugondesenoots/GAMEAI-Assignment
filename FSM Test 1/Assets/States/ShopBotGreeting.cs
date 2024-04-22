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
            shopBotStateManager.interactButton.onClick.AddListener(OnClick);
            shopBotStateManager.yesButton.onClick.AddListener(Yes);
            shopBotStateManager.noButton.onClick.AddListener(No);
        }

        shopBotStateManager.ResetButtons();
        shopBotStateManager.interactButton.gameObject.SetActive(true);
    } 
     
    void OnClick()
    {
        Debug.Log($"{stateName}: {stateDescription}");

        shopBotStateManager.ResetButtons();
        shopBotStateManager.yesButton.gameObject.SetActive(true);
        shopBotStateManager.noButton.gameObject.SetActive(true);
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
            shopBotStateManager.SwitchState(ShopBot.ShoppingState);
            yesClick = false;
        }  
        else if (noClick == true)
        {
            Debug.Log("Going back to Idle State.");
            noClick = false;

            //shopBotStateManager.currentState = ShopBot.IdleState;
        }
    }

    void Update()
    {

    }
}