using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShopBotIdle : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;
    public ShopBotShowShopList shopBotShowShopList;

    //Initialize the StateManager
    //Initialize any other states to be transitioned to from this state 

    public ShopBotIdle()
    {
        stateName = "IDLE";
        stateDescription = "Interact to Start.";
         
        //Sets the state name and state description
    }

    public override void GetDialogue(Text DialogueText)
    {
        DialogueText.text = "Interact to Start.";
    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.IdleState) //Checks if current state is IDLE state
        {
            Debug.Log($"{stateName}: {stateDescription}");

            //Loads in the state name + description   
        }

        ShopBot.interactButton.onClick.RemoveListener(OnClick);
        //Removes any previous listeners if you decide to return to IDLE state   
        //E.g Return to IDLE state from GREETING state 
        //This avoids any duplicate responses when you click on Interact

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
    }
}