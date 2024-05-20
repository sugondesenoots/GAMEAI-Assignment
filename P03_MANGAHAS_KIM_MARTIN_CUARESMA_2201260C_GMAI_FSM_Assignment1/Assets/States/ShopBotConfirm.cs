using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotConfirm : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;

    private bool confirmClick = false;
    private bool backClick = false;

    public ShopBotConfirm()
    {
        stateName = "CONFIRM";
        stateDescription = "I have collected your selected items. Please confirm items.";
    }

<<<<<<< HEAD
=======
    public override void GetDialogue(Text DialogueText)
    {
        DialogueText.text = "I have collected your selected items. Please confirm your items.";
    }

>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc
    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.ConfirmState)
        {
            Debug.Log($"{stateName}: {stateDescription}");
<<<<<<< HEAD
=======
            ShopBot.UpdateDialogue();
>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc

            ShopBot.ResetButtons();
            ShopBot.confirmButton.gameObject.SetActive(true);
            ShopBot.backButton.gameObject.SetActive(true);
<<<<<<< HEAD
=======

            ShopBot.DialogueText.gameObject.SetActive(true);
            ShopBot.Background.gameObject.SetActive(true);
            ShopBot.Avatar.gameObject.SetActive(true);
>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc
        }

        ShopBot.backButton.onClick.RemoveAllListeners();
        ShopBot.backButton.onClick.AddListener(Back);
        ShopBot.confirmButton.onClick.AddListener(Confirm);
    }
    void Confirm()
    {
        confirmClick = true;
<<<<<<< HEAD
=======
        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
>>>>>>> 0b1728b5fca300783c66468caaa5fff729af26cc
    }
    void Back()
    {
        backClick = true;
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

        if (confirmClick == true)
        {
            ShopBot.SwitchState(ShopBot.FollowState);
            confirmClick = false;
        }
        else if (backClick == true)
        {
            Debug.Log("Items collected are not correct, going back for collection.");
            ShopBot.SwitchState(ShopBot.RetrieveState);
            backClick = false;
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