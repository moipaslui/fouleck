using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public float currentMoney;

    public TextMeshProUGUI moneyTextUI;

    private void Start()
    {
        UpdateUI();
    }

    public void AddMoney(float dif)
    {
        currentMoney += dif;
        if (currentMoney < 0)
            currentMoney = 0;
        UpdateUI();
    }

    public bool IsBuyable(Item item)
    {
        return (item.cost <= currentMoney);
    }

    private void UpdateUI()
    {
        moneyTextUI.text = "" + currentMoney;
    }
}
