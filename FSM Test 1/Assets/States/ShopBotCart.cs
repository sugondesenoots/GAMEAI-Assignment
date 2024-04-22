using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotCart : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;

    private bool removeClick = false;
    private bool retrieveClick = false;
    private bool backClick = false;

    public ShopBotCart()
    {
        stateName = "Showing Cart";
        stateDescription = "Here are the list of items you have selected. *Shows Cart*";
    }

    void Update()
    {

    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.CartState)
        {
            Debug.Log($"{stateName}: {stateDescription}");

            ShopBot.ResetButtons();
            ShopBot.backButton.gameObject.SetActive(true);
            ShopBot.removeItemButton.gameObject.SetActive(true);
            ShopBot.retrieveButton.gameObject.SetActive(true);
        }
        ShopBot.backButton.onClick.RemoveAllListeners();

        ShopBot.retrieveButton.onClick.AddListener(retrieve);
        ShopBot.backButton.onClick.AddListener(backToList);
        ShopBot.removeItemButton.onClick.AddListener(removeItem);
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
            ShopBot.SwitchState(ShopBot.RetrieveState);
            retrieveClick = false;
        }
        else if (removeClick == true)
        {
            Debug.Log("Removed item.");
            removeClick = false;
        } 
        else if (backClick == true)
        {
            ShopBot.SwitchState(ShopBot.ShoppingState);
            backClick = false;
        }
    }
}