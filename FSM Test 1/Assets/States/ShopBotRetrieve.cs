using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotRetrieve : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;

    private float elapsedTime = 0f;         
    private bool backClick = false; 
     
    public ShopBotRetrieve()
    {
        stateName = "Retrieve";
        stateDescription = "Please wait a moment, I will be retrieving your items.";
    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.RetrieveState)
        {
            Debug.Log($"{stateName}: {stateDescription}");

            shopBotStateManager.ResetButtons();
            shopBotStateManager.backButton.gameObject.SetActive(true);
        }
        shopBotStateManager.backButton.onClick.RemoveAllListeners(); 
        shopBotStateManager.backButton.onClick.AddListener(back); 
    }
    
    void back()
    {
        backClick = true;
    }

    public override void UpdateState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot; 
        elapsedTime += Time.deltaTime;
  
        if(elapsedTime >= 5.0f)
        {
            //Change to follow state after 5 seconds
            ShopBot.SwitchState(ShopBot.FollowState); 

            //Reset timer
            elapsedTime = 0f;
        }
        else if (backClick == true)
        {
            ShopBot.SwitchState(ShopBot.CartState);
            backClick = false;
        }
    }
}