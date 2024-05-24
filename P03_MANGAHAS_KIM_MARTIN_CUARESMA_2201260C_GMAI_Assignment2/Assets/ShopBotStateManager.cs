using UnityEngine;
using UnityEngine.UI;
using Panda;

public class ShopBotStateManager : MonoBehaviour
{
    public GameObject idleButtons;
    public GameObject shoplistButtons;
    public GameObject cartButtons;
    public GameObject retrieveButtons;
    public GameObject confirmButtons;
    public GameObject followButtons;
    public GameObject paymentButtons;
    public GameObject packingButtons;
    public GameObject collectionButtons;
    public GameObject ratingButtons;

    public Text dialogueText;
    public RawImage background;
    public RawImage avatar;

    public Canvas UI;
    public Canvas shopUI;
    public Canvas cartUI;

    public PandaBehaviour behaviorTree;

    public string currentStateName;
    private bool insideTriggerZone = false;

    private void Start()
    {
        ResetUI();
        SetCurrentState("IdleState");
    }

    private void Update()
    {
        HandleTriggerZone();

        if (insideTriggerZone)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            insideTriggerZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            insideTriggerZone = false;
        }
    }

    private void HandleTriggerZone()
    {
        if (insideTriggerZone)
        {
            switch (currentStateName)
            {
                case "IdleState":
                    ResetUI();
                    UI.gameObject.SetActive(true);
                    idleButtons.SetActive(true);
                    dialogueText.gameObject.SetActive(true);
                    background.gameObject.SetActive(true);
                    avatar.gameObject.SetActive(true);
                    break;
                case "ShoplistState":
                    ResetUI();
                    shopUI.gameObject.SetActive(true);
                    shoplistButtons.SetActive(true);
                    break;
                case "CartState":
                    ResetUI();
                    cartUI.gameObject.SetActive(true);
                    cartButtons.SetActive(true); 
                    break;
                case "RetrieveState":
                    ResetUI();
                    UI.gameObject.SetActive(true);
                    retrieveButtons.SetActive(true);
                    dialogueText.gameObject.SetActive(true);
                    background.gameObject.SetActive(true);
                    avatar.gameObject.SetActive(true);
                    break;
                case "ConfirmState":
                    ResetUI();
                    UI.gameObject.SetActive(true);
                    confirmButtons.SetActive(true);
                    dialogueText.gameObject.SetActive(true);
                    background.gameObject.SetActive(true);
                    avatar.gameObject.SetActive(true);
                    break;
                case "FollowState":
                    ResetUI();
                    UI.gameObject.SetActive(true);
                    followButtons.SetActive(true);
                    dialogueText.gameObject.SetActive(true);
                    background.gameObject.SetActive(true);
                    avatar.gameObject.SetActive(true);
                    break;
                case "PaymentState":
                    ResetUI();
                    UI.gameObject.SetActive(true);
                    paymentButtons.SetActive(true);
                    dialogueText.gameObject.SetActive(true);
                    background.gameObject.SetActive(true);
                    avatar.gameObject.SetActive(true);
                    break;
                case "PackingState":
                    ResetUI();
                    UI.gameObject.SetActive(true);
                    packingButtons.SetActive(true);
                    dialogueText.gameObject.SetActive(true);
                    background.gameObject.SetActive(true);
                    avatar.gameObject.SetActive(true);
                    break;
                default:
                    ResetUI();
                    break;
            }
        }
        else
        {
            ResetUI();
        }
    }

    public void ResetUI()
    {
        idleButtons.gameObject.SetActive(false);
        shoplistButtons.gameObject.SetActive(false);
        cartButtons.gameObject.SetActive(false);
        retrieveButtons.gameObject.SetActive(false);
        confirmButtons.gameObject.SetActive(false);
        followButtons.gameObject.SetActive(false);
        paymentButtons.gameObject.SetActive(false);
        packingButtons.gameObject.SetActive(false);
        collectionButtons.gameObject.SetActive(false);
        ratingButtons.gameObject.SetActive(false);

        shopUI.gameObject.SetActive(false);
        cartUI.gameObject.SetActive(false);
        UI.gameObject.SetActive(false);

        dialogueText.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        avatar.gameObject.SetActive(false);
    }

    public void SetCurrentState(string stateName)
    {
        currentStateName = stateName;

        behaviorTree.enabled = true;
    }
}