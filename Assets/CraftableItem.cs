using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class recipeItem
{
    public InventoryItemData item;
    public int RequiredAmount;
}

public class CraftableItem : MonoBehaviour
{
    private PlayerInventoryHolder inventory;
    private Button button;
    public InventoryItemData itemData;
    [SerializeField] public List<recipeItem> recipe;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Craft);
        inventory = GameObject.Find("Player").GetComponent<PlayerInventoryHolder>();
    }

    void Craft()
    {
        // Check if player has required resources
        if(HasResources())
        {
            //Remove required resources
            inventory.PrimaryInventorySystem.InventorySlots;
            //Add item to inventory
            inventory.AddToInventory(itemData, 1);
        }

    }

    private bool HasResources()
    {
        foreach (recipeItem item in recipe)
        {
            // Get number of items in inventory
            int numItems = 0;
            inventory.PrimaryInventorySystem.ContainsItem(item.item, ref numItems);
            inventory.SecondaryInventorySystem.ContainsItem(item.item, ref numItems);

            if(numItems < item.RequiredAmount)
            {
                return false;
            }
        }
        return true;
    }
}
