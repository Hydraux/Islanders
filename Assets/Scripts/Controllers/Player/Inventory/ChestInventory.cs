using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChestInventory : InventoryHolder, IInteractable
{
    public static UnityAction OnInteractionComplete {get; set;}

    void IInteractable.Interact(Interactor interactor, out bool interactSuccessful)
    {
        OnDynamicInventoryDisplayRequested?.Invoke(primaryInventorySystem);
        interactSuccessful = true;
    }

    
}
