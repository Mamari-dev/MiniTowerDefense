using UnityEngine;
using UnityEngine.UI;

public class SpeedController : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color pressedColor;

    private readonly int startGameSpeed = 1;

    private void Start()
    {
        ChangeColors(startGameSpeed);
    }

    public void OnClick(int gameSpeed)
    {
        ChangeSpeed(gameSpeed);
        ChangeColors(gameSpeed);
    }

    private void ChangeSpeed(int gameSpeed)
    {
        Time.timeScale = gameSpeed;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    /// <summary>
    /// gameSpeed is used as Index.
    /// pause button is speed 0 is first button in array and so on
    /// </summary>
    /// <param name="gameSpeed"></param>
    private void ChangeColors(int gameSpeed)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == gameSpeed)
            {
                buttons[i].colors = ChangeButtonColor(buttons[i].colors, pressedColor);
            }
            else
            {
                buttons[i].colors = ChangeButtonColor(buttons[i].colors, normalColor);
            }
        }
    }

    private ColorBlock ChangeButtonColor(ColorBlock cb, Color newColor)
    {
        cb.normalColor = newColor;
        cb.selectedColor = newColor;
        cb.highlightedColor = new Color(newColor.r, newColor.g, newColor.b, 0.25f);

        return cb;
    }
}
