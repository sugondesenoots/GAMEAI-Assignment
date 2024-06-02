using UnityEngine;
using UnityEngine.UI;
using Panda;
using UnityEngine.AI;

public class ShopBotStateManager : MonoBehaviour
{
    public GameObject idleButtons;
    public GameObject shoplistButtons;
    public GameObject cartButtons;
    public GameObject retrieveButtons;
    public GameObject confirmButtons;
    public GameObject followButtons;
    public GameObject packingButtons;
    public GameObject ratingButtons;

    public Text dialogueText;
    public Image background;

    public Canvas UI;
    public Canvas shopUI;
    public Canvas cartUI;

    public PandaBehaviour behaviorTree;
    public NavMeshAgent shopBotAgent;

    public string currentStateName;
    private bool insideTriggerZone = false; 

    private void Start()
    {
        ResetUI();
        SetCurrentState("Idle");
    }

    private void Update()
    {
        HandleTriggerZone(); 
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

    public void PauseBotMovement(bool stopMovement)
    {
        if (shopBotAgent != null)
        {
            if (stopMovement)
            {
                //Pause movement if player is in range
                shopBotAgent.velocity = Vector3.zero;
                shopBotAgent.isStopped = true;
            }
            else
            {
                //Resume movement when player is no longer in range
                shopBotAgent.isStopped = false; 
            }
        }
    }
    private void HandleTriggerZone()
    {
        if (insideTriggerZone)
        {
            PauseBotMovement(true);

            switch (currentStateName)
            { 
                //Updates the UI according to current state name 
                //State name changes are handled in the state scripts themselves
                case "Idle":
                    ResetUI();
                    UI.gameObject.SetActive(true);
                    idleButtons.SetActive(true);
                    dialogueText.gameObject.SetActive(true);
                    background.gameObject.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case "ShowShoplist":
                    ResetUI();
                    shopUI.gameObject.SetActive(true);
                    shoplistButtons.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case "ShowCart":
                    ResetUI();
                    cartUI.gameObject.SetActive(true); 
                    cartButtons.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case "RetrieveItems":
                    ResetUI();
                    UI.gameObject.SetActive(true);
                    retrieveButtons.SetActive(true);
                    dialogueText.gameObject.SetActive(true);
                    background.gameObject.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case "ConfirmItems":
                    ResetUI();
                    UI.gameObject.SetActive(true);
                    confirmButtons.SetActive(true);
                    dialogueText.gameObject.SetActive(true);
                    background.gameObject.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case "FollowToCounter":
                    ResetUI();
                    UI.gameObject.SetActive(true);
                    followButtons.SetActive(true);
                    dialogueText.gameObject.SetActive(true);
                    background.gameObject.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case "Payment":
                    ResetUI();
                    UI.gameObject.SetActive(true);
                    dialogueText.gameObject.SetActive(true);
                    background.gameObject.SetActive(true);
                    break;
                case "PackingItems":
                    ResetUI();
                    UI.gameObject.SetActive(true);
                    packingButtons.SetActive(true);
                    dialogueText.gameObject.SetActive(true);
                    background.gameObject.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case "CollectItems":
                    ResetUI();
                    UI.gameObject.SetActive(true);
                    dialogueText.gameObject.SetActive(true);
                    background.gameObject.SetActive(true);  
                    break;
                case "Feedback":
                    ResetUI();
                    UI.gameObject.SetActive(true); 
                    ratingButtons.SetActive(true);
                    dialogueText.gameObject.SetActive(true);
                    background.gameObject.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                default:
                    ResetUI();
                    break;
            }
        }
        else
        {
            PauseBotMovement(false);
            ResetUI();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        } 
         
        //Setting up trigger zone was thanks to ChatGPT! 
        //Had difficulty setting up the trigger zone using OnTriggerEnter/Stay
    }

    //Resets the UI, used for removing previous state UI after switching to a new state
    public void ResetUI() 
    {
        idleButtons.gameObject.SetActive(false);
        shoplistButtons.gameObject.SetActive(false);
        cartButtons.gameObject.SetActive(false);
        retrieveButtons.gameObject.SetActive(false);
        confirmButtons.gameObject.SetActive(false);
        followButtons.gameObject.SetActive(false);
        packingButtons.gameObject.SetActive(false);
        ratingButtons.gameObject.SetActive(false); 

        shopUI.gameObject.SetActive(false);
        cartUI.gameObject.SetActive(false);
        UI.gameObject.SetActive(false);

        dialogueText.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
    }
     
    public void SetCurrentState(string stateName)
    {
        currentStateName = stateName;
        behaviorTree.enabled = true;
    }
}

//Reference: https://docs.unity3d.com/ScriptReference/AI.NavMeshAgent.html 