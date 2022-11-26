using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class MouseItemData : MonoBehaviour
{
    public BuildingManager buildingManager;
    public Image ItemSprite;
    public TextMeshProUGUI ItemCount;
    public InventorySlot AssignedInventorySlot;    
    [SerializeField] private GameObject validBoatTiles;
    [SerializeField] private GameObject validBridgeTiles;


    private void Awake()
    {
        ItemSprite.color = Color.clear;
        ItemCount.text = "";
    }

    public void UpdateMouseSlot(InventorySlot invSlot)
    {
        AssignedInventorySlot.AssignItem(invSlot);
        ItemSprite.sprite = invSlot.ItemData.Icon;
        ItemCount.text = invSlot.StackSize.ToString();
        ItemSprite.color = Color.white;
    }

    private void Update()
    {
        if(AssignedInventorySlot.ItemData != null)
        {
            if(AssignedInventorySlot.ItemData.DisplayName == "Bridge")
            {
                validBridgeTiles.SetActive(true);

            }
            if(AssignedInventorySlot.ItemData.DisplayName == "Boat")
            {
                validBoatTiles.SetActive(true);
            }
            transform.position = Input.mousePosition;

            if(Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject())
            {
                if(buildingManager.placeBuilding(this))
                ClearSlot();
            }

        }
        else if(validBoatTiles.activeInHierarchy || validBridgeTiles.activeInHierarchy)
        {
            validBoatTiles.SetActive(false);
            validBridgeTiles.SetActive(false);
        }
    }

    public void ClearSlot()
    {
        AssignedInventorySlot.ClearSlot();
        ItemCount.text = "";
        ItemSprite.color = Color.clear;
        ItemSprite.sprite = null;
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
