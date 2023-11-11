using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private List<Image> healthBarList;

    public void UpdateHealthBar(float healthCurrent, float healthMax)
    {
        float healthPercent = (float)healthCurrent / (float)healthMax;

        foreach(Image healthBar in healthBarList)
        {
             healthBar.fillAmount = healthPercent;

            if(healthPercent <= 0)
            {
                healthBar.color = new Color(0, 0, 0, 0); 
            }
            else if(healthPercent <= 0.1)
            {
                healthBar.color = new Color(1.0f, 0, 0, 1.0f); // Red
            }
            else if (healthPercent <= 0.25)
            {
                healthBar.color = new Color(1.0f, 0.647f, 0.0f, 1.0f); // Orange
            }
            else if (healthPercent <= 0.5)
            {
                healthBar.color = new Color(1.0f, 1.0f, 0.0f, 1.0f); // Yellow
            }
            else if (healthPercent <= 0.75)
            {
                healthBar.color = new Color(0.565f, 0.933f, 0.565f, 1.0f); // Light Green
            }
            else 
            {
                healthBar.color = new Color(0, 0.5f, 0, 1.0f); //Green
            }
        }
       
    }

    private void OnDrawGizmos()
    {
        foreach(Image healthBar in healthBarList)
        {
            float healthPercent = healthBar.fillAmount;

        if(healthPercent <= 0)
        {
            healthBar.color = new Color(0, 0, 0, 0); 
        }
        else if(healthPercent <= 0.1)
        {
            healthBar.color = new Color(1.0f, 0, 0, 1.0f); // Red
        }
        else if (healthPercent <= 0.25)
        {
            healthBar.color = new Color(1.0f, 0.647f, 0.0f, 1.0f); // Orange
        }
        else if (healthPercent <= 0.5)
        {
            healthBar.color = new Color(1.0f, 1.0f, 0.0f, 1.0f); // Yellow
        }
        else if (healthPercent <= 0.75)
        {
            healthBar.color = new Color(0.565f, 0.933f, 0.565f, 1.0f); // Light Green
        }
        else 
        {
            healthBar.color = new Color(0, 0.5f, 0, 1.0f); //Green
        }
        }
        
    }
}
