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
	public Button volumeButton;
	public Button lightButton;
	public Button bgButton;
	public bool debug;

	private int bgIndex;

	public void ToggleVolume()
	{
		// listener.enabled = !listener.enabled;
		AudioListener.volume = AudioListener.volume > 0.5f ? 0f : 1f;
		volumeButton.GetComponentInChildren<Text>().text = AudioListener.volume > 0.5f ? "声音开" : "声音关";
		PlayerPrefs.SetFloat(VOLUME, AudioListener.volume);
	}

	public void ToggleLight()
	{
		light.enabled = !light.enabled;
		lightButton.GetComponentInChildren<Text>().text = light.enabled ? "灯光开" : "灯光关";
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

	void Start()
	{
		volumeButton.GetComponentInChildren<Text>().text = PlayerPrefs.GetFloat(VOLUME, 1f) > 0.5f ? "声音开" : "声音关";
		lightButton.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(LIGHT, 1) == 1 ? "灯光开" : "灯光关";
		bgIndex = PlayerPrefs.GetInt(BG_INDEX, 0);
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

	private void OnEnable() {
		volumeButton.onClick.AddListener(ToggleVolume);
		lightButton.onClick.AddListener(ToggleLight);
		bgButton.onClick.AddListener(ChangeBackground);
#if UNITY_ANDROID && !UNITY_EDITOR
		previewButton.onClick.AddListener(PreviewWallpaper);
#endif
	}

	private void OnDisable() {
		volumeButton.onClick.RemoveListener(ToggleVolume);
		lightButton.onClick.RemoveListener(ToggleLight);
		bgButton.onClick.RemoveListener(ChangeBackground);
#if UNITY_ANDROID && !UNITY_EDITOR
		previewButton.onClick.RemoveListener(PreviewWallpaper);
#endif
	}

#if UNITY_ANDROID && !UNITY_EDITOR
    private AndroidJavaClass appClass;
    private AndroidJavaClass activityClass;
    private AndroidJavaClass wrapperClass;
    private AndroidJavaObject wrapperObject;

    private string activity = "ulw.ulw.ulw.UnityPlayerActivity";
    private string wrapper = "ulw.ulw.ulw.Wrapper";
	private string app = "ulw.ulw.ulw.App";

    void Awake()
    {
		appClass = new AndroidJavaClass(app);
        activityClass = new AndroidJavaClass(activity);
        wrapperClass = new AndroidJavaClass(wrapper);
        wrapperObject = wrapperClass.CallStatic<AndroidJavaObject>("instance");
    }

    public void PreviewWallpaper()
    {
        activityClass.CallStatic("StartService");
    }

    public void Wrapper()
    {
        wrapperObject.Call("Start");
    }

    void OnApplicationFocus(bool hasFocus)
    {
        bool active = appClass.GetStatic<bool>("ACT");
        if(active)
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
