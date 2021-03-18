using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(ToggleButton))]
public class ToggleButtonEditor : ButtonEditor 
{
    public override void OnInspectorGUI()
    {
		ToggleButton toggleButton = (ToggleButton)target;
		toggleButton.onSprite = (Sprite)EditorGUILayout.ObjectField("On Sprite", toggleButton.onSprite, typeof(Sprite));
		toggleButton.offSprite = (Sprite)EditorGUILayout.ObjectField("Off Sprite", toggleButton.offSprite, typeof(Sprite));
		toggleButton.isOn = EditorGUILayout.Toggle("Is On", toggleButton.isOn);
		base.OnInspectorGUI();
    }
}
