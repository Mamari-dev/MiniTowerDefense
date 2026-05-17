using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyOverlay : MonoBehaviour
{
    [SerializeField] private Image currencyImage;
    [SerializeField] private TextMeshProUGUI currencyAmount;
    private CurrencyTypes currencyType;
    private int currentCurrency;
    private int maxCurrency;

    public CurrencyTypes CurrencyType { get => currencyType; private set => currencyType = value; }
    public int CurrentCurrency { get => currentCurrency; private set => currentCurrency = value; }
    public int MaxCurrency { get => maxCurrency; private set =>  maxCurrency = value; }

    public void InitOverlay(CurrencyStruct currency)
    {
        currencyType = currency.Type;
        currencyImage.sprite = currency.Sprite;
        currencyImage.color = currency.Color;
        currentCurrency = currency.Amount;
        currencyAmount.text = currency.Amount.ToString();
        maxCurrency = (currency.MaxAmount == 0) ? int.MaxValue : currency.MaxAmount;
    }

    public void SetMaxCurrency()
    {
        currentCurrency = maxCurrency;
        currencyAmount.text = maxCurrency.ToString();
    }

    public void AddCurrency(int amount)
    {
        currentCurrency += amount;
        currencyAmount.text = currentCurrency.ToString();
    }

    public void RemoveCurrency(int amount)
    {
        currentCurrency -= amount;
        currencyAmount.text = currentCurrency.ToString();
    }
}
