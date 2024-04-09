using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetStartValue : MonoBehaviour
{
    public int leftStartElement;
    public int rightStartElement;
    
    public OpenRuneMenu _orm;
    public RuneChoser lRC;
    public RuneChoser rRC;
    public PlayerAimer _pa;
    public Image cooldownUIL;
    public Image cooldownUIR;
    public List<Sprite> icons;
    public List<Sprite> runes;

    public List<GameObject> ObjsToHideCheckpoint1;
    public List<GameObject> ObjsToHideCheckpoint2;
    public List<GameObject> ObjsToHideCheckpoint3;
    public EnemySpawningManager _esm;
    public LevelManager _lm;

    public Transform checkpoint1SpawnLocation;
    public Transform checkpoint2SpawnLocation;
    public Transform checkpoint3SpawnLocation;

    public Transform player;

    private void Awake()
    {
        _pa.EID = GameObject.Find("RuneManager").GetComponent<ElementManager>().publicAccessElementDatabase;

        if(StaticValues.startSpellL == 0 && StaticValues.startSpellR == 0)
        {
            StaticValues.startSpellL = leftStartElement;
            StaticValues.startSpellR = rightStartElement;
        }

        SetStartElement();
        SetSpawnPoint();
    }

    // Start is called before the first frame update
    void Start()
    {
        StaticValues.flameLevel = 0;
        StaticValues.score = 0;

        StaticValues.lDamage = 0;
        StaticValues.lDamageCost = 300;
        StaticValues.rDamage = 0;
        StaticValues.rDamageCost = 300;
        StaticValues.lSize = 0;
        StaticValues.lSizeCost = 300;
        StaticValues.lSizeBuildup = 0;
        StaticValues.rSize = 0;
        StaticValues.rSizeCost = 300;
        StaticValues.rSizeBuildup = 0;
        StaticValues.lSpeed = 0;
        StaticValues.lSpeedCost = 300;
        StaticValues.lSpeedBuildup = 0;
        StaticValues.rSpeed = 0;
        StaticValues.rSpeedCost = 300;
        StaticValues.rSpeedBuildup = 0;

        Physics.IgnoreLayerCollision(7, 17);
    }

    public void SetStartElement()
    {
        _pa.SetElement(1, StaticValues.startSpellL);
        _pa.SetElement(2, StaticValues.startSpellR);

        lRC.runeCombo = StaticValues.startSpellL;
        rRC.runeCombo = StaticValues.startSpellR;

        cooldownUIL.sprite = icons[StaticValues.startSpellL];
        cooldownUIR.sprite = icons[StaticValues.startSpellR];

        switch (StaticValues.startSpellL)
        {
            case 0:
                lRC.primaryRune = 1;
                lRC.secondaryRune = 1;
                break;
            case 1:
                lRC.primaryRune = 2;
                lRC.secondaryRune = 2;
                break;
            case 2:
                lRC.primaryRune = 3;
                lRC.secondaryRune = 3;
                break;
            case 3:
                lRC.primaryRune = 1;
                lRC.secondaryRune = 2;
                break;
            case 4:
                lRC.primaryRune = 2;
                lRC.secondaryRune = 1;
                break;
            case 5:
                lRC.primaryRune = 3;
                lRC.secondaryRune = 1;
                break;
            case 6:
                lRC.primaryRune = 1;
                lRC.secondaryRune = 3;
                break;
            case 7:
                lRC.primaryRune = 2;
                lRC.secondaryRune = 3;
                break;
            case 8:
                lRC.primaryRune = 3;
                lRC.secondaryRune = 2;
                break;
        }

        switch (StaticValues.startSpellR)
        {
            case 0:
                rRC.primaryRune = 1;
                rRC.secondaryRune = 1;
                break;
            case 1:
                rRC.primaryRune = 2;
                rRC.secondaryRune = 2;
                break;
            case 2:
                rRC.primaryRune = 3;
                rRC.secondaryRune = 3;
                break;
            case 3:
                rRC.primaryRune = 1;
                rRC.secondaryRune = 2;
                break;
            case 4:
                rRC.primaryRune = 2;
                rRC.secondaryRune = 1;
                break;
            case 5:
                rRC.primaryRune = 3;
                rRC.secondaryRune = 1;
                break;
            case 6:
                rRC.primaryRune = 1;
                rRC.secondaryRune = 3;
                break;
            case 7:
                rRC.primaryRune = 2;
                rRC.secondaryRune = 3;
                break;
            case 8:
                rRC.primaryRune = 3;
                rRC.secondaryRune = 2;
                break;
        }
    }

    public void SetSpawnPoint()
    {
        if(StaticValues.spawningLevel == 0)
        {
            print("Game Start");
        }
        else if (StaticValues.spawningLevel == 1)
        {
            print("Game Start 1");
            //Settings.tutorialOn = false;
            _lm.currentLevel = 2;
            _lm.OpenDoor();

            foreach (GameObject obj in ObjsToHideCheckpoint1)
            {
                obj.SetActive(false);
            }

            _esm.currentLevel = 1;

            player.transform.position = checkpoint1SpawnLocation.position;
        }
        else if (StaticValues.spawningLevel == 2)
        {
            print("Game Start 2");
            //Settings.tutorialOn = false;
            _lm.currentLevel = 4;
            _lm.OpenDoor();

            foreach (GameObject obj in ObjsToHideCheckpoint1)
            {
                obj.SetActive(false);
            }

            foreach (GameObject obj in ObjsToHideCheckpoint2)
            {
                obj.SetActive(false);
            }

            _esm.currentLevel = 2;
            player.transform.position = checkpoint2SpawnLocation.position;
        }
        else if (StaticValues.spawningLevel == 3)
        {
            print("Game Start 3");
            //Settings.tutorialOn = false;
            _lm.currentLevel = 5;
            _lm.OpenDoor();

            foreach (GameObject obj in ObjsToHideCheckpoint1)
            {
                obj.SetActive(false);
            }

            foreach (GameObject obj in ObjsToHideCheckpoint2)
            {
                obj.SetActive(false);
            }

            foreach (GameObject obj in ObjsToHideCheckpoint3)
            {
                obj.SetActive(false);
            }

            _esm.currentLevel = 3;
            player.transform.position = checkpoint3SpawnLocation.position;
        }
    }
}
