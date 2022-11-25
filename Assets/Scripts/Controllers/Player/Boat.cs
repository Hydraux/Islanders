using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boat : Building
{
    BoxCollider2D collider;
    GameObject sailingScene;
    GameObject islandScene;

    public override void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        islandScene = GameObject.FindGameObjectWithTag("Island Scene");        
        sailingScene = GameObject.FindGameObjectWithTag("Sail Scene");        
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
        foreach (Transform child in islandScene.transform)
        {
            child.gameObject.SetActive(false);
        }

        if(sailingScene)
        { 
            foreach (Transform child in sailingScene.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else SceneManager.LoadScene("SailScene", LoadSceneMode.Additive);

   }
}
