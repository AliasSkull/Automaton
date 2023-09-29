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
        if (currentInventory != null)
        {
            currentInventory.SetActive(!currentInventory.activeSelf);
        }
    }

    public void CreateInventoryUI()
    {
        SortInventoryItems();

        if (currentInventory != null)
        {
            Destroy(currentInventory);
        }

        currentInventory = Instantiate(InventoryCanvas);
        InventoryCanvas invCanvScr = currentInventory.GetComponent<InventoryCanvas>();

        float partXMovement = 0;
        float partYMovement = 0;
        float runeXMovement = 0;
        float runeYMovement = 0;

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
            else if (itemsInInventory[i][0].ToString() == "2")
            {
                if (runeXMovement >= iconSpacing * 5)
                {
                    runeXMovement = 0;
                    runeYMovement -= iconSpacing;
                }

                print(runeXMovement);

                Vector3 startPos = invCanvScr.runesItemHolder.transform.localPosition;
                GameObject newRuneInventoryItem = Instantiate(invCanvScr.runesItemHolder, transform);
                newRuneInventoryItem.transform.SetParent(invCanvScr.runesItemHolder.transform.parent);
                newRuneInventoryItem.transform.localPosition = new Vector3(startPos.x + runeXMovement, startPos.y + runeYMovement, startPos.z);
                newRuneInventoryItem.transform.localScale = invCanvScr.runesItemHolder.transform.localScale;

                //newPartInventoryItem.transform.Find("ItemSprite").gameObject.GetComponent<Image>().sprite = StaticItemIDHelper.SpriteFinder(partSpriteSO, itemsInInventory[i]);

                runeXMovement += iconSpacing;
            }

        }



        Destroy(invCanvScr.partItemHolder);
        currentInventory.SetActive(false);
    }

    public void SortInventoryItems()
    {

    }
}
