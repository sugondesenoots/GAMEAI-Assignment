using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Item item = new Item("Item Name", 1);

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Inventory.instance.AddItemToInven(item);
                Destroy(gameObject);
            }
        }
    }
}
