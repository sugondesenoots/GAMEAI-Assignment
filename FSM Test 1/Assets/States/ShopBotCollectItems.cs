using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotCollectItems : ShopBotBaseState
{
    public ShopBotStateManager shopBotStateManager;

    private bool collectClick = false;

    public ShopBotCollectItems()
    {
        stateName = "COLLECT ITEMS";
        stateDescription = "Finished packing. Please collect your items.";
    }

    public override void EnterState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (ShopBot.currentState == ShopBot.CollectItemsState)
        {
            Debug.Log($"{stateName}: {stateDescription}");

            ShopBot.ResetButtons();
            ShopBot.collectButton.gameObject.SetActive(true);
        }

        ShopBot.collectButton.onClick.RemoveAllListeners();
        ShopBot.collectButton.onClick.AddListener(Collect);
    }
    void Collect()
    {
        collectClick = true;
    }
    public override void UpdateState(ShopBotStateManager ShopBot)
    {
        shopBotStateManager = ShopBot;

        if (collectClick == true)
        {
            Debug.Log("Items collected.");
            ShopBot.SwitchState(ShopBot.RatingState);
            collectClick = false;
        }
    }
}