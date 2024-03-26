using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetStartValue : MonoBehaviour
{

    
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
