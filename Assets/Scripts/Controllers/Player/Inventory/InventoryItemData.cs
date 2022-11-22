// Made with help from Dan Pos https://www.youtube.com/watch?v=svoXugGLFwU&list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24&index=2

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public int ID;
    public string DisplayName;
    
    [TextArea(4,4)]
    public string Description;
    public Sprite Icon;
    public int MaxStackSize;
}
