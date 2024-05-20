using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotPacking : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;

    private bool ownClick = false;
    private bool plasticClick = false;

    public ShopBotPacking()
    {
        stateName = "PACKING";
        stateDescription = "Choose your package type. Use your own bag or store's plastic bag?";
    }

    public override void GetDialogue(Text DialogueText)
    {
        DialogueText.text = "Choose your package type. Use your own bag or store's plastic bag?";
    }

    public override void EnterState(ShopBotStateManager ShopBot, ShopCart Cart)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.PackingState)
        {
            Debug.Log($"{stateName}: {stateDescription}");
            ShopBot.UpdateDialogue();

            ShopBot.ResetButtons(); 

            ShopBot.ownBagButton.gameObject.SetActive(true);
            ShopBot.plasticBagButton.gameObject.SetActive(true);

            ShopBot.DialogueText.gameObject.SetActive(true);
            ShopBot.Background.gameObject.SetActive(true);
            ShopBot.Avatar.gameObject.SetActive(true);
        }

        ShopBot.ownBagButton.onClick.AddListener(Own);
        ShopBot.plasticBagButton.onClick.AddListener(Plastic);
    }
    void Own()
    {
        ownClick = true;

        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
    }

    void Plastic()
    {
        plasticClick = true;

        shopBotStateManager.DialogueText.gameObject.SetActive(false);
        shopBotStateManager.Background.gameObject.SetActive(false);
        shopBotStateManager.Avatar.gameObject.SetActive(false);
    }

    public override void UpdateState(ShopBotStateManager ShopBot, ShopCart Cart)
    {
        shopBotStateManager = ShopBot;

        if (ownClick == true)
        {
            Debug.Log("Own bag selected. *Pass your bag* Thank you for reducing plastic usage! Please wait for packing.");
            ShopBot.SwitchState(ShopBot.CollectItemsState);
            ownClick = false;
        }
        else if (plasticClick == true)
        {
            Debug.Log("Plastic bag selected. Please wait for packing.");
            ShopBot.SwitchState(ShopBot.CollectItemsState);
            plasticClick = false;
        }
    }

    public override void OnTriggerEnter(ShopBotStateManager ShopBot, ShopCart Cart, Collider other)
    {
        shopBotStateManager = ShopBot;
        ShopBot.UI.gameObject.SetActive(true);

        EnterState(ShopBot, Cart);

        //Loads in the specific buttons needed for the current state 
        //In this case, it is the Interact button    

        ShopBot.UpdateDialogue();

        shopBotStateManager.DialogueText.gameObject.SetActive(true);
        shopBotStateManager.Background.gameObject.SetActive(true);
        shopBotStateManager.Avatar.gameObject.SetActive(true);
    }

    public override void OnTriggerExit(ShopBotStateManager ShopBot, ShopCart Cart, Collider other)
    {
        shopBotStateManager = ShopBot;
        ShopBot.ResetButtons();
        ShopBot.UI.gameObject.SetActive(false);
    }
}