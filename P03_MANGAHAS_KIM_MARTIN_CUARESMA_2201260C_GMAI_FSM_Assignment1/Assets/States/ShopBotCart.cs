using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShopBotCart : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;
    public ShopCart shopCart; 

    private bool removeClick = false;
    private bool retrieveClick = false;
    private bool backClick = false;

    public ShopBotCart()
    {
        stateName = "CART";
        stateDescription = "Here are the list of items you have selected. *Shows Cart*"; 
    }

    public override void GetDialogue(Text DialogueText)
    {
        DialogueText.text = "Proceeding to CART. Here are the items you have selected.";
        //Dialogue for current state
    }

    public override void EnterState(ShopBotStateManager ShopBot, ShopCart Cart)
    {
        shopBotStateManager = ShopBot; 
        shopCart = Cart;

        if (ShopBot.currentState == ShopBot.CartState)
        {
            Debug.Log($"{stateName}: {stateDescription}");
            ShopBot.UpdateDialogue();

            ShopBot.ResetButtons(); 

            ShopBot.backButton.gameObject.SetActive(true);
            ShopBot.removeItemButton1.gameObject.SetActive(true);
            ShopBot.removeItemButton2.gameObject.SetActive(true);
            ShopBot.removeItemButton3.gameObject.SetActive(true);
            ShopBot.retrieveButton.gameObject.SetActive(true);

            shopBotStateManager.DialogueText.gameObject.SetActive(false);
            shopBotStateManager.Background.gameObject.SetActive(false);
            shopBotStateManager.Avatar.gameObject.SetActive(false); 
             
            shopBotStateManager.ShopUI.gameObject.SetActive(false);
            shopBotStateManager.cartUI.gameObject.SetActive(true);
        }
        ShopBot.backButton.onClick.RemoveAllListeners();

        ShopBot.retrieveButton.onClick.AddListener(retrieve);
        ShopBot.backButton.onClick.AddListener(backToList); 

        ShopBot.removeItemButton1.onClick.AddListener(removeItem);
        ShopBot.removeItemButton2.onClick.AddListener(removeItem);
        ShopBot.removeItemButton3.onClick.AddListener(removeItem);
    }

    void retrieve()
    {  
        if (shopCart.itemsInCart.Count == 0)
        {
            Debug.Log("There is nothing in the cart. Please select items to retrieve.");
            retrieveClick = false;
        }
        else if (shopCart.itemsInCart.Count > 0)
        {
            shopBotStateManager.DialogueText.gameObject.SetActive(false);
            shopBotStateManager.Background.gameObject.SetActive(false);
            shopBotStateManager.Avatar.gameObject.SetActive(false);
            shopBotStateManager.cartUI.gameObject.SetActive(false);

            retrieveClick = true;
        }
    }

    void backToList()  
    {
        backClick = true; 

        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);

        shopBotStateManager.cartUI.gameObject.SetActive(false);
    }

    void removeItem()
    {
        removeClick = true; 

        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
    }

    public override void UpdateState(ShopBotStateManager ShopBot, ShopCart Cart)
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