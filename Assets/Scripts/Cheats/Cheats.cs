using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            CheatCurrency(CurrencyTypes.Red, 100);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            CheatCurrency(CurrencyTypes.Yellow, 100);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            CheatCurrency(CurrencyTypes.Brown, 100);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            CheatCurrency(CurrencyTypes.Purple, 100);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            CheatCurrency(CurrencyTypes.Mana, 10);
        }
    }

    private void CheatCurrency(CurrencyTypes type, int amount)
    {
        CurrencyManager.Instance.UpdateCurrencyOverlay(type, amount);
    }
}
