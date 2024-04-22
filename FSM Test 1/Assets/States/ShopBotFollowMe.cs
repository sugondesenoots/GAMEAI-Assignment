using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotFollowMe : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;

    private bool followClick = false;
    private bool backClick = false;

    public ShopBotFollowMe()
    {
        stateName = "Follow Me";
        stateDescription = "I have collected your selected items. Please follow me for checkout.";
    }

    void Update()
    {

    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.FollowMeState)
        {
            Debug.Log($"{stateName}: {stateDescription}");

            shopBotStateManager.ResetButtons();
            //shopBotStateManager.followButton.gameObject.SetActive(true);
            shopBotStateManager.backButton.gameObject.SetActive(true);
        }
    } 
    void follow()
    {
        followClick = true;
    }
    void back()
    {
        backClick = true;
    }

    public override void UpdateState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (followClick == true)
        { 
            //shopBotStateManager.SwitchState(ShopBot.CheckoutState);
            followClick = false;
        }
        else if (backClick == true)
        {
            Debug.Log("Items collected are not correct, going back for collection.");
            shopBotStateManager.SwitchState(ShopBot.RetrieveState);
            backClick = false;
        } 
    }
}