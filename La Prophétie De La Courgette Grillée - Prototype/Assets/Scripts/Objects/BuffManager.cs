using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public void MangerRepas(Repas repas)
    {
		PlayerHealthManager phm = FindObjectOfType<PlayerHealthManager>();

        phm.HealPlayer(repas.heal);
        FindObjectOfType<WeaponOnPlayer>().damageBuff += repas.damageBuff - 1;
        FindObjectOfType<PlayerControllerIsometric>().speedBuff += repas.speedBuff - 1;
		phm.StartCoroutine(phm.AutoHeal(repas.lifeBuff, repas.timeOfBuff));
        StartCoroutine(WaitTimeOfBuff(repas));
    }

    private IEnumerator WaitTimeOfBuff(Repas repas)
    {
        yield return new WaitForSeconds(repas.timeOfBuff);
        FindObjectOfType<WeaponOnPlayer>().damageBuff -= (repas.damageBuff - 1f);
        FindObjectOfType<PlayerControllerIsometric>().speedBuff -= (repas.speedBuff - 1f);
    }
}