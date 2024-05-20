using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopBotBaseState
{
    public string stateName;
    public string stateDescription;

    public virtual void EnterState(ShopBotStateManager ShopBot)
    {

    }
    public virtual void UpdateState(ShopBotStateManager ShopBot)
    {

    }
}
