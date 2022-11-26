using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Linq;

[System.Serializable]
public class SavedTile
{
    public Sprite sprite;
    public Vector3Int pos;
}

public class BuildingManager : MonoBehaviour
{
    private Vector2 mousePosition;
    public Building[] buildings;
    [SerializeField] private GameObject validBoatTiles;
    [SerializeField] private GameObject validBridgeTiles;

    // Key value pair of position and sprite for tiles that the player built over
    public Dictionary<Vector3Int, Sprite> savedTiles = new Dictionary<Vector3Int, Sprite>();
    public SavedTile savedTile;

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    public bool placeBuilding(MouseItemData mouseItemData)
    {
        InventoryItemData buildingItemData = mouseItemData.AssignedInventorySlot.ItemData;
       
       Building building = buildings.FirstOrDefault(building => building.buildingID == buildingItemData.ID);


        if(building != null)
        {
                
            GameObject walls = GameObject.Find("Walls");
            Tilemap WallTileMap = walls.GetComponent<Tilemap>();
            Tilemap boatTileMap = validBoatTiles.GetComponent<Tilemap>();
            Tilemap bridgeTileMap = validBridgeTiles.GetComponent<Tilemap>();
            Vector3Int nearestTile = WallTileMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if(building.isObject == false && bridgeTileMap.GetTile(nearestTile) != null)
            {
                Tile tile = ScriptableObject.CreateInstance<Tile>();

                // Save the tile for when the player picks up the building
                TileBase wallTile = WallTileMap.GetTile(nearestTile);
                Sprite sprite = WallTileMap.GetSprite(nearestTile);
                
                savedTiles.Add(nearestTile, sprite);
                savedTile.pos = nearestTile;
                savedTile.sprite = sprite;
                tile.sprite = building.sprite;
                tile.colliderType = Tile.ColliderType.None;
                WallTileMap.SetTile(nearestTile, tile);
                return true;
            }
            else if(boatTileMap.GetTile(nearestTile) != null)
            {
                GameObject parent = GameObject.Find("SceneParent");
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                worldPoint.z = 0;
               Instantiate(building.prefab, worldPoint, Quaternion.identity, parent.transform);
               return true;
            }
            return false;
        }
        else
        {
            Debug.Log("No building associated with item");
            return false;
        }
    }

}
