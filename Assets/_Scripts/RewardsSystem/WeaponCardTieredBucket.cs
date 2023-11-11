using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenuAttribute(menuName = "WCardTieredBucket", fileName = "New Weapon Tiered Bucket")]
public class WeaponCardTieredBucket : ScriptableObject
{
    public List<WeaponTag> list;
}
