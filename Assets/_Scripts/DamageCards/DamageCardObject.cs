using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* Class that handle the prefab */
public class DamageCardObject : MonoBehaviour, IBeginDragHandler ,IDragHandler, IEndDragHandler
{
    [SerializeField] private Image draggableImage;
    [SerializeField] private Image _cardBackground;
    [SerializeField] private TextMeshProUGUI _cardText;
    [SerializeField] private bool _isPlayed = false;
    [SerializeField] private bool _isDisabled = false;
    [SerializeField] private DamageType _type;

    public Vector3 StartPosition { get; set; }
    public DamageCard CardData { get; set; }
    public bool IsPlayed 
    { 
        get { return _isPlayed; } 
        set { _isPlayed = value;} 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(IsPlayed == false)
        {
            StartPosition = transform.position;
            draggableImage.raycastTarget = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = StartPosition;
        draggableImage.raycastTarget = true;
    }

    public void SetCardData(DamageCard cardData)
    {
        CardData = cardData;
        _cardBackground.sprite = CardData.CardBackground;
        _cardText.text = cardData.DamageValue.ToString();
        _type = cardData.DamageType;
    }

    public void Destroy()
    {
        transform.GetComponentInParent<DamageCardSlot>().ClearSlot();
        IsPlayed = false;
        gameObject.SetActive(false);
    }
}
