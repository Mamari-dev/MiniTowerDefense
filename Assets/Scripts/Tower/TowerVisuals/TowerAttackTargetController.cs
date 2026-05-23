using UnityEngine;
using UnityEngine.UI;

public class TowerAttackTargetController : TowerAttackController
{
    [SerializeField] private TowerAttackTargetStruct[] attackTargetButtons;

    protected override void SetAttackTarget()
    {
        foreach (var targetStruct in attackTargetButtons)
        {
            if (targetStruct.button == lastClickedButton)
            {
                towerCombatStats.attackTargetTypes = targetStruct.target;
                break;
            }
        }
    }

    protected override void InitStartColor()
    {
        foreach (var targetStruct in attackTargetButtons)
        {
            if (targetStruct.target == towerCombatStats.attackTargetTypes)
            {
                ChangeButtonColor(targetStruct.button, clickedTypeButtonClickedColor);
                lastClickedButton = targetStruct.button;
                break;
            }
        }
    }
}
