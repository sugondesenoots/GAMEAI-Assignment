using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<Item> items = new List<Item>();

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void AddItemToInven(Item itemToAddToInven)
    {
        bool itemExists = false ; 

        foreach (Item item in items)
        {
            if(item.invenItemName == itemToAddToInven.invenItemName)  
            {
                item.invenItemCount += itemToAddToInven.invenItemCount; 
                itemExists = true; 
                break;
            }
        } 
        if(!itemExists)
        {
            items.Add(itemToAddToInven);
        }
        Debug.Log(itemToAddToInven.invenItemCount + " " + itemToAddToInven + "Added to inven");
    }

    public void RemoveItemFromInven(Item itemToRemove) 
    {
        foreach(var item in items)
        {
            if(item.invenItemName == itemToRemove.invenItemName)
            {
                item.invenItemCount -= itemToRemove.invenItemCount; 
                if(item.invenItemCount <= 0)
                {
                    items.Remove(itemToRemove);
                }
                Debug.Log(itemToRemove.invenItemCount + " " + itemToRemove + "Removed to inven");
            }
        }
    }
}
