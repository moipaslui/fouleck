using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPManager : MonoBehaviour
{
	public int joueurXP = 0;
	public int plafondXP = 20;
	public int level = 1;

	public WeaponOnPlayer weaponOnPlayer;

	public void AddExperience(int xp)
	{
		joueurXP += xp;
		while(joueurXP >= plafondXP)
		{
			AddLevel();
		}
	}

	public void AddLevel()
	{
		joueurXP = joueurXP - plafondXP;
		plafondXP = 20 + 10 * level;
		level = level + 1;
		weaponOnPlayer.damage *= 1.2f;
	}
}
