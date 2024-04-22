using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBotStateManager : MonoBehaviour
{
    public Button interactButton;
    public Button noButton;
    public Button yesButton;
    public Button goCartButton;
    public Button addCartButton;
    public Button backCartButton;
    public Button removeItemButton;
    public Button retrieveButton;

    public ShopBotBaseState currentState; 
    public ShopBotGreeting GreetingState = new ShopBotGreeting();  
    public ShopBotShowShopList ShoppingState = new ShopBotShowShopList();
    public ShopBotCart CartState = new ShopBotCart();   
    public ShopBotRetrieve RetrieveState = new ShopBotRetrieve();

    void Start()
    { 
        currentState = GreetingState; 
        currentState.EnterState(this);

        ResetButtons();
        interactButton.gameObject.SetActive(true);
    } 
     
    public void ResetButtons()
    {
        interactButton.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        goCartButton.gameObject.SetActive(false);
        addCartButton.gameObject.SetActive(false);
        backCartButton.gameObject.SetActive(false);
        removeItemButton.gameObject.SetActive(false);
        retrieveButton.gameObject.SetActive(false);
    }
    void Update()
    {
        currentState.UpdateState(this); 
    } 
     
    public void SwitchState(ShopBotBaseState state)
    {
        currentState = state; 
        state.EnterState(this);
    }
}
