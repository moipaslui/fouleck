using UnityEditor;
using UnityEngine;

public class Popup : PopupWindowContent
{
	GameObject gameObject;
	TRIGGER_TYPES triggerType;
	public override Vector2 GetWindowSize()
    {
        return new Vector2(300, 85);
    }
    public override void OnGUI(Rect rect)
    {
        GUILayout.Label("Créer un noeud (trigger) :", EditorStyles.boldLabel);
		gameObject = (GameObject)EditorGUILayout.ObjectField("GameObject", gameObject, typeof(GameObject), true);
		triggerType = (TRIGGER_TYPES)EditorGUILayout.EnumPopup("Type du trigger :", triggerType);
		if(GUILayout.Button("Créer le trigger") && gameObject != null)
		{
			QuestEditor.CreateNode(gameObject, triggerType);
		}
    }
}
