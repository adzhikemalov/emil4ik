using System.Collections;
using System.Collections.Generic;
using PokemonBattle.TurnBasedCombat;
using UnityEngine;

public class Unit : MonoBehaviour
{

	public string unitName;
	public int unitLevel;

	public BattleMove Move1;
	public BattleMove Move2;
	public BattleMove Move3;
	public BattleMove Move4;
	
	public int damage;

	public int maxHP;
	public int currentHP;

	public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}

	public void Heal(int amount)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}
}
