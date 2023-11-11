using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DamageCardSlot : MonoBehaviour
{
    [Header("Debug VisibleAnywhere")]
    [SerializeField] private bool _isOccupied = false;
    [SerializeField] private DamageCardObject _cardObject;
    
    public bool IsOccupied 
    { 
        get { return _isOccupied; } 
    }

    public void AddToSlot(DamageCardObject cardObject)
    {
        _cardObject = cardObject;
        _isOccupied = true;
    }

    public void ClearSlot()
    {
        _cardObject = null;
        _isOccupied = false;
    }
}
