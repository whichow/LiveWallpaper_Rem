using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGameSettings : MonoBehaviour 
{
	private static readonly string VOLUME = "VOLUME";
	private static readonly string LIGHT = "LIGHT";
	private static readonly string BG_INDEX = "BG_INDEX";

	// public AudioListener listener;
	public new Light light;
	public GameObject[] backgrounds;
	public GameObject settingsUI;
	public Button previewButton;
	public ToggleButton volumeButton;
	public ToggleButton lightButton;
	public Button bgButton;
	public bool debug;

	private int bgIndex;

	public void ToggleVolume()
	{
		// listener.enabled = !listener.enabled;
		AudioListener.volume = AudioListener.volume > 0.5f ? 1f : 0f;
		PlayerPrefs.SetFloat(VOLUME, AudioListener.volume);
	}

	public void ToggleLight()
	{
		light.enabled = !light.enabled;
		PlayerPrefs.SetInt(LIGHT, light.enabled ? 1 : 0);
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
		PlayerPrefs.SetInt(BG_INDEX, bgIndex);
	}

	void OnApplicationFocus(bool hasFocus)
	{
		if(hasFocus)
		{
			volumeButton.isOn = PlayerPrefs.GetFloat(VOLUME, 1f) > 0.5f;
			lightButton.isOn = PlayerPrefs.GetInt(LIGHT, 1) == 1;
			bgIndex = PlayerPrefs.GetInt(BG_INDEX, 0);
			backgrounds [bgIndex].SetActive (true);
		}
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

	private void OnEnable() {
		volumeButton.onClick.AddListener(ToggleVolume);
		lightButton.onClick.AddListener(ToggleLight);
		bgButton.onClick.AddListener(ChangeBackground);
#if UNITY_ANDROID
		previewButton.onClick.AddListener(PreviewWallpaper);
#endif
	}

	private void OnDisable() {
		volumeButton.onClick.RemoveListener(ToggleVolume);
		lightButton.onClick.RemoveListener(ToggleLight);
		bgButton.onClick.RemoveListener(ChangeBackground);
#if UNITY_ANDROID
		previewButton.onClick.RemoveListener(PreviewWallpaper);
#endif
	}

#if UNITY_ANDROID
	class ActiveCallback : AndroidJavaProxy
    {
		public delegate void ActiveEventHandler(bool active);
		public event ActiveEventHandler activeEventHandler;

        public ActiveCallback() : base("com.whichow.remlive.UnityPlayerProxy$ActivityActiveListener") {}
        private void onActivityActive(bool active)
        {
            activeEventHandler.Invoke(active);
        }
    }

	private AndroidJavaClass LiveWallpaperManager;
	private AndroidJavaClass UnityPlayerProxy;

	void Awake()
	{
		try
		{
			LiveWallpaperManager = new AndroidJavaClass("com.whichow.remlive.LiveWallpaperManager");
			// bool isPreview = LiveWallpaperManager.CallStatic<bool>("isPreview");
			// if(isPreview)
			// {
			// 	settingsUI.gameObject.SetActive(false);
			// }
			// else
			// {
			// 	settingsUI.gameObject.SetActive(true);
			// }
		}
		catch(System.Exception e)
		{
			Debug.LogError(e);
		}

		try
		{
			UnityPlayerProxy = new AndroidJavaClass("com.whichow.remlive.UnityPlayerProxy");
			ActiveCallback callback = new ActiveCallback();
			callback.activeEventHandler += OnActivityActive;
			UnityPlayerProxy.CallStatic("setActivityActiveListener", callback);
		}
		catch(System.Exception e)
		{
			Debug.LogError(e);
		}
	}

    private void OnActivityActive(bool active)
    {
        if(active)
		{
			settingsUI.gameObject.SetActive(true);
		}
		else
		{
			settingsUI.gameObject.SetActive(false);
		}
    }

    private void PreviewWallpaper()
	{
		try
		{
			LiveWallpaperManager.CallStatic("previewWallpaper");
		}
		catch (System.Exception e)
		{
			Debug.LogError(e);
		}
	}
#endif
}
