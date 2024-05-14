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

    public virtual void EnterState(ShopBotStateManager ShopBot)
    {

    }
    public virtual void UpdateState(ShopBotStateManager ShopBot)
    {

    }

    public virtual void OnTriggerEnter(ShopBotStateManager ShopBot, Collider other)
    {

    }

    public virtual void OnTriggerExit(ShopBotStateManager ShopBot, Collider other)
    {

    }
}
