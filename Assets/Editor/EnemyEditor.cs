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

        if (enemy.BaseStats != null && !Application.isPlaying)
            DrawEnemyValuesEditor(enemy.BaseStats, ref enemy.foldout, ref dwarfEditor);
        else if (enemy.CopyStats != null && Application.isPlaying)
            DrawEnemyValuesEditor(enemy.CopyStats, ref enemy.foldout, ref dwarfEditor);
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
}
