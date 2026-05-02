using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    Enemy enemy;
    Editor dwarfEditor;

    private void OnEnable()
    {
        enemy = (Enemy)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();  //standart Editor anzeige

        if (enemy.Stats != null && !Application.isPlaying)
            DrawEnemyValuesEditor(enemy.Stats, ref enemy.foldout, ref dwarfEditor);
        else if (enemy.CopyStats != null && Application.isPlaying)
            DrawEnemyRunTimeValuesEditor(enemy.CopyStats, ref enemy.foldout, ref dwarfEditor);
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
