using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject container;

    void Start()
    {
        var currentEnemy = EnemyLibrary.Instance.GetNextEnemyData();
        Instantiate(currentEnemy, container.transform);        
    }
}
