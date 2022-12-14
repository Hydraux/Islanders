using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private InventoryItemData itemData;
    [SerializeField] private int stackSize;

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlot(InventoryItemData source, int amount)
    {
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot(){
        
    }

    public void ClearSlot(){
        itemData = null;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot)
    {
        if(itemData == invSlot.itemData) AddToStack(invSlot.stackSize);
        else
        {
            itemData = invSlot.itemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = ItemData.MaxStackSize - stackSize;
        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        if(stackSize + amountToAdd <= itemData.MaxStackSize) return true;
        else return false;
    }

    public void AddToStack(int amount)
    {
        stackSize+=amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }

    internal void UpdateInventorySlot(InventoryItemData item, int amount)
    {
        itemData = item;
        stackSize = amount;
    }

    public bool SplitStack(out InventorySlot splitStack)
    {
        if(stackSize <= 1)
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(stackSize/2);
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(itemData, halfStack);
        return true;
    }
}
