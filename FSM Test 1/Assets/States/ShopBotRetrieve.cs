using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotRetrieve : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;

    private bool startTimer = false;
    private bool backClick = false;

    public ShopBotRetrieve()
    {
        stateName = "Retrieve";
        stateDescription = "Please wait a moment, I will be retrieving your items.";
    }
     
    void Start()
    {
        startTimer = true;
    }

    void Update()
    {

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

        shopBotStateManager.backButton.onClick.AddListener(back);
    }
    IEnumerator ChangeStateAfterWaitingTime() 
    { 
        //Waiting time
        yield return new WaitForSeconds(5.0f);

        // Change state after 5 seconds
        shopBotStateManager.SwitchState(shopBotStateManager.FollowMeState);
    }
    void back()
    {
        backClick = true;
    }

    public override void UpdateState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;
  
        if(startTimer == true)
        {
            ChangeStateAfterWaitingTime();
            startTimer = false;
        }
        else if (backClick == true)
        {
            shopBotStateManager.SwitchState(ShopBot.CartState);
            backClick = false;
        }
    }
}