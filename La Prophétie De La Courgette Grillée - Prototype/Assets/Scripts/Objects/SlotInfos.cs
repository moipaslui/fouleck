using System.Collections;
using UnityEngine;
using TMPro;

public class SlotInfos : MonoBehaviour
{
    public bool isInventory;
    public TextMeshProUGUI infosText;
    public TextMeshProUGUI infosTitle;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void DisplayInfos(Item itemToBuy)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayInfosCoroutine(itemToBuy));
    }

    public void HideInfos()
    {
        StopAllCoroutines();
        StartCoroutine("HideInfosCoroutine");
    }

    private IEnumerator DisplayInfosCoroutine(Item itemToBuy)
    {
        infosTitle.gameObject.SetActive(true);
        infosTitle.text = itemToBuy.name;

        infosText.gameObject.SetActive(true);
        infosText.text = itemToBuy.description + "\n";
        if (itemToBuy.GetType() == typeof(Weapon))
        {
            infosText.text += "\nDamage : " + ((Weapon)itemToBuy).damage;
            infosText.text += "\nNiveau : " + ((Weapon)itemToBuy).levelNeeded;
        }
        if (itemToBuy.GetType() == typeof(Repas))
        {
            infosText.text += "\nSoin : " + ((Repas)itemToBuy).heal;
            if(((Repas)itemToBuy).damageBuff != 1f)
                infosText.text += "\nDegat  x  " + ((Repas)itemToBuy).damageBuff;
            if(((Repas)itemToBuy).shieldBuff != 0)
                infosText.text += "\nArmure + " + ((Repas)itemToBuy).shieldBuff;
            if(((Repas)itemToBuy).speedBuff != 1f)
                infosText.text += "\nVitesse  x  " + ((Repas)itemToBuy).speedBuff;
            if(((Repas)itemToBuy).lootBuff != 1f)
                infosText.text += "\nLoot  x  " + ((Repas)itemToBuy).lootBuff;
            if(((Repas)itemToBuy).lifeBuff != 0f)
                infosText.text += "\nVie + 1 / " + ((Repas)itemToBuy).lifeBuff + "s";
            if(((Repas)itemToBuy).timeOfBuff != 0f)
                infosText.text += "\nTemps : " + ((Repas)itemToBuy).timeOfBuff + " secs";
        }
        infosText.text += "\nPrix : " + itemToBuy.cost;

        if (isInventory)
        {
            float xScale = rectTransform.localScale.x;
            for (; xScale <= 1; xScale += 0.04f)
            {
                rectTransform.localPosition = new Vector3(- xScale * 130f, 0, 0);
                rectTransform.localScale = new Vector3(xScale, rectTransform.localScale.y, rectTransform.localScale.z);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            float xPos = rectTransform.localPosition.x;
            for (; xPos < 226; xPos += 10)
            {
                rectTransform.localPosition = new Vector3(xPos, rectTransform.localPosition.y);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private IEnumerator HideInfosCoroutine()
    {
        if (isInventory)
        {
            float xScale = rectTransform.localScale.x;
            for (; xScale >= 0; xScale -= 0.04f)
            {
                rectTransform.localPosition = new Vector3(-xScale * 100f, 0, 0);
                rectTransform.localScale = new Vector3(xScale, rectTransform.localScale.y, rectTransform.localScale.z);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            float xPos = rectTransform.localPosition.x;
            for (; xPos > 40; xPos -= 10)
            {
                rectTransform.localPosition = new Vector3(xPos, rectTransform.localPosition.y);
                yield return new WaitForEndOfFrame();
            }
        }
        
        infosTitle.gameObject.SetActive(false);
        infosText.gameObject.SetActive(false);
    }
}
