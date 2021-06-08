using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICtrl : MonoBehaviour
{
    public GameObject ui;
    private AndroidJavaClass appClass;

    private string app = "ulw.ulw.ulw.App";

    // Start is called before the first frame update
    void Start()
    {
        appClass = new AndroidJavaClass(app);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnApplicationFocus(bool hasFocus)
    {
        bool active = appClass.GetStatic<bool>("ACT");
        ui.SetActive(active);
    }
}
