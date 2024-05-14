using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotReceipt: ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;

    private bool collectClick = false;
    private bool discardClick = false;

    public ShopBotReceipt()
    {
        stateName = "RECEIPT";
        stateDescription = "Here is your receipt. *Gives receipt*";
    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.ReceiptState)
        {
            Debug.Log($"{stateName}: {stateDescription}");

            ShopBot.ResetButtons();
            ShopBot.collectButton.gameObject.SetActive(true);
            ShopBot.discardButton.gameObject.SetActive(true);
        }

        ShopBot.collectButton.onClick.RemoveAllListeners();
        ShopBot.collectButton.onClick.AddListener(Collect);
        ShopBot.discardButton.onClick.AddListener(Discard);
    }
    void Collect()
    {
        collectClick = true;
    }
    void Discard()
    {
        discardClick = true;
    }
    public override void UpdateState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (collectClick == true)
        {
            ShopBot.SwitchState(ShopBot.PackingState);
            collectClick = false;
        }
        else if (discardClick == true)
        {
            ShopBot.SwitchState(ShopBot.PackingState);
            discardClick = false;
        }
    }
}