using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetNumber(string damage)
    {
        text.SetText(damage);
    }
}
