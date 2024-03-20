using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSpell : MonoBehaviour
{
    public bool right;
    
    public List<GameObject> damageLevels;
    public List<GameObject> sizeLevels;
    public List<GameObject> speedLevels;

    public TMPro.TextMeshProUGUI playerScore;

    public TMPro.TextMeshProUGUI damageCostText;
    public TMPro.TextMeshProUGUI sizeCostText;
    public TMPro.TextMeshProUGUI speedCostText;

    public Color LevelRecolorColor;

    public int damage;
    public int size;
    public int speed;

    private float damageMultiplier = 1.8f;
    private float sizeMultiplier = 1.7f;
    private float speedMultiplier = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        if (right)
        {
            damage = StaticValues.rDamage;
            size = StaticValues.rSize;
            speed = StaticValues.rSpeed;
        }
        else if (!right)
        {
            damage = StaticValues.lDamage;
            size = StaticValues.lSize;
            speed = StaticValues.lSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (right)
        {
            damageCostText.text = "Cost: " + StaticValues.rDamageCost.ToString();
            sizeCostText.text = "Cost: " + StaticValues.rSizeCost.ToString();
            speedCostText.text = "Cost: " + StaticValues.rSpeedCost.ToString();
        }
        else if (!right)
        {
            damageCostText.text = "Cost: " + StaticValues.lDamageCost.ToString();
            sizeCostText.text = "Cost: " + StaticValues.lSizeCost.ToString();
            speedCostText.text = "Cost: " + StaticValues.lSpeedCost.ToString();
        }

        playerScore.text = StaticValues.score.ToString();
    }

    public void UpgradeDamage()
    {
        if (right && StaticValues.score >= StaticValues.rDamageCost)
        {
            StaticValues.rDamage++;
            damageLevels[StaticValues.rDamage - 1].GetComponent<Image>().color = LevelRecolorColor;

            StaticValues.score -= StaticValues.rDamageCost;
            StaticValues.rDamageCost = (int)(StaticValues.rDamageCost * damageMultiplier);
            StaticValues.rDamageBuildup += 2;
        }
        else if (!right && StaticValues.score >= StaticValues.lDamageCost)
        {
            StaticValues.lDamage++;
            damageLevels[StaticValues.lDamage - 1].GetComponent<Image>().color = LevelRecolorColor;

            StaticValues.score -= StaticValues.lDamageCost;
            StaticValues.lDamageCost = (int)(StaticValues.lDamageCost * damageMultiplier);
            StaticValues.lDamageBuildup += 2;
        }
    }

    public void UpgradeSize()
    {
        if (right && StaticValues.score >= StaticValues.rSizeCost)
        {
            StaticValues.rSize++;
            sizeLevels[StaticValues.rSize - 1].GetComponent<Image>().color = LevelRecolorColor;

            StaticValues.score -= StaticValues.rSizeCost;
            StaticValues.rSizeCost = (int)(StaticValues.rSizeCost * damageMultiplier);
            StaticValues.rSizeBuildup += 0.1f;
        }
        else if (!right && StaticValues.score >= StaticValues.lSizeCost)
        {
            StaticValues.lSize++;
            sizeLevels[StaticValues.lSize - 1].GetComponent<Image>().color = LevelRecolorColor;

            StaticValues.score -= StaticValues.lSizeCost;
            StaticValues.lSizeCost = (int)(StaticValues.lSizeCost * damageMultiplier);
            StaticValues.lSizeBuildup += 0.1f;
        }
    }

    public void UpgradeSpeed()
    {
        if (right && StaticValues.score >= StaticValues.rSpeedCost)
        {
            StaticValues.rSpeed++;
            speedLevels[StaticValues.rSpeed - 1].GetComponent<Image>().color = LevelRecolorColor;

            StaticValues.score -= StaticValues.rSpeedCost;
            StaticValues.rSpeedCost = (int)(StaticValues.rSpeedCost * damageMultiplier);
            StaticValues.rSpeedBuildup += 1;
        }
        else if (!right && StaticValues.score >= StaticValues.lSpeedCost)
        {
            StaticValues.lSpeed++;
            speedLevels[StaticValues.lSpeed - 1].GetComponent<Image>().color = LevelRecolorColor;

            StaticValues.score -= StaticValues.lSpeedCost;
            StaticValues.lSpeedCost = (int)(StaticValues.lSpeedCost * damageMultiplier);
            StaticValues.lSpeedBuildup += 1;
        }
    }
}
