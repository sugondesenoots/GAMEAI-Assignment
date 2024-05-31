using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Panda;

public class CartState : MonoBehaviour
{
    public ShopBotStateManager _stateManager;
    public ShopCart _shopCart;
    private PandaBehaviour _bt;

    public Button _retrieveBtn;
    public Button _backBtn;

    private bool retrieveClicked = false;
    private bool backClicked = false;

    public void Initialize(ShopBotStateManager stateManager, ShopCart shopCart, PandaBehaviour bt, Button retrieveBtn, Button backBtn)
    {
        _stateManager = stateManager;
        _shopCart = shopCart;
        _bt = bt;

        _retrieveBtn = retrieveBtn;
        _backBtn = backBtn;

        _stateManager.ResetUI(); 
    }

    [Task]
    public bool IsCartState()  
    {  
        return _stateManager.currentStateName == "CartState";

        //Follows same logic as previous states (IdleState, etc.)
    }

    [Task]
    void WaitForRetrieveClick()
    {
        _retrieveBtn.onClick.AddListener(RetrieveClick);

        if (retrieveClicked)
        {
            retrieveClicked = true;
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

        //Follows same logic as previous states (IdleState, etc.)
    }

    void RetrieveClick()
    {
        retrieveClicked = true;
    }

    [Task]
    void SwitchToRetrieve()
    {
        if (retrieveClicked)
        {
            retrieveClicked = false;

            int itemsInCartCount = _shopCart.itemsInCart.Count;

            if (itemsInCartCount > 0) //Checks if cart is empty
            {
                _stateManager.SetCurrentState("RetrieveState");
                _stateManager.ResetUI();
                Task.current.Succeed();
            }
            else
            {
                Task.current.Fail(); //Fails if cart is empty 
                Debug.Log("Nothing in cart to retrieve!");
            } 
             
            //This way, the bot does not retrieve when the cart is empty 
            //Wouldn't make sense for the bot to fetch nothing
        }
        else
        {
            Task.current.Fail();
        }

        //Other than what I commented above, it follows same logic as previous states (IdleState, etc.)
    }


    [Task]
    void WaitForBackToShoplist()
    {
        _backBtn.onClick.AddListener(BackClick);

        if (backClicked)
        {
            backClicked = true;
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

        //Follows same logic as previous states (IdleState, etc.)
    }

    void BackClick()
    {
        backClicked = true;
    }

    [Task]
    void BackToShoplist()
    {
        if (backClicked)
        {
            backClicked = false;
            _stateManager.SetCurrentState("ShoplistState");

            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

        //Follows same logic as previous states (IdleState, etc.)
    }
}
