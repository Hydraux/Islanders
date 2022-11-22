using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Linq;

public class BuildingManager : MonoBehaviour
{
    private Vector2 mousePosition;
    public Building[] buildings;

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    public void placeBuilding(MouseItemData mouseItemData)
    {
        InventoryItemData buildingItemData = mouseItemData.AssignedInventorySlot.ItemData;
       
       Building building = buildings.FirstOrDefault(building => building.buildingID == buildingItemData.ID);


        if(building != null)
        {
            building.sprite = buildingItemData.Icon;
            GameObject walls = GameObject.FindGameObjectsWithTag("Walls")[0];
            Tilemap TileMap = walls.GetComponent<Tilemap>();
            Tile tile = ScriptableObject.CreateInstance<Tile>();
            Vector3Int nearestTile = TileMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            
            tile.sprite = building.sprite;
            tile.colliderType = Tile.ColliderType.None;

            TileMap.SetTile(nearestTile, tile );
        }
    }

}
