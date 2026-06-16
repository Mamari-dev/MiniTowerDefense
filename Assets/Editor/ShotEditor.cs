using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Shot), true)]
public class ShotEditor : Editor
{
    Shot shot;
    Editor shotBaseEditor;
    Editor shotCombatEditor;

    private void OnEnable()
    {
        shot = (Shot)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();  //standart Editor anzeige

        if (shot.BaseStats != null && !Application.isPlaying)
            DrawEnemyValuesEditor(shot.BaseStats, ref shot.foldout, ref shotBaseEditor, ref shotCombatEditor);
        else if (shot.RunTimeStats != null && Application.isPlaying)
            DrawEnemyValuesEditor(shot.RunTimeStats, ref shot.foldout, ref shotBaseEditor, ref shotCombatEditor);
    }

    private void DrawEnemyValuesEditor(Object baseValues, ref bool foldout, ref Editor baseEditor, ref Editor combatEditor)
    {
        foldout = EditorGUILayout.InspectorTitlebar(foldout, baseValues);    //erzeugt ein einklapbares Feld

        if (foldout)
        {
            if (baseValues != null)
            {
                CreateCachedEditor(baseValues, null, ref baseEditor);   //editor wird erstellt
                baseEditor.OnInspectorGUI();                            //editor wird gezeichnet
            }
        }
    }
}