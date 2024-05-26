[System.Serializable]

public class Item
{
    public string invenItemName;
    public int invenItemCount; 
     
    public Item(string itemName, int itemCount)
    {
        invenItemName = itemName;
        invenItemCount = itemCount;
    }
}
