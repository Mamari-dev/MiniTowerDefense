using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickUpgradeButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            OnShiftLeftClick();
        }
        else
        {
            OnLeftClick();
        }
    }

    private void OnLeftClick()
    {

    }

    private void OnShiftLeftClick()
    {

    }
}
