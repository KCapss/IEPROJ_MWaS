using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCardManager : MonoBehaviour
{
    [SerializeField] private List<WeaponCard> weaponCards;
    [SerializeField] private List<WeaponCardObject> weaponObjects;

    private void OnStart()
    {
        GetCurrentWeapons();
        DisplayWeapons();
    }

    private void DisplayWeapons() 
    {
        for(int i = 0; i < 3; i++)
        {
            weaponObjects[i].WeaponCardData = weaponCards[i];
        }
    }

    private void GetCurrentWeapons()
    {
        weaponCards = PlayerData.Instance.GetWeapons();
    }
}
