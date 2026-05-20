using TMPro;
using UnityEngine;

public class TowerDescriptionUIScaler : MonoBehaviour
{
    private int maxLines = 2;
    [SerializeField] private TMP_Text tmp;

    private void Start()
    {
        tmp.GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        tmp.OnPreRenderText += LineLimit;
    }

    private void OnDisable()
    {
        tmp.OnPreRenderText -= LineLimit;
    }

    private void LineLimit(TMP_TextInfo textInfo)
    {
        if (textInfo.lineCount > maxLines)
            tmp.maxVisibleLines = maxLines;
        else
            tmp.maxVisibleLines = textInfo.lineCount;
    }
}
