using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour, IInteractable
{
    public int maxHealth;
    public int health;
    public GameObject resource;
    public int numItemsDropped;
    
    public AnimationBehavior toolAnimation;

    void Awake()
    {
        health = maxHealth;
        
    }
    void IInteractable.Interact(Interactor interactor, out bool interactSuccessful)
    {
        health--;
        Debug.Log(health.ToString());
        if(health <= 0)
        {
            Instantiate(resource, position: gameObject.transform.position, new Quaternion());
            Destroy(gameObject);
        }

        interactSuccessful = true;
    }
}
