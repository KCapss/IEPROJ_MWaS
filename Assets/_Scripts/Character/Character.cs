using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField] protected string _name;
    [SerializeField] protected int _healthMax;
    [SerializeField] protected int _healthCurrent;
    protected Sprite _sprite;

    public string Name => _name;

    public int HealthMax
    {
        get => _healthMax;
        set => _healthMax = value;
    } 

    public int HealthCurrent
    {
        get => _healthCurrent;
        set => _healthCurrent = value;
    } 

    public Sprite Sprite
    {
        get => _sprite;
        set => _sprite = value;
    }
    public virtual void Kill() {}
    public virtual void ReceiveDamage(int damage) 
    {
        HealthCurrent -= damage;
        Debug.Log("DAMAGED RECIEVED: " + damage.ToString());

        if(damage < 0)
        {
            HealthCurrent = Mathf.Min(HealthCurrent, HealthMax); //cap healing
        }

        if (HealthCurrent < 0) 
        { 
            Kill(); 
        }

    }
   
}


