using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTabManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button BackpackButton;
    public Button CraftingButton;
    private GameObject BackpackTab;
    private GameObject CraftingTab;
    private Vector3 BackpackTabPosition;
    private GameObject Backpack;
    private GameObject Crafting;
    void Start()
    {
        BackpackTab = GameObject.Find("Backpack Tab");
        CraftingTab = GameObject.Find("Crafting Tab");
        BackpackButton = BackpackTab.GetComponent<Button>(); 
        CraftingButton = CraftingTab.GetComponent<Button>(); 
        BackpackButton.onClick.AddListener(OpenBackpackMenu);
        CraftingButton.onClick.AddListener(OpenCraftingMenu);
        Backpack = GameObject.Find("Backpack");
        Crafting = GameObject.Find("Crafting");
        Crafting.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCraftingMenu()
    {
        if(Backpack.activeInHierarchy)
        {
            Backpack.SetActive(false);
            Crafting.SetActive(true);
            CraftingTab.transform.localPosition = new Vector3(CraftingTab.transform.localPosition.x, 280, CraftingTab.transform.localPosition.z);
            BackpackTab.transform.localPosition = new Vector3(BackpackTab.transform.localPosition.x, 256, BackpackTab.transform.localPosition.z);
        }

    }

    public void OpenBackpackMenu()
    {
        if(Crafting.activeInHierarchy)
        {
            Backpack.SetActive(true);
            Crafting.SetActive(false);

            CraftingTab.transform.localPosition = new Vector3(CraftingTab.transform.localPosition.x, 256, CraftingTab.transform.localPosition.z);
            BackpackTab.transform.localPosition = new Vector3(BackpackTab.transform.localPosition.x, 280, BackpackTab.transform.localPosition.z);

        }
    }
}
