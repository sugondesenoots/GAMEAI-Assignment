using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBotGreeting : ShopBotBaseState
{
    public ShopBotGreeting()
    {
        stateName = "GREETING";
        stateDescription = "Welcome to the supermarket!";
    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        Debug.Log($"{stateName}: {stateDescription}");
    }

    public override void UpdateState(ShopBotStateManager ShopBot)
    {

    }

    public override void OnCollisionEnter(ShopBotStateManager ShopBot, Collision collision)
    {

    }

    void Update()
    {
        
    }
}
