using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Player player;

    /* Replace Faction with Character Class */
    public void DealDamage(Faction target, int damage)
    {
        switch(target)
        {
            case Faction.Player: 
                player.ReceiveDamage(damage); 
                break;

            case Faction.Enemy:
                enemy.ReceiveDamage(damage);
                break;
        }
    }

    public void RegisterEnemy(Enemy enemy)
    {
        this.enemy = enemy;
    }
}
    

   

