using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;


    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAddd)
    {
        if(ContainsItem(itemToAdd, out List<InventorySlot> slots))
        {
            InventorySlot slot = slots.FirstOrDefault(slot => slot.RoomLeftInStack(amountToAddd) == true);

            if(slot != null)
            {
                slot.AddToStack(amountToAddd);
                OnInventorySlotChanged?.Invoke(slot);
                return true;
            }
        }
        
        if(HasFreeSlot(out InventorySlot freeSlot))
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAddd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }
        return false;
    }

    private bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = inventorySlots.FirstOrDefault(slot => slot.ItemData == null);
        return freeSlot == null ? false : true;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, ref int numItems)
    {
        List<InventorySlot> slots = InventorySlots.Where(item => item.ItemData == itemToAdd).ToList();
        foreach (InventorySlot slot in slots)
        {
            numItems += slot.StackSize;
        }
        return slots.Count > 0 ? true : false;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> slots)
    {
        slots = InventorySlots.Where(item => item.ItemData == itemToAdd).ToList();
        return slots.Count > 0 ? true : false;
    }

    public int RemoveResources(recipeItem item, int numToAdd)
    {
        
        foreach (InventorySlot slot in InventorySlots)
        {
            if(slot.ItemData == item.item && numToAdd > 0)
            {
                if(slot.StackSize <= numToAdd)
                {   
                    numToAdd -= slot.StackSize;
                    slot.ClearSlot();
                    Debug.Log("Cleared slot");
                }
                else
                {
                    Debug.Log("Removed item from stack");
                    slot.RemoveFromStack(numToAdd);
                    numToAdd = 0;
                }
                    OnInventorySlotChanged?.Invoke(slot);

            }
        }
        return numToAdd;
    }

    

}
