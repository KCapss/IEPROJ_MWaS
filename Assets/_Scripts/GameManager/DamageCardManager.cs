using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Random = UnityEngine.Random;

/* this class manages deck the player's deck management */
public class DamageCardManager : MonoBehaviour
{
    [SerializeField] private List<DamageCard> deckData = new List<DamageCard>();
    [SerializeField] private Queue<DamageCard> deckActual = new Queue<DamageCard>();
    [SerializeField] private List<DamageCardSlot> handSlots = new List<DamageCardSlot>();

    [Header("Player Indicator")]
    [SerializeField] private TextMeshProUGUI currentAmount;
    [SerializeField] private TextMeshProUGUI maxAmount;
    [SerializeField] private DamageCardObject preview;

    private void Start()
    {
        PlayerData playerData = FindObjectOfType<PlayerData>(); 
        deckData = playerData.GetDeckElements();
        maxAmount.text = deckData.Count.ToString();

        FillDeck();
        FillHand();
    }

    //Shuffles Deck Data Order
    //Fisher Yates Shuffle from https://gist.github.com/jasonmarziani/7b4769673d0b593457609b392536e9f9
    public void ShuffleDeck()
    {
        for(int i = deckData.Count-1; i > 0; i--)
        {
            int rnd = Random.Range(0, i);
            DamageCard temp = deckData[i];

            deckData[i] = deckData[rnd];
            deckData[rnd] = temp;
        }
    }

    //Clears and Repopulates deckActual
    public void FillDeck()
    {
        ShuffleDeck();
        deckActual.Clear();

        for (int i = 0; i < deckData.Count; i++)
        {
            DamageCard element = deckData[i];
            deckActual.Enqueue(element);
        }
    }

    //Gets Card Info -> Called by DamageCard Object
    public DamageCard GetDamageCardData()
    {
        if(deckActual.Count > 0)
        {
            DamageCard data = deckActual.Dequeue();
            return data;
        }

        return null;
    }

    //Draw 1 Card
    public void PlayerDrawCard()
    { 
        //escape statement if deck is empty
        if(deckActual.Count < 1)
        {
            Debug.Log("Deck is EMPTY!!");
            //Add Effect if needed
            return;
        }

        GameObject card = GameManager.Instance.objectPoolManager.GetPooledObject(PoolTag.DamageCard);

        if(card != null)
        {
            int index = FindEmptySlot();
            if(index > -1)
            {
                card.SetActive(true);
                card.transform.SetParent(handSlots[index].transform);
                card.GetComponent<RectTransform>().localPosition = Vector3.zero;
                card.transform.rotation = Quaternion.identity;
                card.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                

                card.GetComponent<DamageCardObject>().SetCardData(GetDamageCardData());
                handSlots[index].AddToSlot(card.GetComponent<DamageCardObject>());
                currentAmount.text = deckActual.Count.ToString();
                if(deckActual.Count != 0)
                    preview.SetCardData(deckActual.Peek());
            }
            else
            {
                Debug.Log("Hand is Full");            
            }
            
        }
    }

    //Draw until hand is full
    public void FillHand()
    {
        int nEmpty = CountEmptySlot();
        for(int i = 0; i < nEmpty; i++)
        {
            PlayerDrawCard();
        }
    }

    //look for an empty hand slot
    public int FindEmptySlot()
    {
        for(int i = 0; i < handSlots.Count; i++)
        {
            if(handSlots[i].IsOccupied == false)
            {
                return i;
            }
        }

        return -1;
        
    }

    //return the number of empty hand slots
    private int CountEmptySlot()
    {
        int count = 0;

        for(int i = 0; i < handSlots.Count; i++)
        {
            if(handSlots[i].IsOccupied == false)
            {
                count++;
            }
        }
        return count;
    }

    public void ResetDeck()
    {
        FillDeck();
    }
}
