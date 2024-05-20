using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotFeedback : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;
    private float elapsedTime = 0f;

    public ShopBotFeedback()
    {
        stateName = "FEEDBACK";
        stateDescription = "We appreciate your feedback. Thank you for shopping! *Goes back to Idle State*";
    }

<<<<<<< HEAD
=======
    public override void GetDialogue(Text DialogueText)
    {
        DialogueText.text = "We appreciate your feedback. Thank you for shopping!";
    }

>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc
    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.FeedbackState)
        {
            Debug.Log($"{stateName}: {stateDescription}");
<<<<<<< HEAD

            ShopBot.ResetButtons();
=======
            ShopBot.UpdateDialogue();

            ShopBot.ResetButtons();

            ShopBot.DialogueText.gameObject.SetActive(true);
            ShopBot.Background.gameObject.SetActive(true);
            ShopBot.Avatar.gameObject.SetActive(true);
>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc
        }
    }

    public override void UpdateState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;
        elapsedTime += Time.deltaTime;

<<<<<<< HEAD
        if (elapsedTime >= 5.0f)
        {
            //Change to idle state after 5 seconds
            ShopBot.SwitchState(ShopBot.IdleState);

            //Reset timer
            elapsedTime = 0f;
        }
=======
        if (elapsedTime >= 3.0f)
        {
            ShopBot.UI.gameObject.SetActive(false);

            if (elapsedTime >= 5.0f)
            {
                //Change to idle state after 5 seconds
                ShopBot.SwitchState(ShopBot.IdleState);

                //Reset timer
                elapsedTime = 0f;
            }
        }  
    }

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
>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc
    }
}