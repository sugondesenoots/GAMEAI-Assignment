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
     
    //The above code follows the same logic as my Inventory script

    public int CalculateTotal()
    {
        int total = 0; //Resets total cost of items before calculation to ensure accuracy

        //Go through each item in the cart list & calculate the total price of the items
        foreach (ShopItems item in itemsInCart)
        {
            total += item.price * item.quantity;
        }
        return total;
    }

    public void UpdateCartDisplay()
    {
        cartText.text = ""; //Resets cart display before updating

        //Go through each item in the cart list & add the item details to cart display
        foreach (ShopItems item in itemsInCart)
        {
            cartText.text += $"{item.itemName} x{item.quantity} = ${item.price * item.quantity}\n"; //Formats the item details to be shown in the cart display
        }
        cartText.text += $"Total: ${CalculateTotal()}"; //Updates the cart display to show the total cost of the items
    }
     
    //Calls the functions accordingly when clicking on the buttons in the ShoplistState
    //Adds vegetables, etc. to cart list when the respective buttons are clicked & vice versa

    public void AddItemToCartOnClick(string itemName)
    {
        AddItemToCart(itemName);
    }

    public void RemoveItemFromCartOnClick(string itemName)
    {
        RemoveItemFromCart(itemName);
    }
}