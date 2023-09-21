using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemID : MonoBehaviour
{
    public string ID;

    private PartSprites partSpriteSO;

    public void StartItemCreation()
    {
        

        if (ID.Length == 5)
        {
            if (ID[0].ToString() == "1")
            {
                gameObject.name = "Part";
                Part partScript = this.gameObject.AddComponent<Part>();

                PartSetup(partScript);
            }
            else if (ID[0].ToString() == "2")
            {
                gameObject.name = "Rune";
                Rune runeScript = this.gameObject.AddComponent<Rune>();

                RuneSetup(runeScript);
            }
        }
        else
        {
            print("broken ID number");
        }
    }

    public void PartSetup(Part partScript)
    {
        partScript.sprite = StaticItemIDHelper.SpriteFinder(partSpriteSO, ID);

        int.TryParse(ID[4].ToString(), out int runeSlots);

        partScript.attachmentSlots = runeSlots;
    }

    public void RuneSetup(Rune runeScript)
    {

    }
}
