using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay chestPanel;
    public StaticInventoryDisplay playerBackpackPanel;
    private bool isBackpackOpen;

    private static InventoryUIController inventoryUIController;

    private void Awake()
    {
        chestPanel.gameObject.SetActive(false);
        playerBackpackPanel.gameObject.SetActive(false);
        isBackpackOpen = false;
        DontDestroyOnLoad(gameObject);

        if(inventoryUIController == null)
        {
            inventoryUIController = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }
    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested += DisplayPlayerBackpack;
    }

    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested -= DisplayPlayerBackpack;

    }

    // Update is called once per frame
    void Update()
    {

        if(chestPanel.gameObject.activeInHierarchy && (Input.GetKey(KeyCode.Escape)))
        {
            chestPanel.gameObject.SetActive(false);
        }
    }

    void DisplayInventory(InventorySystem invToDisplay)
    {
        chestPanel.gameObject.SetActive(true);
        chestPanel.RefreshDynamicInventory(invToDisplay);

    }

    void DisplayPlayerBackpack(InventorySystem invToDisplay)
    {
        if(playerBackpackPanel.gameObject.activeInHierarchy == false)
        {
            playerBackpackPanel.gameObject.SetActive(true);
        }
        else
        {
            playerBackpackPanel.gameObject.SetActive(false);
        }
    }
}
