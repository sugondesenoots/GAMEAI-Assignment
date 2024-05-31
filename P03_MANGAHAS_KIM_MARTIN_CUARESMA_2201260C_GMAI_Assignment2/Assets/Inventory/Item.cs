[System.Serializable] 
public class Item
{
    public string invenItemName;
    public int invenItemCount;

    //Initializes the item with a name & count
    public Item(string itemName, int itemCount)
    {
        invenItemName = itemName;
        invenItemCount = itemCount;
    }

    //[System.Serializable] ensures the Item class is serializable
    //This is so it can be saved & loaded later on
}
