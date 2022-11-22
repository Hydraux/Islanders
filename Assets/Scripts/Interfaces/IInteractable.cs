using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IInteractable
{
    public static UnityAction OnInteractionComplete{get; set;}

    public void Interact(Interactor interactor, out bool interactSuccessful);
}
