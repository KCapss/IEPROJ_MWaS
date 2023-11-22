using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Player player;

    /* Replace Faction with Character Class */
    public void DealDamage(Faction target, int damage, DamageType damageType)
    {
        switch(target)
        {
            case Faction.Player: 
                player.ReceiveDamage(damage, damageType); 
                break;

            case Faction.Enemy:
                enemy.ReceiveDamage(damage, damageType);
                break;
        }
    }

    public void RegisterEnemy(Enemy enemy)
    {
        this.enemy = enemy;
    }
}
    

   

