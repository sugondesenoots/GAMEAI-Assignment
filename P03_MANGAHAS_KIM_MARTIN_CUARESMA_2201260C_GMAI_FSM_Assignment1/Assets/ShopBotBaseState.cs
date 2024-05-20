using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using UnityEngine.UI;
>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc

public abstract class ShopBotBaseState
{
    public string stateName;
    public string stateDescription;

<<<<<<< HEAD
=======
    public virtual void GetDialogue(Text DialogueText)
    {

    }

>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc
    public virtual void EnterState(ShopBotStateManager ShopBot)
    {

    }
    public virtual void UpdateState(ShopBotStateManager ShopBot)
    {

    }
<<<<<<< HEAD
=======

    public virtual void OnTriggerEnter(ShopBotStateManager ShopBot, Collider other)
    {

    }

    public virtual void OnTriggerExit(ShopBotStateManager ShopBot, Collider other)
    {

    }
>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc
}
