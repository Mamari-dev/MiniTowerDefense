using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TowerCombat))]
public class TowerCombatEditor : Editor
{
    TowerCombat tower;
    Editor towerBaseEditor;
    Editor towerCombatEditor;

    private void OnEnable()
    {
        tower = (TowerCombat)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();  //standart Editor anzeige

        if (tower.TowerBaseStats != null && tower.TowerCombatStats != null && !Application.isPlaying)
            DrawEnemyValuesEditor(tower.TowerBaseStats, tower.TowerCombatStats, ref tower.foldout, ref towerBaseEditor, ref towerCombatEditor);
        else if (tower.TowerRunTimeCombatStats != null && Application.isPlaying)
            DrawEnemyValuesEditor(tower.TowerRunTimeBaseStats, tower.TowerRunTimeCombatStats, ref tower.foldout, ref towerBaseEditor, ref towerCombatEditor);
    }

    private void DrawEnemyValuesEditor(Object baseValues, Object combatValues, ref bool foldout, ref Editor baseEditor, ref Editor combatEditor)
    {
        foldout = EditorGUILayout.InspectorTitlebar(foldout, baseValues);    //erzeugt ein einklapbares Feld

        if (foldout)
        {
            if (baseValues != null && combatValues != null)
            {
                CreateCachedEditor(baseValues, null, ref baseEditor);   //editor wird erstellt
                baseEditor.OnInspectorGUI();                            //editor wird gezeichnet
                
                EditorGUILayout.Space(10);

                CreateCachedEditor(combatValues, null, ref combatEditor);
                combatEditor.OnInspectorGUI();                            //editor wird gezeichnet
            }
        }
    }
}