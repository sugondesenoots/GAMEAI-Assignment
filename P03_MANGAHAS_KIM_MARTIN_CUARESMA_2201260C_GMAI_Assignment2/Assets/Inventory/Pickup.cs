using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Item item = new Item("Item Name", 1);
    private bool isPlayerInRange = false;

    void Update()
    {  
        //Put in update so that it checks each frame whether the E is pressed, ensures no input delay when trying to pick up the item
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PickUpItem(); 
        }
    }
     
    //Both trigger functions check if player is within range to pick up item or not
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; 
        }
    }

    private void PickUpItem()
    {
        Inventory.instance.AddItemToInven(item); //Adds the item to the inventory list
        Destroy(gameObject); //Removes item from scene when added to inventory
    }
}
