using UnityEngine;

public class CardLibrary : MonoBehaviour
{
    [Header("Libraries")]
    [SerializeField] private DamageCardLibrary damageLibrary;
    [SerializeField] private WeaponCardLibrary weaponLibrary;

    public DamageCardLibrary DamageLibrary => damageLibrary;
    public WeaponCardLibrary WeaponLibrary => weaponLibrary;
    public static CardLibrary Instance;

    private void Awake()
    {
        CreateSingleton();
        damageLibrary= GetComponent<DamageCardLibrary>();
        weaponLibrary= GetComponent<WeaponCardLibrary>();
    }

    void CreateSingleton()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
