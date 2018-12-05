using UnityEngine;
using TMPro;

public class EXPManager : MonoBehaviour
{
	public int joueurXP = 0;
	public int plafondXP = 20;
	public int level = 1;

	public WeaponOnPlayer weaponOnPlayer;

    [Header("UI")]
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI lvlText;

	public void AddExperience(int xp)
	{
		joueurXP += xp;
		while(joueurXP >= plafondXP)
		{
			AddLevel();
		}

        UpdateUI();
	}

	private void AddLevel()
	{
		joueurXP = joueurXP - plafondXP;
		plafondXP += 10 * level;
		level++;

        // Upgrades
		weaponOnPlayer.damage *= 1.2f;
	}

    private void UpdateUI()
    {
        xpText.text = "" + joueurXP + "/" + plafondXP;
        lvlText.text = "" + level;
    }
}
