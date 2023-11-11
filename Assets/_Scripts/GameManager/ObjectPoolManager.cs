using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [Header("PREFABS")]
    [SerializeField] private List<GameObject> cardPrefabs;

    [Header("Pooled Objects")]
    [SerializeField] private List<List<GameObject>> objectPools;

    [Header("Pool Settings")]
    [Tooltip("How Many Objects to Create")]
    [SerializeField] private int poolAmount;
    [Tooltip("Container")]
    [SerializeField] private GameObject container;

    void Awake()
    {
        CreateObjectPools();
    }

    private void CreateObjectPools()
    {
        objectPools = new List<List<GameObject>>();
        for(int i = 0; i < cardPrefabs.Count; i++)
        {
            objectPools.Add(new List<GameObject>());

            for(int j = 0; j < poolAmount; j++)
            {
                GameObject temp;
                temp = Instantiate(cardPrefabs[i], container.transform);
                temp.SetActive(false);
                objectPools[i].Add(temp);
            }
        }    
    }

    public GameObject GetPooledObject(PoolTag poolTag)
    {
        int index = poolAmount;
        int poolIndex = (int)poolTag;
        for(int i = 0; i < index; i++)
        {
            if(objectPools[poolIndex][i].activeInHierarchy == false)
            {
                return objectPools[poolIndex][i];
            }
        }

        return objectPools[poolIndex][index];
    }
}
