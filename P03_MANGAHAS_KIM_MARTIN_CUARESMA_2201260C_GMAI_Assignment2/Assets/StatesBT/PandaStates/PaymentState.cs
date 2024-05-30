using Panda;
using UnityEngine;

public class PaymentState : MonoBehaviour
{
    public ShopBotStateManager _stateManager;
    public EquipItems equipItems;

    private bool cardPayDetected = false;
    private bool cashPayDetected = false;

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
        if (cardPayDetected || cashPayDetected)
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
        if (cardPayDetected || cashPayDetected)
        {
            cardPayDetected = false;
            cashPayDetected = false;
            _stateManager.SetCurrentState("PackingState");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsPaymentState())
        {
            if (equipItems.holdCard && other.gameObject.CompareTag("POS"))
            {
                cardPayDetected = true;
                Debug.Log("Card detected at POS");
            }
            else if (equipItems.holdCash && other.gameObject.CompareTag("POS"))
            {
                cashPayDetected = true;
                Debug.Log("Cash detected at POS");
            }
        }
    }
}
