using Panda;
using UnityEngine;
using UnityEngine.UI;

public class CollectionState : MonoBehaviour
{
    public ShopBotStateManager _stateManager;
    public ShopCart _shopCart;

    public GameObject _objectToSpawn;
    public GameObject _counter; 

    public Inventory _inventory;
    public string itemNameToCheck;

    [SerializeField] private float verticalOffset = 1f; 

    private bool itemsCollected = false;
    private bool objectSpawned = false; 

    public void Initialize(ShopBotStateManager stateManager, ShopCart shopCart, GameObject objectToSpawn, GameObject counter, Inventory inventory)
    {
        _stateManager = stateManager; 
        _shopCart = shopCart;

        _objectToSpawn = objectToSpawn;
        _counter = counter;
        _inventory = inventory;

        _stateManager.ResetUI();

        itemsCollected = false; 
        objectSpawned = false; 
         
        //Bools should be called at Initialize too
        //This fixes any issue related to skipping states due to bool conditions
    }

    [Task]
    public bool IsCollectionState()
    {
        return _stateManager.currentStateName == "CollectionState";

        //Follows same logic as previous states (IdleState, etc.)
    }

    [Task]
    void CollectionDialogue()
    {
        _stateManager.dialogueText.text = "I have packed your items. Please collect them. (Press E)";
        _stateManager.dialogueText.gameObject.SetActive(true); 

        Task.current.Succeed();

        //Follows same logic as previous states (IdleState, etc.)
    }

    [Task]
    void SpawnObjectOnCounter()
    {
        if (!objectSpawned) 
        {
            //Spawns Groceries object if not already spawned
            //Also checks if the Groceries object and counter are not null 

            if (_objectToSpawn != null && _counter != null)
            {
                //Calculates the spawn position to be above the counter
                Vector3 spawnPosition = _counter.transform.position;
                spawnPosition.y += verticalOffset;

                //Instantiates the Groceries object
                Instantiate(_objectToSpawn, spawnPosition, Quaternion.identity);
                objectSpawned = true; 
            }
            else
            {
                Debug.LogError("Object to spawn or counter is not set."); //Checks if there is a null reference error
            }
        }
        Task.current.Succeed(); 
    }

    [Task]
    void WaitForItemCollection()
    {
        if (IsItemInInventory(itemNameToCheck))
        {
            itemsCollected = true;
            Task.current.Succeed();
        }
        else
        {
            itemsCollected = false;
            Task.current.Fail();
        }

        //This checks if a certain items is in your inventory... 
        //If it is, it sets itemsCollected to true and switches states  

        //If you realize that there is skipping of states from Packing to Feedback 
        //This is due to your inventory still having the item after the first time round 
        //In order to avoid this, please reset your inventory first
    }

    bool IsItemInInventory(string itemName) //Checks if the specific item is in the inventory
    {
        foreach (Item item in _inventory.items)
        {
            if (item.invenItemName == itemName)
            {
                return true; //If in inventory, return true
            }
        }
        return false; //If not in inventory, return false
    }

    [Task]
    void SwitchToFeedback()
    {
        if (itemsCollected)
        {
            itemsCollected = false;
            objectSpawned = false;

            _shopCart.itemsInCart.Clear();
            _shopCart.UpdateCartDisplay();
             
            _stateManager.SetCurrentState("FeedbackState");

            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }

        //Follows same logic as previous states (IdleState, etc.)
    }
}
