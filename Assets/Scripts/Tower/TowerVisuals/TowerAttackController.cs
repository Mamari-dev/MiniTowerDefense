using UnityEngine;
using UnityEngine.UI;

public abstract class TowerAttackController : MonoBehaviour
{
    [SerializeField] protected Color clickedTypeButtonBaseColor;
    [SerializeField] protected Color clickedTypeButtonClickedColor;

    protected TowerCombatShootingStats towerCombatShootingStats;
    protected Button lastClickedButton;

    private void Awake()
    {
        TowerCombatShooting towerCombatShooting = GetComponentInParent<TowerCombatShooting>();
        if (towerCombatShooting != null)
            towerCombatShootingStats = towerCombatShooting.TowerRunTimeCombatStats;
    }

    private void Start()
    {
        if (towerCombatShootingStats == null) return;
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
