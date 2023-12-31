using UnityEngine;

public class Player : Character, IKillable
{
    [Header("CharacterInformation")]
    [SerializeField] private Animator animator;

    [Header("HEALTH BAR UI")]
    [SerializeField] private HealthBar healthBar;


    private void Start()
    {
        _name = gameObject.name; // can be change later
        _healthCurrent = _healthMax = PlayerData.Instance.GetMaxHP();

        if(animator  = GetComponent<Animator>())
            Debug.Log("Animator Success");
    }


    // Observer Pattern
    private void OnEnable()
    {
        //Register this player to the current player list
    }

    private void OnDisable()
    {
        //UnRegister this player to the current player list
    }


    //Interface Methods
    public  override void Kill()
    {
        //this will trigger the end sequences of the game of the current run
        Debug.LogError("Player Died");
        EventBroadcaster.Instance.PostEvent(EventNames.EndCondition.ON_LOSE);
    }

    public override void ReceiveDamage(int damage, DamageType damageType)
    {
        HealthCurrent -= damage;

        if (HealthCurrent < 0) 
        { 
            Kill(); 
        }

        healthBar.UpdateHealthBar(HealthCurrent, HealthMax);
    }
}
