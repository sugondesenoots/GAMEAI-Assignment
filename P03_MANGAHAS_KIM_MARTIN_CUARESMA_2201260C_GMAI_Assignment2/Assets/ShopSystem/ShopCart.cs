using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCart : MonoBehaviour
{
    public List<ShopItems> itemsInStore = new List<ShopItems>();
    public List<ShopItems> itemsInCart = new List<ShopItems>();
    public Text cartText;

    void Start()
    {
        itemsInStore.Add(new ShopItems("Vegetables", 1));
        itemsInStore.Add(new ShopItems("Meat", 2));
        itemsInStore.Add(new ShopItems("Seasonings", 3));
    }

    public void AddItemToCart(string itemName)
    {
        ShopItems itemToAdd = itemsInStore.Find(item => item.itemName == itemName);

        if (itemToAdd != null)
        {
            ShopItems existingCartItem = itemsInCart.Find(item => item.itemName == itemName);
            if (existingCartItem != null)
            {
                existingCartItem.quantity++;
            }
            else
            {
                itemsInCart.Add(new ShopItems(itemToAdd.itemName, itemToAdd.price));
            }

            UpdateCartDisplay();
        }
    }

    public void RemoveItemFromCart(string itemName)
    {
        ShopItems itemToRemove = itemsInCart.Find(item => item.itemName == itemName);

        if (itemToRemove != null)
        {
            itemToRemove.quantity--;

            if (itemToRemove.quantity <= 0)
            {
                itemToRemove.quantity = 0;
            }

            UpdateCartDisplay();
        }
    }

    public int CalculateTotal()
    {
        int total = 0;
        foreach (ShopItems item in itemsInCart)
        {
            total += item.price * item.quantity;
        }
        return total;
    }

    public void UpdateCartDisplay()
    {
        cartText.text = "";

        foreach (ShopItems item in itemsInCart)
        {
            cartText.text += $"{item.itemName} x{item.quantity} = ${item.price * item.quantity}\n";
        }
        cartText.text += $"Total: ${CalculateTotal()}";
    }

    public void AddItemToCartOnClick(string itemName)
    {
        AddItemToCart(itemName);
    }

    public void RemoveItemFromCartOnClick(string itemName)
    {
        RemoveItemFromCart(itemName);
    }
}