using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventoryHolder : InventoryHolder
{
    [SerializeField] protected int secondaryInventorySize;
    [SerializeField] protected InventorySystem secondaryInventorySystem;

    public InventorySystem SecondaryInventorySystem => secondaryInventorySystem;

    public static UnityAction<InventorySystem> OnPlayerBackpackDisplayRequested;

    private static PlayerInventoryHolder inventoryHolder;
    public Vector3 SpawnPoint;


    protected override void Awake()
    {
        base.Awake();
        secondaryInventorySystem = new InventorySystem(secondaryInventorySize);
        DontDestroyOnLoad(gameObject);

        if(inventoryHolder == null)
        {
            inventoryHolder = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Tab)) OnPlayerBackpackDisplayRequested?.Invoke(secondaryInventorySystem);
    }

    public bool AddToInventory(InventoryItemData data, int amount)
    {
        if(primaryInventorySystem.AddToInventory(data, amount))
        {
            return true;
        }
        else if(secondaryInventorySystem.AddToInventory(data, amount))
        {
            return true;
        }

        return false;
    }

    public void RemoveResources(List<recipeItem> recipe){
        foreach (recipeItem item in recipe)
            {
                Debug.Log("Removing from primary inventory");
                Debug.Log("required amount: " + item.RequiredAmount);
                int numToAdd = primaryInventorySystem.RemoveResources(item, item.RequiredAmount);
                

                if(numToAdd > 0)
                {
                    Debug.Log("remaining: " + numToAdd);
                    secondaryInventorySystem.RemoveResources(item, numToAdd);    
                } 
            }
    }

    void Spawn()
    {
        transform.position = SpawnPoint;
    }
}
