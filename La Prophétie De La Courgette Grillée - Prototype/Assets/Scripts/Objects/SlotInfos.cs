using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SlotInfos : MonoBehaviour
{
    private RectTransform rectTransform;
    public TextMeshProUGUI infosText;
    public TextMeshProUGUI infosTitle;

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
            infosText.text += "\nDamage : " + ((Weapon)itemToBuy).damage;
        if (itemToBuy.GetType() == typeof(Ingredient))
            infosText.text += "\nQualite : " + ((Ingredient)itemToBuy).quality;
        if (itemToBuy.GetType() == typeof(Repas))
            infosText.text += "\nSoin : " + ((Repas)itemToBuy).heal;
        infosText.text += "\nPrix : " + itemToBuy.cost;

        float xPos = rectTransform.localPosition.x;
        for (; xPos < 226; xPos += 10)
        {
            rectTransform.localPosition = new Vector3(xPos, -27, 0);
            yield return null;
        }
    }

    private IEnumerator HideInfosCoroutine()
    {
        float xPos = rectTransform.localPosition.x;
        for (; xPos > 40; xPos -= 10)
        {
            rectTransform.localPosition = new Vector3(xPos, -27, 0);
            yield return null;
        }
        
        infosTitle.gameObject.SetActive(false);
        infosText.gameObject.SetActive(false);
    }
}
