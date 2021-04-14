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
	public GameObject[] backgrounds;
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
		backgrounds [bgIndex].SetActive (false);
		bgIndex++;
		if(bgIndex >= backgrounds.Length)
		{
			bgIndex = 0;
		}
		backgrounds [bgIndex].SetActive (true);
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
