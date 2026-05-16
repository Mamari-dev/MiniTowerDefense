using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private GameObject currencyPrefab;
    [SerializeField] private CurrencyStruct[] currencys;
    private List<CurrencyOverlay> currencyOverlays = new();

    public static CurrencyManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        InitCurrency(CurrencyTypes.Mana);
    }

    private CurrencyOverlay InitCurrency(CurrencyTypes type)
    {
        foreach (CurrencyStruct currency in currencys)
        {
            if (currency.Type != type) continue;

            CurrencyOverlay newOverlay = Instantiate(currencyPrefab, transform).GetComponent<CurrencyOverlay>(); ;

            newOverlay.InitOverlay(currency);
            currencyOverlays.Add(newOverlay);

            return newOverlay;
        }

        return null;
    }

    public void UpdateCurrencyOverlay(CurrencyTypes type, int amount)
    {
        CurrencyOverlay currencyOverlay = currencyOverlays.Find(c => c.CurrencyType == type);
        currencyOverlay = (currencyOverlay != null) ? currencyOverlay : InitCurrency(type);

        if (currencyOverlay == null) return;

        currencyOverlay.AddCurrency(amount);
    }

    public bool CheckCurrencyAmount(CurrencyTypes type, int amount)
    {
        CurrencyOverlay overlay = currencyOverlays.Find(c => c.CurrencyType == type);

        if (overlay == null) return false;

        if (overlay.CurrentCurrency < amount) return false;

        return true;
    }

    public void BuyTower(CurrencyTypes type, int amount)
    {
        CurrencyOverlay overlay = currencyOverlays.Find(c => c.CurrencyType == type);
        overlay.RemoveCurrency(amount);
    }

    public void RefreshMana()
    {
        CurrencyOverlay overlay = currencyOverlays.Find(c => c.CurrencyType == CurrencyTypes.Mana);
        overlay.SetMaxCurrency();
    }
}
