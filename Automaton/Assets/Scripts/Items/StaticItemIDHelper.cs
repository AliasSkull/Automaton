using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticItemIDHelper
{
    public static Sprite SpriteFinder(PartSprites spriteList, string ID)
    {
        Sprite sprite = null;

        int.TryParse(ID[2].ToString() + ID[3].ToString(), out int partListIndex);

        switch (ID[1].ToString())
        {
            case "1":
                sprite = spriteList.headSprites[partListIndex]; //head
                break;
            case "2":
                sprite = spriteList.bodySprites[partListIndex]; //body
                break;
            case "3":
                sprite = spriteList.ArmLSprites[partListIndex]; //Left Arm
                break;
            case "4":
                sprite = spriteList.Elements[partListIndex]; //Right Arm
                break;
        }

        return sprite;
    }
}
