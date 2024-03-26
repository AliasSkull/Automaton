using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIconManagement : MonoBehaviour
{
    public List<EnemyWayIcons> wayIcons;
    public GameObject[] enemies;

    public bool changed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(StaticValues.timeSinceLastKill > 25 && !changed)
        {
            enemies = GameObject.FindGameObjectsWithTag("Damageable");
            
            for(int i = 0; i < enemies.Length && i < wayIcons.Count; i++)
            {
                wayIcons[i].target = enemies[i].transform;
                wayIcons[i].gameObject.SetActive(true);
            }

            changed = true;
        }
        else if(StaticValues.timeSinceLastKill < 10 && changed)
        {
            enemies = null;

            foreach (EnemyWayIcons script in wayIcons)
            {
                if (script.gameObject.activeSelf)
                {
                    script.gameObject.SetActive(false);
                }
            }
            changed = false;
        }
    }
}
