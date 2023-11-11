using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Main GameManager
    public static GameManager Instance;
    
    [Header("Game Components")]
    [SerializeField] public BattleManager battleManager;
    [SerializeField] public DamageCardManager damageCardManager;
    [SerializeField] public WeaponCardManager weaponCardManager;
    [SerializeField] public ObjectPoolManager objectPoolManager;
    [SerializeField] public LevelManager levelManager;
    [SerializeField] public VFXManager vfxManager;

    private void Awake()
    {
        Time.timeScale = 1;
        CreateSingleton();
    }

    private void Start()
    {
        if(AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBGM(MyStrings.Audio.Level1Theme);
        }
    }

    void CreateSingleton()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

}
