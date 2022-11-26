using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Animator anim;
    public bool animationFinished {get; set;}

    Vector2 movementInput;
    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;
    Interactor interactor;
    GameObject islandScene;
    GameObject sailingScene;
    GameObject playerParent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
        boxCollider2D = GetComponent<BoxCollider2D>();
        interactor = GetComponent<Interactor>();
        playerParent = GameObject.Find("InventoryHolder");


    }

    // Update is called once per frame
    void Update()
    {
        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        
    }

    private void FixedUpdate(){
        moveCharacter(movementInput);

    }

    private void moveCharacter(Vector2 movement)
    {
        if(movement != Vector2.zero)
        {
            anim.SetBool("moving", true);
            anim.SetFloat("moveX", movement.x);
            anim.SetFloat("moveY", movement.y);
            rb.velocity = movement * moveSpeed * Time.fixedDeltaTime;
        }
        else
        {
            anim.SetBool("moving", false);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collided with tag: " + other.gameObject.tag);
        Debug.Log("Collided with name: " + other.gameObject.name);
        if(other.gameObject.tag == "Island")
        {
            sailingScene = GameObject.FindGameObjectWithTag("Sail Scene");
            foreach (Transform child in playerParent.transform)
            {
             child.gameObject.SetActive(true);   
            }
            foreach (Transform child in sailingScene.transform)
            {
                child.gameObject.SetActive(false);
            }   
            if(other.gameObject.name == "Island (1)")
            {

                islandScene = GameObject.FindGameObjectWithTag("Island Scene");


            }
            else if (other.gameObject.name == "Island")
            {
                islandScene = GameObject.FindGameObjectWithTag("Island2 Scene");

            }
                foreach (Transform child in islandScene.transform)
                {
                    child.gameObject.SetActive(true);
                } 
                GameObject inventoryHolder = GameObject.Find("InventoryHolder");
                
                inventoryHolder.SetActive(true);
            transform.position = other.transform.position + new Vector3(0,-0.5f,0);  
        }
    }


    

}
