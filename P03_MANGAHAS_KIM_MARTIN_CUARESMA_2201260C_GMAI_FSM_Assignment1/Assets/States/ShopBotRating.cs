using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotRating : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;

    private bool positiveClick = false; 
    private bool negativeClick = false;

    public ShopBotRating()
    {
        stateName = "RATING";
        stateDescription = "Please rate your experience.";
    }

<<<<<<< HEAD
=======
    public override void GetDialogue(Text DialogueText)
    {
        DialogueText.text = "Please rate your experience.";
    }

>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc
    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.RatingState)
        {
            Debug.Log($"{stateName}: {stateDescription}");
<<<<<<< HEAD

            ShopBot.ResetButtons();
            ShopBot.positiveButton.gameObject.SetActive(true);
            ShopBot.negativeButton.gameObject.SetActive(true);
=======
            ShopBot.UpdateDialogue();

            ShopBot.ResetButtons(); 

            ShopBot.positiveButton.gameObject.SetActive(true);
            ShopBot.negativeButton.gameObject.SetActive(true);

            ShopBot.DialogueText.gameObject.SetActive(true);
            ShopBot.Background.gameObject.SetActive(true);
            ShopBot.Avatar.gameObject.SetActive(true);
>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc
        }

        ShopBot.positiveButton.onClick.AddListener(Positive);
        ShopBot.negativeButton.onClick.AddListener(Negative);
    }
    void Positive()
    {
        positiveClick = true;
<<<<<<< HEAD
=======

        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc
    }
    void Negative()
    {
        negativeClick = true;
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

        if (positiveClick == true)
        {
            ShopBot.SwitchState(ShopBot.PositiveState);
            positiveClick = false;
        }
        else if (negativeClick == true)
        {
            ShopBot.SwitchState(ShopBot.NegativeState);
            negativeClick = false;
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