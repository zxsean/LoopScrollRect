using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(LoopScrollRect), true)]
public class LoopScrollRectInspector : Editor
{
    int index = 0;
    float speed = 1000;
	public override void OnInspectorGUI ()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();

        LoopScrollRect scroll = (LoopScrollRect)target;

        if (GUILayout.Button("Create PoolObject"))
        {
            var _tf = scroll.transform.Find(LoopScrollPrefabSource.__POOLSNAME);

            if (!_tf)
            {
                GameObject _gameObject = new GameObject(LoopScrollPrefabSource.__POOLSNAME);

                _gameObject.transform.SetParent(scroll.transform);
                _gameObject.transform.localScale = Vector3.one;
                _gameObject.transform.localPosition = Vector3.zero;

                _tf = _gameObject.transform;
            }

            scroll.prefabSource.PoolGameObject = _tf;

            Debug.Log("Create Suc!");
        }

        GUI.enabled = Application.isPlaying;

        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Clear"))
        {
            scroll.ClearCells();
        }
        if (GUILayout.Button("Refresh"))
        {
            scroll.RefreshCells();
		}
		if(GUILayout.Button("Refill"))
		{
			scroll.RefillCells();
		}
		if(GUILayout.Button("RefillFromEnd"))
		{
			scroll.RefillCellsFromEnd();
		}
        EditorGUILayout.EndHorizontal();

        EditorGUIUtility.labelWidth = 45;
        float w = (EditorGUIUtility.currentViewWidth - 100) / 2;
        EditorGUILayout.BeginHorizontal();
        index = EditorGUILayout.IntField("Index", index, GUILayout.Width(w));
        speed = EditorGUILayout.FloatField("Speed", speed, GUILayout.Width(w));
        if(GUILayout.Button("Scroll", GUILayout.Width(45)))
        {
            scroll.SrollToCell(index, speed);
        }
        EditorGUILayout.EndHorizontal();
	}
}