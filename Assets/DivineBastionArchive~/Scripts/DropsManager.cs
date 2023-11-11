using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DropsManager : MonoBehaviour
{
    public static DropsManager instance;
    private Dictionary<MyEnums.ItemDropType, int> levelDrops;
    private int Money = 0;

    private void Awake()
    {
        instance = this;
        levelDrops= new Dictionary<MyEnums.ItemDropType, int>();
        PlayerPrefs.DeleteAll();
        foreach(MyEnums.ItemDropType dropType in Enum.GetValues(typeof(MyEnums.ItemDropType)))
        {
            //if(levelDrops.ContainsKey(dropType) == false)
            //{
                levelDrops.Add(dropType, 0);
            //}
        }

        foreach(var item in levelDrops)
        {
            Debug.Log(item);
        }
    }

    public void AddDrop(MyEnums.ItemDropType itemType, int itemCount)
    {
        levelDrops[itemType] += itemCount;
    }

    public void AddMoney(int AddedAmount)
    {
        Money += AddedAmount;
    }

    public void OnDisable()
    {
        int temp;
        //MONEY drop
        if(PlayerPrefs.HasKey(PlayerValues.MONEY) == false)
        {
            PlayerPrefs.SetInt(PlayerValues.MONEY, 0);
        }

        temp = PlayerPrefs.GetInt(PlayerValues.MONEY);
        PlayerPrefs.SetInt(PlayerValues.MONEY, temp + Money);

        //CELESTIAL ORB drop
        if(PlayerPrefs.HasKey(PlayerValues.CELESTIAL_ORB) == false)
        {
            PlayerPrefs.SetInt(PlayerValues.CELESTIAL_ORB, 0);
        }

        temp = PlayerPrefs.GetInt(PlayerValues.CELESTIAL_ORB);
        PlayerPrefs.SetInt(PlayerValues.CELESTIAL_ORB, temp + levelDrops[MyEnums.ItemDropType.CelestialOrb]);

        //STICK drop
        if(PlayerPrefs.HasKey(PlayerValues.STICK) == false)
        {
            PlayerPrefs.SetInt(PlayerValues.STICK, 0);
        }

        temp = PlayerPrefs.GetInt(PlayerValues.STICK);
        PlayerPrefs.SetInt(PlayerValues.STICK, temp + levelDrops[MyEnums.ItemDropType.Stick]);
    }
}
