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
    }

    [Task]
    void WaitForPayment()
    {
        if (checkPayment.cardPayDetected || checkPayment.cashPayDetected)
        {
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
    }
}