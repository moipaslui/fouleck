using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    #region Singleton

    public static MoneyManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of money manager found !");
            return;
        }

        instance = this;
    }

    #endregion

    public int currentMoney;

    public TextMeshProUGUI moneyTextUI;

    private void Start()
    {
        UpdateUI();
    }

    public void AddMoney(int dif)
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
