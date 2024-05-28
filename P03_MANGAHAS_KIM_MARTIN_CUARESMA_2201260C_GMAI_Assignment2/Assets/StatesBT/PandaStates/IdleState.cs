using Panda;
using UnityEngine;
using UnityEngine.UI;

public class IdleState : MonoBehaviour
{
    public ShopBotStateManager _stateManager;
    public Button _interactBtn;

    private bool interactClicked = false;

    public void Initialize(ShopBotStateManager stateManager, Button interactBtn)
    {
        _stateManager = stateManager;
        _interactBtn = interactBtn;

        _stateManager.ResetUI();

        //Initializes all the variables that I am using 
    }

    [Task]
    public bool IsIdleState()
    {
        return _stateManager.currentStateName == "IdleState"; 

        //Updates the current state to enable UI in the state manager 
        //Check state manager to see what I mean 
    }

    [Task]
    void IdleDialogue()
    {
        _stateManager.dialogueText.text = "Press 'Interact' to start shopping!";
        Task.current.Succeed();

        //Updates dialogue for current state 
    }

    [Task]
    void WaitForInteractClick()
    {
        _interactBtn.onClick.AddListener(OnClick);

        if (interactClicked)
        {
            interactClicked = true;
            Task.current.Succeed();

            //Set interactClicked to true if interact button is clicked on
        }
    }

    void OnClick()
    {
        interactClicked = true;
    }

    [Task]
    void SwitchToShoplist()
    {
        if (interactClicked)
        {
            interactClicked = false;
            _stateManager.SetCurrentState("ShoplistState");
            Task.current.Succeed(); 
             
            //Switch to next state 
            //Reset interactClicked
        }
        else
        {
            Task.current.Fail();
        }
    } 
     
    //I won't comment similar tasks for other states
}
