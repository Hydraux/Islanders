using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;


public class Building : MonoBehaviour
{
    public Sprite sprite;
    public GameObject prefab;
    public int buildingID;
    public bool isObject;

    public virtual void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;
    }

    void SetSail()
    {
        Debug.Log("Set sail");
    }

}
