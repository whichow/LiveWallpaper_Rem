using System;
using System.Collections;
using System.Collections.Generic;
using LostPolygon.uLiveWallpaper;
using UnityEngine;
using UnityEngine.UI;

public class MyGameSettings : MonoBehaviour 
{
	// public AudioListener listener;
	public new Light light;
	public Texture[] background;
	public GameObject[] effects;
	public MeshRenderer bgRender;
	public GameObject settingsUI;
	public Button volumeButton;
	public Button lightButton;
	public Button bgButton;
	public bool debug;

	private int bgIndex;

	public void ToggleVolume()
	{
		// listener.enabled = !listener.enabled;
		AudioListener.pause = !AudioListener.pause;
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
		if(bgIndex == 5 || bgIndex == 6)
		{
			effects[0].SetActive(true);
			effects[1].SetActive(false);
		}
		else if(bgIndex == 2 || bgIndex == 4)
		{
			effects[1].SetActive(true);
			effects[0].SetActive(false);
		}
		else
		{
			effects[0].SetActive(false);
			effects[1].SetActive(false);
		}
		bgRender.material.mainTexture = background[bgIndex];
	}

	void OnGUI()
	{
		if(debug)
		{
			if(GUILayout.Button("Voice"))
			{
				ToggleVolume();
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

#if UNITY_ANDROID
	private void OnEnable() {
		LiveWallpaper.IsPreviewChanged += OnPreviewChanged;
		volumeButton.onClick.AddListener(ToggleVolume);
		lightButton.onClick.AddListener(ToggleLight);
		bgButton.onClick.AddListener(ChangeBackground);
	}

	private void OnDisable() {
		LiveWallpaper.IsPreviewChanged -= OnPreviewChanged;
		volumeButton.onClick.RemoveListener(ToggleVolume);
		lightButton.onClick.RemoveListener(ToggleLight);
		bgButton.onClick.RemoveListener(ChangeBackground);
	}

	private void OnPreviewChanged(bool isPreview)
	{
		if(isPreview)
		{
			settingsUI.gameObject.SetActive(true);
		}
		else
		{
			settingsUI.gameObject.SetActive(false);
		}
	}
#endif
}
