using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShopBotGreeting : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager; 
    public ShopBotShowShopList shopBotShowShopList;

    private bool yesClick = false; 
    private bool noClick = false;   

    public ShopBotGreeting()
    {
        stateName = "GREETING";
        stateDescription = "Welcome to the store! Would you like to take a look at our list of items?";
<<<<<<< HEAD
    } 
=======
    }

    public override void GetDialogue(Text DialogueText)
    {
        DialogueText.text = "Hello there! Would you like to look at the list of items?";
    }
>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.GreetingState)
<<<<<<< HEAD
        {
            Debug.Log($"{stateName}: {stateDescription}");

            ShopBot.ResetButtons();
            ShopBot.yesButton.gameObject.SetActive(true);
            ShopBot.noButton.gameObject.SetActive(true);
        }

        ShopBot.yesButton.onClick.AddListener(Yes);
        ShopBot.noButton.onClick.AddListener(No);
    } 
    void Yes() 
    { 
        yesClick = true;
=======
        { 
            Debug.Log($"{stateName}: {stateDescription}");
            ShopBot.UpdateDialogue();

            ShopBot.ResetButtons(); 

            ShopBot.yesButton.gameObject.SetActive(true);
            ShopBot.noButton.gameObject.SetActive(true);

            ShopBot.DialogueText.gameObject.SetActive(true);
            ShopBot.Background.gameObject.SetActive(true);
            ShopBot.Avatar.gameObject.SetActive(true);
        }

        ShopBot.yesButton.onClick.AddListener(Yes);
        ShopBot.noButton.onClick.AddListener(No); 
    }

    void Yes() 
    {
        yesClick = true;
        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc
    }

    void No()
    {
        noClick = true;
<<<<<<< HEAD
=======
        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc
    }

    public override void UpdateState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (yesClick == true)
        {
            ShopBot.SwitchState(ShopBot.ShoppingState);
            yesClick = false;
        }  
        else if (noClick == true)
        {
            Debug.Log("Going back to Idle State.");
            ShopBot.SwitchState(ShopBot.IdleState);
            noClick = false;
        }
    }
<<<<<<< HEAD
=======

    public override void OnTriggerEnter(ShopBotStateManager ShopBot, Collider other)
    {
        shopBotStateManager = ShopBot;
        ShopBot.UI.gameObject.SetActive(true);

        EnterState(ShopBot);

        //Loads in the specific buttons needed for the current state 
        //In this case, it is the Interact button    

        ShopBot.UpdateDialogue();

        shopBotStateManager.DialogueText.gameObject.SetActive(true);
        shopBotStateManager.Background.gameObject.SetActive(true);
        shopBotStateManager.Avatar.gameObject.SetActive(true);
    }

    public override void OnTriggerExit(ShopBotStateManager ShopBot, Collider other)
    {
        shopBotStateManager = ShopBot;
        ShopBot.ResetButtons();
        ShopBot.UI.gameObject.SetActive(false);
    }
>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc
}