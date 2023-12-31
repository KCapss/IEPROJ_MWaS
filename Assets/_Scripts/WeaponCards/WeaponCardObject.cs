using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class WeaponCardObject : MonoBehaviour, IDropHandler
{
    [Header("Debug VisibleAnywhere")]
    [SerializeField] private int damage = 0;
    [SerializeField] private DamageCardObject damageCardInChamber = null;
    [SerializeField] private bool isOnCooldown = false;

    [Header("Card Components")]
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private Image weaponArt;
    [SerializeField] private Image weaponElement;
    [SerializeField] private TextMeshProUGUI restrictionValue;
    [SerializeField] private TextMeshProUGUI restrictionType;
    [SerializeField] private TextMeshProUGUI cardText;
    [SerializeField] private TextMeshProUGUI cardCooldown;
    [SerializeField] private GameObject damageCardSlot;
    [SerializeField] private VFXTag vfxTag;
    [SerializeField] private CooldownIndicator cooldownIndicator;

    private WeaponCard weaponCardData;

    public DamageCardObject DamageCardInChamber 
    { 
        set { damageCardInChamber = value; } 
    }

    public WeaponCard WeaponCardData
    {
        get { return weaponCardData; }
        set { ReceiveCardData(value); }
    }

    public void OnDrop(PointerEventData eventData)
    {      
        // If transform being dragged is a damage card
        if(eventData.pointerDrag.transform.tag == MyTags.DAMAGE_CARD && isOnCooldown == false)
        {
            Debug.Log("Damage Card Drop Event");
            // Get Damage Card Component
            DamageCardObject damageCard = eventData.pointerDrag.GetComponent<DamageCardObject>();

            // If Damage Card Value is within weapon restrictions
            if(damageCard != null) 
            {
                //If Weapon is In MIN MODE AND DMG Card Value Meets Minimum
                if((weaponCardData.RestrictionType == RestrictionType.MIN 
                    && damageCard.CardData.DamageValue >= weaponCardData.RestrictionValue) 

                || // OR

                //If Weapon is In MAX MODE AND DMG Card Value Meets Maximum
                (weaponCardData.RestrictionType == RestrictionType.MAX 
                    && damageCard.CardData.DamageValue <= weaponCardData.RestrictionValue))
                {
                    //set card position
                    damageCard.StartPosition = damageCardSlot.transform.position;
                    damageCard.transform.position = damageCardSlot.transform.position;

                    //allocating the slot of the weapon card object
                    damageCardInChamber = damageCard;
                    damageCardInChamber.GetComponent<DamageCardObject>().IsPlayed = true;

                    //Debug.Log(damageCard.CardData.DamageType);
                    damage = damageCard.CardData.DamageValue;

                    PlayAttackSequence();
                    AttackSequence();
                }
            }
        }
    }

    private void PlayAttackSequence()
    {
        bool isCrit = false;
        Lane weaponLane = GetComponentInParent<WeaponCardSlot>().Lane;

        if(damageCardInChamber.CardData.DamageType == weaponCardData.DamageType)
        {
            isCrit = true;
        }

        int weaponType = (int)weaponCardData.WeaponCardType / 8;

        Debug.Log(weaponType);

        GameManager.Instance.vfxManager.PlayAttackVFX((VFXTag)weaponType, weaponCardData.DamageType, isCrit, weaponLane);

        int weaponArchetype =  (int)weaponCardData.WeaponCardType / 24;

        string SFX_string;

        if(weaponArchetype == 0)
        {
            SFX_string = "Dagger_SFX";
        }
        else if (weaponArchetype == 1)
        {
             SFX_string = "Bow_SFX";
        }
        else
        {
            SFX_string = "Sword_SFX";
        }

        AudioManager.Instance.PlaySFX(SFX_string);
    }

    public void AttackSequence()
    {
        StartCoroutine(StartCooldownIndicator(weaponCardData.Cooldown));
    }

   IEnumerator StartCooldownIndicator(float cooldownTime)
    {
        yield return new WaitForEndOfFrame();
        FireChamber();
        //cooldownIndicator.StartCooldownIndicator(cooldownTime);
        yield return StartCoroutine(StartCooldown(cooldownTime));
    }


    // Consume Damage Card and Deal Damage
    private void FireChamber()
    {
        bool isSameType = false;

        //Add a checker to prevent any null reference pointer upon winning the battle
        if (damageCardInChamber != null)
        {

            if (weaponCardData.DamageType == damageCardInChamber.CardData.DamageType)
            {
                isSameType = true;
            }

            //add animation and sound play here
            GameManager.Instance.battleManager.DealDamage(Faction.Enemy,
                weaponCardData.CalculateDamage(damage, damageCardInChamber.CardData.DamageType),
                isSameType ? weaponCardData.DamageType : DamageType.NONE);

            //Reset Stuff
            damageCardInChamber.Destroy();
            damageCardInChamber = null;
            damage = 0;

            GameManager.Instance.damageCardManager.PlayerDrawCard();
        }
    }

    IEnumerator StartCooldown(float cooldownTime)
    {
        if (isOnCooldown) yield break;

        isOnCooldown = true;

        float startTime = Time.time;
        float elapsedTime = 0.0f;

        while (elapsedTime < cooldownTime)
        {
            elapsedTime = Time.time - startTime;
            float completionRate = 1.0f - Mathf.Clamp01(elapsedTime / cooldownTime);
            cooldownIndicator.SetFillAmount(completionRate);
            yield return null;
        }

        isOnCooldown = false;
    }

    public void ReceiveCardData(WeaponCard data)
    {
        weaponCardData = data;
        vfxTag = data.VFXWeaponTag;
        cardName.text = data.Name;
        weaponArt.sprite = data.CardIcon;
        weaponElement.sprite = weaponCardData.CardBackground; 
        restrictionValue.text = data.RestrictionValue.ToString();
        cardText.text = data.CardText.ToString();

        switch(data.RestrictionType)
        {
            case RestrictionType.MAX:
                restrictionType.text = "MAX";
                break;

            case RestrictionType.MIN:
                restrictionType.text = "MIN";
                break;

            default:
                break;
        }

        cardCooldown.text = data.Cooldown.ToString() + " seconds";
    }
}
