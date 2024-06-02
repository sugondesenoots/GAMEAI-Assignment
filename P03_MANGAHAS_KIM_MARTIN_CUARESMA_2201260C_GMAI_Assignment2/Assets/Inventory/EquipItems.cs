using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItems : MonoBehaviour
{
    //These objects represent items to be equipped
    public GameObject Cash;
    public GameObject Card;

    //These bools check which items are held currently
    public bool holdCash = false;
    public bool holdCard = false;

    private void Start()
    {
        //Ensure no items are equipped at the start of the game 
        //This prevents players from holding a Cash/Card item at the start 
        //When they do, the bool's are still false which can cause issues for the states 

        Unequip();
         
        //Instructions for testers 

        Debug.Log("Press 1 to equip Cash Item");
        Debug.Log("Press 2 to equip Card Item");
        Debug.Log("Press 3 to unequip items");
    }

    void Update()
    {
        //Setting respective keybinds for the items/unequipping
        if (Input.GetKeyDown("1"))
        {
            CashSlot();
            Debug.Log("Cash Item Equipped");
        }

        if (Input.GetKeyDown("2"))
        {
            CardSlot();
            Debug.Log("Card Item Equipped");
        }

        if (Input.GetKeyDown("3"))
        {
            Unequip();
            Debug.Log("Unequipped items");
        }
    }

    //Sets the respective bools for the functions 
    //Sets respective items active for each function 
    void CashSlot()
    {
        Cash.SetActive(true);
        Card.SetActive(false);

        holdCash = true;
        holdCard = false;
    }

    void CardSlot()
    {
        Cash.SetActive(false);
        Card.SetActive(true);

        holdCard = true;
        holdCash = false;
    }

    void Unequip()
    {
        Cash.SetActive(false);
        Card.SetActive(false);

        holdCard = false;
        holdCash = false;
    }
}

//Reference: https://www.youtube.com/watch?v=RyzFwix15Dg 