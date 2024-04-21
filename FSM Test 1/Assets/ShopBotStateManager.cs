using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBotStateManager : MonoBehaviour
{
    ShopBotBaseState currentState;
    public ShopBotGreeting GreetingState = new ShopBotGreeting(); 

    void Start()
    {
        currentState = GreetingState; 
        currentState.EnterState(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
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
