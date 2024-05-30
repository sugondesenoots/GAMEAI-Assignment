using UnityEngine;

public class CheckPayment : MonoBehaviour
{
    public EquipItems equipItems;   
    public PaymentState paymentState;

    public bool cardPayDetected = false; 
    public bool cashPayDetected = false; 

    void OnTriggerStay(Collider other)
    {
        if (paymentState.IsPaymentState())
        {
            if (equipItems.holdCard && other.CompareTag("Player"))
            {
                cardPayDetected = true;
                Debug.Log("Card detected at POS");
            }
            else if (equipItems.holdCash && other.CompareTag("Player"))
            {
                cashPayDetected = true;
                Debug.Log("Cash detected at POS");
            }
        }
    }
}
