using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Panda;

public class ShoplistState : MonoBehaviour
{
    public ShopBotStateManager _stateManager;
    public ShopCart _shopCart;
    private PandaBehaviour _bt;

    public Button _goCartBtn;
    public Button _backBtn;

    public Button _vegetables;
    public Button _meat;
    public Button _seasonings;

    private bool goCartClicked = false;
    private bool backClicked = false; 
    private bool listenersAdded = false;

    public void Initialize(ShopBotStateManager stateManager, ShopCart shopCart, PandaBehaviour bt, Button goCartBtn, Button backBtn)
    {
        _stateManager = stateManager; 
        _shopCart = shopCart;
        _bt = bt;

        _goCartBtn = goCartBtn;
        _backBtn = backBtn;

        _stateManager.ResetUI();
    }

    [Task]
    public bool IsShoplistState()  
    {  
        return _stateManager.currentStateName == "ShoplistState"; 
    }

    [Task]
    void WaitForGoCartClick()
    {
        _goCartBtn.onClick.AddListener(GoCartClick);
        _goCartBtn.gameObject.SetActive(true);

        if (goCartClicked)
        {
            goCartClicked = true;
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    void GoCartClick()
    {
        goCartClicked = true;
    }

    [Task]
    void SwitchToCart()
    {
        if (goCartClicked)
        {
            goCartClicked = false;
            _stateManager.SetCurrentState("CartState");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void WaitForBackClick()
    {
        _backBtn.onClick.AddListener(BackClick);
        _backBtn.gameObject.SetActive(true);

        if (backClicked)
        {
            _backBtn.onClick.RemoveListener(BackClick);
            backClicked = true;  
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }


    void BackClick()
    {
        backClicked = true;
    }

    [Task]
    void BackToIdle()
    {
        if (backClicked)
        {
            backClicked = false;
            _stateManager.SetCurrentState("IdleState");

            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    void AddItemsToCart()
    {
        if (!listenersAdded)  //Prevents more than 1 listener being added at a time
        {
            _vegetables.onClick.AddListener(VegetableAdd);
            _meat.onClick.AddListener(MeatAdd);
            _seasonings.onClick.AddListener(SeasoningAdd);
            listenersAdded = true;
        } 
         
        //Implemented the condition due to a problem where when I clicked onto the button
        //The quantity of the item would amount to a large number rather than incrementing by 1 normally

        Task.current.Succeed();
    }

    void VegetableAdd()
    {
        _shopCart.AddItemToCart("Vegetables");
    }

    void MeatAdd()
    {
        _shopCart.AddItemToCart("Meat");
    }

    void SeasoningAdd()
    {
        _shopCart.AddItemToCart("Seasonings");
    }
}
