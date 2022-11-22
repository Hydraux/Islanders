using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;

    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;
    
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start(){}

    public abstract void AssignSlot(InventorySystem invToDisplay);

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if (slot.Value == updatedSlot) // Slot value - the "under the hood" inventory slot
            {
                slot.Key.UpdateUISlot(updatedSlot); // Slot key - the UI representation of the value
            }
        }
    }

    public void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift);

        // Clicked slot has an item - mouse doesn't have an item - pick up that item

        if(clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData == null)
        {
            // if the player is h olding shift? split the stack
            if(isShiftPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot))
            {
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();
                return;
            }
            else
            {
                mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                clickedUISlot.ClearSlot();
                return;
            }

        }

        // Clicked slot doesnt' have an item - Mouse does have an item - place the mouse item into the empty slot
        if (clickedUISlot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            mouseInventoryItem.ClearSlot();
            return;
        }

        // Both slots have an item - decide what to do
        if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        {
            bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData == mouseInventoryItem.AssignedInventorySlot.ItemData;
            bool isRoomInStack = clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize, out int leftInStack);

            // If the items are different then swap them.
            if(!isSameItem)
            {
                SwapSlots(clickedUISlot);
                return;
            }


            // if both items are the same. combine them
            if(isSameItem && isRoomInStack)
            {
                // Is the slot stack size + mouse stack size > the slot max stack size? if so take from mouse.
                clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
                clickedUISlot.UpdateUISlot();

                mouseInventoryItem.ClearSlot();
                return;

            }
            else if(isSameItem && !isRoomInStack)
            {
                if(leftInStack < 1) SwapSlots(clickedUISlot); // Stack is full so swap the items
                else // Slot is not at max, take remainder from mouse inventory
                {
                    int remainingOnMouse = mouseInventoryItem.AssignedInventorySlot.StackSize - leftInStack;
                    clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);

                    var newItem = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData, remainingOnMouse);
                    mouseInventoryItem.ClearSlot();
                    mouseInventoryItem.UpdateMouseSlot(newItem);
                    clickedUISlot.UpdateUISlot();
                    return;
                }
            }


        }
    }

    private void SwapSlots(InventorySlot_UI clickedSlot)
    {
        InventorySlot temp = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData, mouseInventoryItem.AssignedInventorySlot.StackSize);

        mouseInventoryItem.ClearSlot();
        mouseInventoryItem.UpdateMouseSlot(clickedSlot.AssignedInventorySlot);

        clickedSlot.ClearSlot();
        clickedSlot.AssignedInventorySlot.AssignItem(temp);
        clickedSlot.UpdateUISlot();
    }
}
