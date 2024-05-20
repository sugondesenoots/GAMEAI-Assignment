using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using Unity.VisualScripting;
>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShopBotIdle : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;
<<<<<<< HEAD
    public ShopBotShowShopList shopBotShowShopList;  

    //Initialize the StateManager
    //Initialize any other states to be transitioned to from this state
=======
    public ShopBotShowShopList shopBotShowShopList;

    //Initialize the StateManager
    //Initialize any other states to be transitioned to from this state 
>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc

    public ShopBotIdle()
    {
        stateName = "IDLE";
<<<<<<< HEAD
        stateDescription = "Interact to Start."; 
=======
        stateDescription = "Interact to Start.";
>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc
         
        //Sets the state name and state description
    }

<<<<<<< HEAD
=======
    public override void GetDialogue(Text DialogueText)
    {
        DialogueText.text = "Interact to Start.";
    }

>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc
    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.IdleState) //Checks if current state is IDLE state
        {
            Debug.Log($"{stateName}: {stateDescription}");
<<<<<<< HEAD
            ShopBot.ResetButtons();
            ShopBot.interactButton.gameObject.SetActive(true); 
             
            //Loads in the state name + description 
            //Resets all the buttons to remove any excess buttons 
            //Loads in the specific buttons needed for the current state 
            //In this case, it is the Interact button
        } 


        ShopBot.interactButton.onClick.RemoveListener(OnClick); 
=======

            //Loads in the state name + description   
        }

        ShopBot.interactButton.onClick.RemoveListener(OnClick);
>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc
        //Removes any previous listeners if you decide to return to IDLE state   
        //E.g Return to IDLE state from GREETING state 
        //This avoids any duplicate responses when you click on Interact

<<<<<<< HEAD
        ShopBot.interactButton.onClick.AddListener(OnClick); 
        //Simply adds a listener to check for when the Interact button is clicked
    }

    void OnClick()
    {
        shopBotStateManager.SwitchState(shopBotStateManager.GreetingState); 
        //Switches to GREETING state if button is clicked
=======
        ShopBot.interactButton.onClick.AddListener(OnClick);
        //Simply adds a listener to check for when the Interact button is clicked
    }

    public override void OnTriggerEnter(ShopBotStateManager ShopBot, Collider other)
    {
        shopBotStateManager = ShopBot;
        ShopBot.UI.gameObject.SetActive(true);

        EnterState(ShopBot);
        ShopBot.interactButton.gameObject.SetActive(true);

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

    void OnClick()
    {
        shopBotStateManager.SwitchState(shopBotStateManager.GreetingState);
        //Switches to GREETING state if button is clicked 
>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc
    }
}