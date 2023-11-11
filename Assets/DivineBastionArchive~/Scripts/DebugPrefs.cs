using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPrefs : MonoBehaviour
{
    int Money;
    int CelestialOrb;
    int Stick;

    private void Start()
    {
        Money = PlayerPrefs.GetInt(PlayerValues.MONEY);
        CelestialOrb = PlayerPrefs.GetInt(PlayerValues.CELESTIAL_ORB);
        Stick = PlayerPrefs.GetInt(PlayerValues.STICK);

        Debug.Log($"Money:{Money}");
        Debug.Log($"Celestial Orbs: {CelestialOrb}");
        Debug.Log($"Sticks: {Stick}");
    }
}
