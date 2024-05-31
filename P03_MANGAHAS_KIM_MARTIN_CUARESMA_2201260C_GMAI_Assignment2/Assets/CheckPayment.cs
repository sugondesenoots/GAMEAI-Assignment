using UnityEngine;

public class CheckPayment : MonoBehaviour
{
    public EquipItems equipItems; 
    public PaymentState paymentState; 

    public bool cardPayDetected = false; 
    public bool cashPayDetected = false; 

    //Bools check if cash/card payment is made

    void OnTriggerStay(Collider other)
    {
        //Double-checks if it is in PaymentState 
        //Don't want it skipping states
        if (paymentState.IsPaymentState())
        {
            //Checks if the player is holding a cash/card when it is in range of the POS System
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
