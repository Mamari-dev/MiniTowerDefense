using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tower))]
public class TowerBaseEditor : Editor
{
    Tower tower;
    Editor towerEditor;

    private void OnEnable()
    {
        tower = (Tower)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();  //standart Editor anzeige

        if (tower.TowerBaseStats != null && !Application.isPlaying)
            DrawEnemyValuesEditor(tower.TowerBaseStats, ref tower.foldout, ref towerEditor);
        else if (tower.TowerRunTimeBaseStats != null && Application.isPlaying)
            DrawEnemyValuesEditor(tower.TowerRunTimeBaseStats, ref tower.foldout, ref towerEditor);
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
