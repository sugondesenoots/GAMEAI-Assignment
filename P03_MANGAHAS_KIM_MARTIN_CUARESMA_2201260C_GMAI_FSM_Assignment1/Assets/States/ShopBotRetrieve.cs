using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotRetrieve : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;
    public ShopCart shopCart;

    private float elapsedTime = 0f;         
    private bool backClick = false; 
     
    public ShopBotRetrieve()
    {
        stateName = "RETRIEVE";
        stateDescription = "Please wait a moment, I will be retrieving your items.";
    }

    public override void GetDialogue(Text DialogueText)
    {
        DialogueText.text = "Please wait a moment, I will be retrieving your items.";
    }

    public override void EnterState(ShopBotStateManager ShopBot, ShopCart Cart)
    {
        shopBotStateManager = ShopBot;
        shopCart = Cart;

        if (ShopBot.currentState == ShopBot.RetrieveState)
        {
            Debug.Log($"{stateName}: {stateDescription}");
            ShopBot.UpdateDialogue();

            ShopBot.ResetButtons();
            ShopBot.backButton.gameObject.SetActive(true);

            ShopBot.DialogueText.gameObject.SetActive(true);
            ShopBot.Background.gameObject.SetActive(true);
            ShopBot.Avatar.gameObject.SetActive(true);
        } 

        ShopBot.backButton.onClick.RemoveAllListeners();
        ShopBot.backButton.onClick.AddListener(back); 
    }
    
    void back()
    {
        backClick = true;
        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
    }

    public override void UpdateState(ShopBotStateManager ShopBot, ShopCart Cart)
    {
        shopBotStateManager = ShopBot;
        shopCart = Cart;
        elapsedTime += Time.deltaTime;
  
        if(elapsedTime >= 5.0f)
        {
            //Change to collection state after 5 seconds
            ShopBot.SwitchState(ShopBot.ConfirmState); 

            //Reset timer
            elapsedTime = 0f;
        }
        else if (backClick == true)
        {
            ShopBot.SwitchState(ShopBot.CartState);
            backClick = false;
        }
    }

    public override void OnTriggerEnter(ShopBotStateManager ShopBot, ShopCart Cart, Collider other)
    {
        shopBotStateManager = ShopBot;
        shopCart = Cart;
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
        shopCart = Cart; 

        ShopBot.ResetButtons();
        ShopBot.UI.gameObject.SetActive(false);
    }
}