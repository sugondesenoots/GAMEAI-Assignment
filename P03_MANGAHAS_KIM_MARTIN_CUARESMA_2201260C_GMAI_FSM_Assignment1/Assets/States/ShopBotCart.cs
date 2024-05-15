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
        stateName = "CART";
        stateDescription = "Here are the list of items you have selected. *Shows Cart*";
    }

    public override void GetDialogue(Text DialogueText)
    {
        DialogueText.text = "Proceeding to CART. Here are the items you have selected.";
        //Dialogue for current state
    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.CartState)
        {
            Debug.Log($"{stateName}: {stateDescription}");
            ShopBot.UpdateDialogue();

            ShopBot.ResetButtons(); 

            ShopBot.backButton.gameObject.SetActive(true);
            ShopBot.removeItemButton.gameObject.SetActive(true);
            ShopBot.retrieveButton.gameObject.SetActive(true);

            ShopBot.DialogueText.gameObject.SetActive(true);
            ShopBot.Background.gameObject.SetActive(true);
            ShopBot.Avatar.gameObject.SetActive(true);
        }
        ShopBot.backButton.onClick.RemoveAllListeners();

        ShopBot.retrieveButton.onClick.AddListener(retrieve);
        ShopBot.backButton.onClick.AddListener(backToList);
        ShopBot.removeItemButton.onClick.AddListener(removeItem);
    }

    void retrieve()
    {
        retrieveClick = true;
        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
    } 
     
    void backToList()  
    {
        backClick = true;
        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
    }

    void removeItem()
    {
        removeClick = true;
        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
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