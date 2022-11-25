using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Buildings/building")]
public class Building : MonoBehaviour
{
    public Sprite sprite;
    public int buildingID;

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
