using UnityEngine;
using UnityEngine.UI;

public abstract class TowerAttackController : MonoBehaviour
{
    [SerializeField] protected Color clickedTypeButtonBaseColor;
    [SerializeField] protected Color clickedTypeButtonClickedColor;

    protected TowerCombatStats towerCombatStats;
    protected Button lastClickedButton;

    private void Awake()
    {
        towerCombatStats = GetComponentInParent<TowerCombat>().TowerRunTimeCombatStats;
    }

    private void Start()
    {
        InitStartColor();
    }

    public void OnAttackTypesButton(Button clickedButton)
    {
        if (lastClickedButton != null)
            ChangeButtonColor(lastClickedButton, clickedTypeButtonBaseColor);

        lastClickedButton = clickedButton;
        ChangeButtonColor(lastClickedButton, clickedTypeButtonClickedColor);

        SetAttackTarget();
    }

    protected void ChangeButtonColor(Button button, Color color)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = color;
        colors.selectedColor = color;

        button.colors = colors;
    }

    protected abstract void SetAttackTarget();

    protected abstract void InitStartColor();
}
