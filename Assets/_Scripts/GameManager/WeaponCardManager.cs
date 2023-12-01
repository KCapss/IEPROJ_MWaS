using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCardManager : MonoBehaviour
{
    [SerializeField] private List<WeaponCard> weaponCards;
    [SerializeField] private List<WeaponCardObject> weaponObjects;

    private void OnEnable()
    {
        GetCurrentWeapons();
        DisplayWeapons();
    }

    private void DisplayWeapons() 
    {
        for(int i = 0; i < 3; i++)
        {
            //GameObject card = GameManager.Instance.objectPoolManager.GetPooledObject(PoolTag.WeaponCard);
            //card.transform.SetParent(weaponSlots[i].transform);

            //// Set Transfrom and SetActive true
            //card.GetComponent<RectTransform>().localPosition = Vector3.zero;
            //card.transform.rotation = Quaternion.identity;
            //card.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            //card.SetActive(true);

            //Set Data and Add To List of WeaponObjects
            weaponObjects[i].WeaponCardData = weaponCards[i];
            //weaponObjects.Add(card.GetComponent<WeaponCardObject>());
        }
    }

    private void GetCurrentWeapons()
    {
        weaponCards = PlayerData.Instance.GetWeapons();
    }
}
