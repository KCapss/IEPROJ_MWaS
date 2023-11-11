using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenuAttribute(menuName = "WCardBucket", fileName = "New Weapon Card Bucket")]
public class WeaponCardBucket : ScriptableObject
{
    public List<WeaponCard_SO> list;
}


