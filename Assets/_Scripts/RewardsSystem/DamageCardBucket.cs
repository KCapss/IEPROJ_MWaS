using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenuAttribute(menuName = "DCardBucket", fileName = "New Damage Card Bucket")]
public class DamageCardBucket : ScriptableObject
{
    [SerializeField] public string _name;
    [SerializeField] public List<DamageTag> cards;
}
