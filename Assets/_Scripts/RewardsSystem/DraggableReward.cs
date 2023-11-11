using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableReward : MonoBehaviour, IBeginDragHandler ,IDragHandler, IEndDragHandler
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Image draggableImage;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        draggableImage.raycastTarget = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPosition;
        draggableImage.raycastTarget = true;
    }
}
