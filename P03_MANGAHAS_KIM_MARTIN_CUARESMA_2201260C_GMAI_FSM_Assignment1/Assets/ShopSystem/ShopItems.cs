using UnityEngine;

[System.Serializable]
public class ShopItems
{
    public string itemName;
    public int price;
    public int quantity;

    public ShopItems(string name, int price, int quantity = 1)
    {
        this.itemName = name;
        this.price = price;
        this.quantity = quantity;
    } 
}
