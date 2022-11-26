using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boat : Building
{
    BoxCollider2D collider;
    Transform islandScene;
    GameObject PlayerParent;

    public override void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        PlayerParent = GameObject.Find("InventoryHolder");
        base.Start();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
        Debug.Log("collided with " + other.gameObject.tag);
        
        if(other.gameObject.tag == "Player")
        {
            other.transform.position = new Vector3(-0.28f, -0.2f, 0f);
            SetSail();
        } 
            
    }


   void SetSail()
   {

        islandScene = transform.parent;        
    GameObject sailingScene = GameObject.FindGameObjectWithTag("Sail Scene");        


        foreach (Transform child in PlayerParent.transform)
        {
            child.gameObject.SetActive(false);   
        }
        foreach (Transform child in islandScene)
        {
            child.gameObject.SetActive(false);
        }

        if(sailingScene != null)
        { 
            foreach (Transform child in sailingScene.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else SceneManager.LoadScene("SailScene", LoadSceneMode.Additive);

   }
}
