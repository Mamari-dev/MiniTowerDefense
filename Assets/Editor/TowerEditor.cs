using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tower_SingleAttack))]
public class TowerEditor : Editor
{
    Tower_SingleAttack tower;
    Editor dwarfEditor;

    private void OnEnable()
    {
        tower = (Tower_SingleAttack)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();  //standart Editor anzeige

        if (tower.TowerStats != null && !Application.isPlaying)
            DrawEnemyValuesEditor(tower.TowerStats, ref tower.foldout, ref dwarfEditor);
        else if (tower.TowerRunTimeStats != null && Application.isPlaying)
            DrawEnemyRunTimeValuesEditor(tower.TowerRunTimeStats, ref tower.foldout, ref dwarfEditor);
    }

    private void DrawEnemyValuesEditor(Object baseValues, ref bool foldout, ref Editor editor)
    {
        foldout = EditorGUILayout.InspectorTitlebar(foldout, baseValues);    //erzeugt ein einklapbares Feld

        if (foldout)
        {
            if (baseValues != null)
            {
                CreateCachedEditor(baseValues, null, ref editor);   //editor wird erstellt

                editor.OnInspectorGUI();                            //editor wird gezeichnet
            }
        }
    }

    private void DrawEnemyRunTimeValuesEditor(Object baseValues, ref bool foldout, ref Editor editor)
    {
        foldout = EditorGUILayout.InspectorTitlebar(foldout, baseValues);    //erzeugt ein einklapbares Feld

        if (foldout)
        {
            if (baseValues != null)
            {
                CreateCachedEditor(baseValues, null, ref editor);   //editor wird erstellt

                editor.OnInspectorGUI();                            //editor wird gezeichnet
            }
        }
    }
}
