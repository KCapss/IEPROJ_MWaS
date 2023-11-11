using UnityEngine;
using UnityEngine.EventSystems;

public class ReplaceWeapon : MonoBehaviour, IDropHandler
{
    [SerializeField] private WeaponCardObject owner;
    [SerializeField] private Lane weaponLane;

    public void OnDrop(PointerEventData eventData)
    {
        // If transform being dragged is a weapon card
        if(eventData.pointerDrag.transform.tag == MyTags.WEAPON_CARD)
        {
            Debug.Log("Replace Weapon Drop Event");
            // Get Weapon Card Component
            WeaponCardObject weaponCard = eventData.pointerDrag.GetComponent<WeaponCardObject>();

            if(weaponCard != null) 
            {
                WeaponCard cardData = weaponCard.WeaponCardData;
                owner.ReceiveCardData(cardData);
                weaponCard.gameObject.SetActive(false);
                PlayerData.Instance.ReplaceCurrentWeapon(weaponLane, cardData.WeaponCardType, cardData.DamageType);
                EventBroadcaster.Instance.PostEvent(EventNames.UI.DAMAGE_REWARD_OPEN);
            }
        }
    }
}
