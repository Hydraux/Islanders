using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform InteractionPoint;
    public AnimationBehavior behavior;

    private PlayerController playerController;

    public DynamicInventoryDisplay inventoryPanel;
    public LayerMask InteractionLayer;
    public float InteractionPointRadius = 1f;
    public bool IsInteracting{get; private set;}
    public bool finished;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(InteractionPoint.position, InteractionPointRadius, InteractionLayer);


        if(Input.GetKeyDown(KeyCode.E) && collider != null)
        {
            var interactable = collider.GetComponent<IInteractable>();
            if(collider.gameObject.tag == "Tree")
            {
                // Do axe animation
                playerController.anim.SetTrigger("useTool");
                StartCoroutine(WaitForAnimationComplete(interactable));
                
            }
            else if(interactable != null)
            {
                StartInteraction(interactable);
            }
        }

        if(collider == null)
        {
            inventoryPanel.gameObject.SetActive(false);
        }
    }

    void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool interactSuccessful);
        IsInteracting = true;
    

    }

    IEnumerator WaitForAnimationComplete(IInteractable interactable)
    {
        yield return new WaitUntil(()=> finished == true);
        StartInteraction(interactable);
        finished = false;
    }

    public void toggleFinished()
    {
        finished = true;
        Debug.Log("test");
    }

}
