using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItems : MonoBehaviour
{

    public GameObject Cash;
    public GameObject Card;

    public bool holdCash = false; 
    public bool holdCard = false;

    private void Start()
    {
        Unequip();
    }

    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            Equip1();
            Debug.Log("Cash Item Equipped"); 
        }

        if (Input.GetKeyDown("2"))
        {
            Equip2();
            Debug.Log("Card Item Equipped"); 
        }

        if (Input.GetKeyDown("3"))
        {
            Unequip();
            Debug.Log("Unequipped items");
        }
    }

    void Equip1()
    {
        Cash.SetActive(true);
        Card.SetActive(false); 

        holdCash = true;
        holdCard = false;
    }

    void Equip2()
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