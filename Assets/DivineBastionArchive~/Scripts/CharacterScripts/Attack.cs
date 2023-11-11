using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Character character;

    // Start is called before the first frame update
    private void Awake()
    {
        character = gameObject.GetComponent<Character>();
    }

    public void AttackGridPosition(GridObject targetGridObject)
    {
        int damage = RandomizeDamage(character.Damage);
        targetGridObject.GetComponent<Character>().TakeDamage(damage);
    }

    private int RandomizeDamage(int damage)
    {
        float random = UnityEngine.Random.Range(0f, 1f);
        Debug.Log(random);

        if (random <= 0.4)
        {
            return damage - 1;
        }
        else if (random > 0.4 && random <= 0.9)
        {
            return damage;
        }
        else 
        {
            return damage * 2;
        }
    }
}

