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
        stateName = "SHOPLIST";
        stateDescription = "Here are the list of items available in the store. *Shows shop list*";
    }

    public override void GetDialogue(Text DialogueText)
    {
        DialogueText.text = "Here are the list of items available in the store."; 
        //Dialogue for current state
    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot; 

        if(ShopBot.currentState == ShopBot.ShoppingState)
        {
            Debug.Log($"{stateName}: {stateDescription}");
            ShopBot.UpdateDialogue();

            ShopBot.ResetButtons(); 

            ShopBot.goCartButton.gameObject.SetActive(true);
            ShopBot.addCartButton.gameObject.SetActive(true);

            ShopBot.DialogueText.gameObject.SetActive(true);
            ShopBot.Background.gameObject.SetActive(true);
            ShopBot.Avatar.gameObject.SetActive(true);  

            //Set respective game objects active
        }

        ShopBot.addCartButton.onClick.AddListener(Yes);
        ShopBot.goCartButton.onClick.AddListener(No);  
    }

    void Yes()
    {
        yesClick = true;
        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
    }

    void No()
    {
        noClick = true;
        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
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
            ShopBot.SwitchState(ShopBot.CartState);
            noClick = false;
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