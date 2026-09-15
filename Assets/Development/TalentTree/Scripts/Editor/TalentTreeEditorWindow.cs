using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class TalentTreeEditorWindow : EditorWindow
{
    private TalentTreeGraphView graphView;

    [MenuItem("Window/Talent Tree Editor")]
    public static void OpenWindow()
    {
        TalentTreeEditorWindow window = GetWindow<TalentTreeEditorWindow>();
        window.titleContent = new GUIContent("Talent Tree Editor");
    }

    private void OnEnable()
    {
        ConstructGraphView();
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }

    private void ConstructGraphView()
    {
        graphView = new TalentTreeGraphView();
        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);
    }

}
