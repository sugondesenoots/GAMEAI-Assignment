using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopBotBaseState
{
    public string stateName;
    public string stateDescription;

    public virtual void GetDialogue(Text DialogueText)
    {

    }

    public virtual void EnterState(ShopBotStateManager ShopBot, ShopCart Cart)
    {

    }
    public virtual void UpdateState(ShopBotStateManager ShopBot, ShopCart Cart)
    {

    }

    public virtual void OnTriggerEnter(ShopBotStateManager ShopBot, ShopCart Cart, Collider other)
    {

    }

    public virtual void OnTriggerExit(ShopBotStateManager ShopBot, ShopCart Cart, Collider other)
    {

    }
}
