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

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.PackingState)
        {
            Debug.Log($"{stateName}: {stateDescription}");

            ShopBot.ResetButtons();
            ShopBot.ownBagButton.gameObject.SetActive(true);
            ShopBot.plasticBagButton.gameObject.SetActive(true);
        }

        ShopBot.ownBagButton.onClick.AddListener(Own);
        ShopBot.plasticBagButton.onClick.AddListener(Plastic);
    }
    void Own()
    {
        ownClick = true;
    }

    void Plastic()
    {
        plasticClick = true;
    }

    public override void UpdateState(ShopBotStateManager ShopBot)
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
}