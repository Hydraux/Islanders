using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipItem
{
    public InventoryItemData item;
    public int numRequired;
}


public class Escape : MonoBehaviour
{
    [SerializeField] private ShipItem[] shipItems;
    [SerializeField] private PlayerInventoryHolder playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(hasShipItems())
            {
                Debug.Log("You win");
            }

        }
    }


    bool hasShipItems()
    {
        GameObject player = GameObject.Find("InventoryHolder");
        playerInventory = player.GetComponent<PlayerInventoryHolder>();
        foreach (ShipItem item in shipItems)
        {
            if(playerInventory.PrimaryInventorySystem.ContainsItem(item.item, ref item.numRequired) == false && playerInventory.SecondaryInventorySystem.ContainsItem(item.item, ref item.numRequired) == false)
            {
                return false;
            }
        }
        return true;
    }
}
