using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //Singleton instance to ensure only one Inventory exists
    public static Inventory instance;

    //List to hold the items in the inventory
    public List<Item> items = new List<Item>();

    //Button to clear inventory 
    public Button clearInventoryBtn;

    private void Awake()
    {
        //Doing this ensures, that only one Inventory exists & not multiple of them
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Update()
    {
        ClearInventory();
    }

    public void AddItemToInven(Item itemToAddToInven)
    {
        bool itemExists = false;

        //Checks if the item already exists within the inventory
        foreach (Item item in items)
        {
            if (item.invenItemName == itemToAddToInven.invenItemName)
            {
                //If the item does exists, increase the count of the current item in the list
                item.invenItemCount += itemToAddToInven.invenItemCount;
                itemExists = true;
                break;
            }
        }
        //If the item doesn't exist yet, add to inventory
        if (!itemExists)
        {
            items.Add(itemToAddToInven);
        }
        Debug.Log(itemToAddToInven.invenItemCount + " " + itemToAddToInven + " Added to inventory");
    }

    public void RemoveItemFromInven(Item itemToRemove)
    {
        foreach (var item in items)
        {
            if (item.invenItemName == itemToRemove.invenItemName)
            {
                //Decreases item count == the current count of the item
                item.invenItemCount -= itemToRemove.invenItemCount; 

                //Condition checks the count of the items, if it is 0 or less, remove the item completely
                if (item.invenItemCount <= 0)
                {
                    items.Remove(itemToRemove);
                }
                Debug.Log(itemToRemove.invenItemCount + " " + itemToRemove + " Removed from inventory");
            }
        }
    }
      
    //Handles clearing inventory
    public void ClearInventory()
    {
        clearInventoryBtn.onClick.AddListener(ClearItemsInInventory);
    } 
     
    void ClearItemsInInventory()
    {
        items.Clear();

        clearInventoryBtn.onClick.RemoveAllListeners();
    }
}
