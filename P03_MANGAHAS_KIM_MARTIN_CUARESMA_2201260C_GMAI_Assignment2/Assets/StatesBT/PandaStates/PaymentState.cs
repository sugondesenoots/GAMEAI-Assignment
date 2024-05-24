using Panda;
using UnityEngine;
using UnityEngine.UI;

public class PaymentState : MonoBehaviour
{
    public ShopBotStateManager _stateManager; 

    public Button _cardPayBtn;
    public Button _cashPayBtn;

    private bool cardPayClicked = false;
    private bool cashPayClicked = false;

    public void Initialize(ShopBotStateManager stateManager, Button cardPayBtn, Button cashPayBtn)
    {
        _stateManager = stateManager;
        _cardPayBtn = cardPayBtn;
        _cashPayBtn = cashPayBtn;

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
            _stateManager.dialogueText.text = "Scan here to pay.";
            _stateManager.dialogueText.gameObject.SetActive(true);
        }
        Task.current.Succeed();
    }

    [Task]
    void WaitForCardClick()
    {
        _cardPayBtn.onClick.AddListener(CardClick);
        _cardPayBtn.gameObject.SetActive(true);

        if (cardPayClicked)
        {
            cardPayClicked = true;
            Task.current.Succeed();
        } 
        else
        {
            Task.current.Fail();
        }
    }

    void CardClick()
    {
        cardPayClicked = true;
    }

    [Task]
    void WaitForCashClick()
    {
        _cashPayBtn.onClick.AddListener(CashClick);
        _cashPayBtn.gameObject.SetActive(true);

        if (cashPayClicked)
        {
            cardPayClicked = true;
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    void CashClick()
    {
        cashPayClicked = true;
    }

    [Task]
    void SwitchToPacking()
    {
        if (cardPayClicked)
        {
            cardPayClicked = false;
            _stateManager.SetCurrentState("PackingState");
            Task.current.Succeed();
        }
        else if (cashPayClicked)
        {
            cashPayClicked = false;
            _stateManager.SetCurrentState("PackingState");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
}
