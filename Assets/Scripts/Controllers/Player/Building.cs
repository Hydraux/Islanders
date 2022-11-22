using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buildings/building")]
public class Building : MonoBehaviour
{
    public Sprite sprite;
    public int buildingID;

    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;
    }

}
