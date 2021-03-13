using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameSettings : MonoBehaviour 
{
	public AudioListener listener;
	public new Light light;
	public Material[] background;
	public MeshRenderer bgRender;
	public bool debug;

	private int bgIndex;

	public void ToggleVoice()
	{
		listener.enabled = !listener.enabled;
	}

	public void ToggleLight()
	{
		light.enabled = !light.enabled;
	}

	public void ChangeBackground()
	{
		bgIndex++;
		if(bgIndex >= background.Length)
		{
			bgIndex = 0;
		}
		bgRender.material = background[bgIndex];
	}

	void OnGUI()
	{
		if(debug)
		{
			if(GUILayout.Button("Voice"))
			{
				ToggleVoice();
			}
			if(GUILayout.Button("Light"))
			{
				ToggleLight();
			}
			if(GUILayout.Button("Background"))
			{
				ChangeBackground();
			}
		}
	}
}
