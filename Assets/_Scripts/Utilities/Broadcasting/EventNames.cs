using UnityEngine;
using System.Collections;

/*
 * Holder for event names
 * Created By: NeilDG
 */ 
public class EventNames 
{

	public class EndCondition
	{
		public const string ON_DEATH = "ON_DEATH";
        public const string ON_WINN = "ON_WINN";
		public const string ON_LOSE = "ON_LOSE";
		public const string ON_COMBAT_END = "ON_COMBAT_END";
    }

	public class UI
	{
		public const string DAMAGE_REWARD_OPEN = "DAMAGE_REWARD_SELECTED";
		public const string WEAPON_REWARD_OPEN = "WEAPON_REWARD_SELECTED";
		public const string CONTINUE = "CONTINUE";
        public const string END_TURN = "END_TURN";
		public const string SHIELDS_UP = "SHIELDS_UP";
	}

	public class Hotkeys
	{
		public const string DESELECT_CHARACTER = "DESELECT_CHARACTER";
	}

	public class AttackSequence
	{
		public const string ATTACK_ANIMATION_END = "ATTACK_ANIMATION_END";
		public const string ENEMY_ATTACK = "ENEMY_ATTACK";
	}
	
}







