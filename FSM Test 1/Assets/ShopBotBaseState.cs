using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopBotBaseState
{
    public string stateName;
    public string stateDescription;

    public abstract void EnterState(ShopBotStateManager ShopBot);
    public abstract void UpdateState(ShopBotStateManager ShopBot);
}
