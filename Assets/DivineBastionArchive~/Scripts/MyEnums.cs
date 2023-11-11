using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnums : MonoBehaviour
{
    public enum ItemDropType { None = -1, CelestialOrb = 0, Stick };
    public static ItemDropType itemDropType;
}
