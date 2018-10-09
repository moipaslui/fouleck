using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int currentMoney;

    public TextMeshProUGUI moneyTextUI;

    private void Start()
    {
        UpdateUI();
    }

    public void addMoney(int dif)
    {
        currentMoney += dif;
        if (currentMoney < 0)
            currentMoney = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        moneyTextUI.text = "" + currentMoney;
    }
}
