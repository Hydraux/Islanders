using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ItemPickup : MonoBehaviour
{
    
    public float PickUpRadius = 0.1f;
    public InventoryItemData ItemData;

    private CircleCollider2D myCollider;
    private SpriteRenderer spriteRenderer;

    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = ItemData.Icon;
        myCollider = GetComponent<CircleCollider2D>();
        myCollider.isTrigger = true;
        myCollider.radius = PickUpRadius;
    }    

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collided with " + ItemData.DisplayName);
        var inventory = other.transform.parent.GetComponent<PlayerInventoryHolder>();

        if(!inventory) return;

        if(inventory.AddToInventory(ItemData, 1))
        {
            Destroy(this.gameObject);
        }
        
    }

    
}
