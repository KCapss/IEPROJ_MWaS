using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[Serializable]
public class Int2Val
{
    public int current;
    public int max;

    public bool canGoNegative;

    public Int2Val(int current, int max)
    {
        this.current = current;
        this.max = max;
    }

    internal void Subtract(int amount)
    {
        current -= amount;

        if (canGoNegative == false)
        {
            if (current < 0)
            {
                current = 0;
            }
        }
    }
}
public class Character : MonoBehaviour
{
    [Header("CHARACTER STATS")]
    [SerializeField] public CharacterSheet statsTemplate;
    [SerializeField] private GameObject numberUI;
    
    public string Name { get; private set; }
    public float MovementPoints { get; private set; } // 10 points = 1 tile
    public Int2Val HP { get; private set; }
    public int Damage { get; private set; }
    public int AttackRange { get; private set; }
    public Factions Faction { get; private set; }
    public bool isDefeated { get; private set; }
    public MyEnums.ItemDropType characterDrop { get; private set;}
    public int characterMoneyDrop { get; private set; }

    private void Awake()
    {
        Name = statsTemplate.name;
        MovementPoints = statsTemplate.movementPoints;
        HP = statsTemplate.HP;
        HP.current = HP.max;
        Damage = statsTemplate.damage;
        AttackRange = statsTemplate.attackRange;
        Faction = statsTemplate.faction;
        characterDrop = statsTemplate.DropType;
        characterMoneyDrop = statsTemplate.Money;

        isDefeated = false;

        GetComponent<CharacterTurn>().Faction = Faction;
    }

    private void Start()
    {
        Instantiate(statsTemplate.sprite, transform);
    }

    public void TakeDamage(int damage)
    {
        HP.Subtract(damage);
        StartCoroutine(ShowDamage(damage.ToString()));
        //Debug.Log(damage);
        Debug.Log($"{Name} HP: {HP.current} / {HP.max}");
        
    }

    IEnumerator ShowDamage(string damage)
    {
        GameObject obj = Instantiate(numberUI, transform);
        obj.GetComponent<DamageNumber>().SetNumber(damage);

        yield return new WaitForSeconds(1f);

        Destroy(obj);
        CheckDefeated();
    }

    private void CheckDefeated()
    {
        if (HP.current <= 0)
        {
            Defeated();
        }
    }

    private void Defeated()
    {
        Debug.Log($"Defeated {gameObject.name}");
        isDefeated = true;
        gameObject.SetActive(false);

        EventBroadcaster.Instance.PostEvent(EventNames.EndCondition.CHECK_CONDITON);
        DropItem();
        this.GetComponent<GridObject>().RemoveFromGrid();
    }

    private void DropItem()
    {
        DropsManager.instance.AddDrop(characterDrop, 1);
        DropsManager.instance.AddMoney(characterMoneyDrop);
    }
}
