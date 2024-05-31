using Panda;
using UnityEngine;

public class PaymentState : MonoBehaviour
{
    public ShopBotStateManager _stateManager;
    public EquipItems equipItems;
    public CheckPayment checkPayment;

    public void Initialize(ShopBotStateManager stateManager)
    {
        _stateManager = stateManager;
        _stateManager.ResetUI(); 
    }

    [Task]
    public bool IsPaymentState()
    {
        return _stateManager.currentStateName == "PaymentState";

        //Follows same logic as previous states (IdleState, etc.)
    }

    [Task]
    void PaymentDialogue()
    {
        if (!_stateManager.dialogueText.gameObject.activeSelf)
        {
            _stateManager.dialogueText.text = "Provide Cash/Card at the POS System on the left-hand side of the counter.";
            _stateManager.dialogueText.gameObject.SetActive(true);
        }
        Task.current.Succeed();

        //Follows same logic as previous states (IdleState, etc.)
    }

    [Task]
    void WaitForPayment()
    {
        if (checkPayment.cardPayDetected || checkPayment.cashPayDetected)
        {
            //If payment is detected, calls SwitchToPacking function
            SwitchToPacking(); 
            Task.current.Succeed(); 
        }
        else
        {
            Task.current.Fail(); 
        }
    }

    [Task]
    void SwitchToPacking()
    {
        if (checkPayment.cardPayDetected || checkPayment.cashPayDetected)
        {
            checkPayment.cardPayDetected = false;
            checkPayment.cashPayDetected = false;

            _stateManager.SetCurrentState("PackingState");
            Task.current.Succeed(); 
        }
        else
        {
            Task.current.Fail(); 
        }
         
        //Only thing different is how the bools are checked 
        //But those are handled in the CheckPayment script
        //Other than that, it follows same logic as previous states (IdleState, etc.)
    }
}
