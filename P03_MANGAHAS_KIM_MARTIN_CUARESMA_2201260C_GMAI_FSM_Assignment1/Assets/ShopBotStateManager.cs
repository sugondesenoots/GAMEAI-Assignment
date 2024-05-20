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
    public Button backButton;
    public Button retrieveButton; 
    public Button confirmButton;
    public Button followButton;
    public Button cashButton;
    public Button cardButton;
    public Button ownBagButton;
    public Button plasticBagButton; 
    public Button collectButton;
    public Button positiveButton; 
    public Button negativeButton; 
    public Button discardButton;
    public Button feedbackButton;

    public Button removeItemButton1;
    public Button removeItemButton2;
    public Button removeItemButton3;

    public ShopBotBaseState currentState;  
    public ShopBotIdle IdleState = new ShopBotIdle();
    public ShopBotGreeting GreetingState = new ShopBotGreeting();  
    public ShopBotShowShopList ShoppingState = new ShopBotShowShopList();
    public ShopBotCart CartState = new ShopBotCart();   
    public ShopBotRetrieve RetrieveState = new ShopBotRetrieve();  
    public ShopBotConfirm ConfirmState = new ShopBotConfirm();
    public ShopBotFollow FollowState = new ShopBotFollow(); 
    public ShopBotPayment PaymentState = new ShopBotPayment();
    public ShopBotReceipt ReceiptState = new ShopBotReceipt();
    public ShopBotPacking PackingState = new ShopBotPacking();
    public ShopBotCollectItems CollectItemsState = new ShopBotCollectItems();
    public ShopBotRating RatingState = new ShopBotRating();
    public ShopBotPositive PositiveState = new ShopBotPositive();
    public ShopBotNegative NegativeState = new ShopBotNegative();
    public ShopBotFeedback FeedbackState = new ShopBotFeedback();

    public Text DialogueText;
    public RawImage Background;
    public RawImage Avatar; 

    public Canvas UI;
    public Canvas ShopUI;
    public Canvas cartUI;

    public ShopCart shopCart;

    void Start()
    { 
        currentState = IdleState; 
        currentState.EnterState(this, shopCart);
        ShopUI.gameObject.SetActive(false);
        cartUI.gameObject.SetActive(false);

        ResetButtons();
    } 
     
    public void ResetButtons()
    {
        interactButton.gameObject.SetActive(false); 

        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false); 

        goCartButton.gameObject.SetActive(false);
        addCartButton.gameObject.SetActive(false);  

        backButton.gameObject.SetActive(false); 
        removeItemButton1.gameObject.SetActive(false);
        removeItemButton2.gameObject.SetActive(false);
        removeItemButton3.gameObject.SetActive(false); 
        retrieveButton.gameObject.SetActive(false); 

        confirmButton.gameObject.SetActive(false);
        followButton.gameObject.SetActive(false);
        cashButton.gameObject.SetActive(false);
        cardButton.gameObject.SetActive(false);
        ownBagButton.gameObject.SetActive(false);
        plasticBagButton.gameObject.SetActive(false);
        collectButton.gameObject.SetActive(false);
        positiveButton.gameObject.SetActive(false);
        negativeButton.gameObject.SetActive(false);
        discardButton.gameObject.SetActive(false);
        feedbackButton.gameObject.SetActive(false);
        DialogueText.gameObject.SetActive(false); 
        Background.gameObject.SetActive(false);
        Avatar.gameObject.SetActive(false);
    }

    public void UpdateDialogue()
    {
        currentState.GetDialogue(DialogueText);
    }

    void Update()
    {
        currentState.UpdateState(this, shopCart);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player"))
        {
            currentState.OnTriggerEnter(this, shopCart, other);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player"))
        {
            currentState.OnTriggerExit(this, shopCart, other);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void SwitchState(ShopBotBaseState state)
    {
        currentState = state; 
        state.EnterState(this, shopCart);
        UpdateDialogue();
    }
}
