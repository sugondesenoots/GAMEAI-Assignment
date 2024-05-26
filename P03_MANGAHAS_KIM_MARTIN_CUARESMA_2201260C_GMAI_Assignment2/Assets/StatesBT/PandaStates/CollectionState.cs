using Panda;
using UnityEngine;
using UnityEngine.UI;

public class CollectionState : MonoBehaviour
{
    public ShopBotStateManager _stateManager;
    public Button _collectBtn;

    public GameObject _objectToSpawn;
    public GameObject _counter;

    public Inventory _inventory;
    public string itemNameToCheck;

    [SerializeField] private float verticalOffset = 1f;
    private bool itemsCollected = false;
    private bool objectSpawned = false; 

    public void Initialize(ShopBotStateManager stateManager, Button collectBtn, GameObject objectToSpawn, GameObject counter, Inventory inventory)
    {
        _stateManager = stateManager;
        _collectBtn = collectBtn;

        _objectToSpawn = objectToSpawn;
        _counter = counter;
        _inventory = inventory;

        _stateManager.ResetUI();
    }

    [Task]
    public bool IsCollectionState()
    {
        return _stateManager.currentStateName == "CollectionState";
    }

    [Task]
    void CollectionDialogue()
    {
        if (!_stateManager.dialogueText.gameObject.activeSelf)
        {
            _stateManager.dialogueText.text = "I have packed your items. Please collect them.";
            _stateManager.dialogueText.gameObject.SetActive(true);
        }
        Task.current.Succeed();
    }

    [Task]
    void SpawnObjectOnCounter()
    {
        if (!objectSpawned) 
        {
            if (_objectToSpawn != null && _counter != null)
            {
                Vector3 spawnPosition = _counter.transform.position;
                spawnPosition.y += verticalOffset;
                Instantiate(_objectToSpawn, spawnPosition, Quaternion.identity);
                objectSpawned = true; 
            }
            else
            {
                Debug.LogError("Object to spawn or counter is not set.");
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
    }

    bool IsItemInInventory(string itemName)
    {
        foreach (Item item in _inventory.items)
        {
            if (item.invenItemName == itemName)
            {
                return true;
            }
        }
        return false;
    }

    [Task]
    void SwitchToFeedback()
    {
        if (itemsCollected == true)
        {
            itemsCollected = false;
            _stateManager.SetCurrentState("FeedbackState");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
}
