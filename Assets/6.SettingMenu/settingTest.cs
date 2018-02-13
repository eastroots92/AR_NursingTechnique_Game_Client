using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingTest : MonoBehaviour {
    public GameObject settingMenu;

    private void Start()
    {
        settingMenu.SetActive(false);
    }

    public void OnSettingButton()
    {
        settingMenu.SetActive(true);
    }
}
