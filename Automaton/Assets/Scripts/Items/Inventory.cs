using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<string> itemsInInventory;
    public GameObject InventoryCanvas;

    public float iconSpacing;

    private PartSprites partSpriteSO;
    private bool inventoryActive = false;

    private GameObject currentInventory;

    // Start is called before the first frame update
    void Start()
    {   
        partSpriteSO = GameObject.Find("ItemManager").GetComponent<ItemManager>().partSpriteListScriptableObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            OpenInventoryUI();
        }

        if (Input.GetKeyDown("l"))
        {
            CreateInventoryUI();
        }
    }

    public void OpenInventoryUI()
    {
        if(currentInventory != null)
        {
            currentInventory.SetActive(!currentInventory.activeSelf);
        }
    }

    public void CreateInventoryUI()
    {
        SortInventoryItems();
        
        if(currentInventory != null)
        {
            Destroy(currentInventory);
        }    
        
        currentInventory = Instantiate(InventoryCanvas);
            InventoryCanvas invCanvScr = currentInventory.GetComponent<InventoryCanvas>();

            float partXMovement = 0;
            float partYMovement = 0;

            for (int i = 0; i < itemsInInventory.Count; i++)
            {
                if (itemsInInventory[i][0].ToString() == "1")
                {
                    if (partXMovement >= iconSpacing * 5)
                    {
                        partXMovement = 0;
                        partYMovement -= iconSpacing;
                    }

                    Vector3 startPos = invCanvScr.partItemHolder.transform.localPosition;

                    GameObject newPartInventoryItem = Instantiate(invCanvScr.partItemHolder, transform);
                    newPartInventoryItem.transform.SetParent(invCanvScr.partItemHolder.transform.parent);
                    newPartInventoryItem.transform.localPosition = new Vector3(startPos.x + partXMovement, startPos.y + partYMovement, startPos.z);
                    newPartInventoryItem.transform.localScale = invCanvScr.partItemHolder.transform.localScale;

                    newPartInventoryItem.transform.Find("ItemSprite").gameObject.GetComponent<Image>().sprite = StaticItemIDHelper.SpriteFinder(partSpriteSO, itemsInInventory[i]);

                    partXMovement += iconSpacing;
                }

                inventoryActive = true;
            }

        Destroy(invCanvScr.partItemHolder);
        currentInventory.SetActive(false);
    }

    public void SortInventoryItems()
    {

    }
}
