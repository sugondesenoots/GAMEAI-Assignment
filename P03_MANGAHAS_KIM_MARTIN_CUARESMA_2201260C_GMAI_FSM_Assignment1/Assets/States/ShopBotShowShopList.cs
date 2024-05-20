using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotShowShopList : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;
    public ShopCart shopCart;

    private bool yesClick = false;
    private bool noClick = false; 

    public ShopBotShowShopList()
    {
        stateName = "SHOPLIST";
        stateDescription = "Here are the list of items available in the store. *Shows shop list*";
    }

    public override void EnterState(ShopBotStateManager ShopBot, ShopCart Cart)
    {
        shopBotStateManager = ShopBot;
        shopCart = Cart;

        if (ShopBot.currentState == ShopBot.ShoppingState)
        {
            Debug.Log($"{stateName}: {stateDescription}");
            ShopBot.UpdateDialogue();

            ShopBot.ResetButtons(); 

            ShopBot.goCartButton.gameObject.SetActive(true);
            ShopBot.addCartButton.gameObject.SetActive(true);

            ShopBot.DialogueText.gameObject.SetActive(false);
            ShopBot.Background.gameObject.SetActive(false);
            ShopBot.Avatar.gameObject.SetActive(false);   

            //Set respective game objects active
        }

        ShopBot.addCartButton.onClick.AddListener(Yes);
        ShopBot.goCartButton.onClick.AddListener(No);  
    }

    void Yes()
    {
        yesClick = true; 
    }

    void No()
    {
        noClick = true; 
    }

    public override void UpdateState(ShopBotStateManager ShopBot, ShopCart Cart)
    {
        shopBotStateManager = ShopBot;

        if (yesClick == true)
        {
            ShopBot.SwitchState(ShopBot.GreetingState);
            yesClick = false;
        }
        else if (noClick == true)
        {
            ShopBot.SwitchState(ShopBot.CartState);
            noClick = false;
        }
    }

    public override void OnTriggerEnter(ShopBotStateManager ShopBot, ShopCart Cart, Collider other)
    {
        shopBotStateManager = ShopBot;
        shopCart = Cart;

        ShopBot.UI.gameObject.SetActive(true);
        shopBotStateManager.ShopUI.gameObject.SetActive(true);

        EnterState(ShopBot, Cart);

        //Loads in the specific buttons needed for the current state 
        //In this case, it is the Interact button    

        ShopBot.UpdateDialogue();
    }

    public override void OnTriggerExit(ShopBotStateManager ShopBot, ShopCart Cart, Collider other)
    {
        shopBotStateManager = ShopBot;
        ShopBot.ResetButtons();
        ShopBot.UI.gameObject.SetActive(false);
        shopBotStateManager.ShopUI.gameObject.SetActive(false);
    }
}