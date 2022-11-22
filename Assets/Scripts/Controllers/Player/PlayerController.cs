using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Animator anim;
    public bool animationFinished {get; set;}

    Vector2 movementInput;
    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;
    Interactor interactor;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
        boxCollider2D = GetComponent<BoxCollider2D>();
        interactor = GetComponent<Interactor>();

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
        Debug.Log("collision");
    }

    

}
