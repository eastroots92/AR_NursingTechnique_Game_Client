using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

//setting option을 담을 클래스
[Serializable]
public class SettingsOption
{
    public bool arToggleValue;
    public bool soundToggleValue;
    public int qualityValue;
    public float volumeValue;
    
    public SettingsOption(bool arToggleValue, bool soundToggleValue, int qualityValue, float volumeValue)
    {
        this.arToggleValue = arToggleValue;
        this.soundToggleValue = soundToggleValue;
        this.qualityValue = qualityValue;
        this.volumeValue = volumeValue;
    }
}

/// <summary>
/// 게임 내 옵션을 변경하고 그 값을 local 환경에 저장하여 차후에도 유지
/// 조절 가능 옵션은 AR On/Off, Sound On/Off, Graphic Quality, Volume
/// Apply Button을 클릭하기 전까지 변경사항을 저장하지 않는다
/// </summary>
public class SettingMenuController : ViewController
{
    [SerializeField] private GameObject settingMenu;        //setting menu 게임오브젝트
    [SerializeField] private AudioMixer audioMixer;         //앱 내부의 AudioMixer
    [SerializeField] private Toggle arToggle;               //AR Toggle UI
    [SerializeField] private Toggle soundToggle;            //Sound Toggle UI
    [SerializeField] private Dropdown qualityDropdown;      //QualitySettings을 조절하는 Dropdown UI
    [SerializeField] private Slider volumeSlider;           //AudioMixer 값을 조절하는 Slider

    private SettingsOption loadSettingsOption = null;
    private SettingsOption settingsOption = null;
    private SettingsOption curSettingsOption = null;
    private bool isOnAR = true;                            //AR 기능의 On/Off 여부
    private bool isOnSound = true;
    private bool isApplyButton = false;

    private void Awake()
    {
        //저장된 SettingsOption이 있을 때 Game Setting
        if (LoadSettings())
        {
            isOnAR = loadSettingsOption.arToggleValue;
            isOnSound = loadSettingsOption.soundToggleValue;

            QualitySettings.SetQualityLevel(loadSettingsOption.qualityValue);

            if (loadSettingsOption.soundToggleValue)
                audioMixer.SetFloat("volume", loadSettingsOption.volumeValue);
            else
                audioMixer.SetFloat("volume", -80);
        }
        //저장된 SettingsOption이 없을 때 Game Setting
        else
        {
            isOnAR = true;
            isOnSound = true;
            QualitySettings.SetQualityLevel(2);
            audioMixer.SetFloat("volume", 20);
        }

        float value;
        audioMixer.GetFloat("volume", out value);
        //현재 Game Setting을 담을 객체 생성
        curSettingsOption = new SettingsOption(isOnAR, isOnSound, QualitySettings.GetQualityLevel(), value);
    }

    //UI 출력시 UI 내용 변경
    private void OnEnable()
    {
        float value;
        audioMixer.GetFloat("volume", out value);
        //변경 사항을 담을 객체 생성
        settingsOption = new SettingsOption(isOnAR, isOnSound, QualitySettings.GetQualityLevel(), value);

        UpdateContent(curSettingsOption);
    }

    //Settings View 내용을 갱신
    public void UpdateContent(SettingsOption option)
    {
        arToggle.isOn = curSettingsOption.arToggleValue;
        soundToggle.isOn = curSettingsOption.soundToggleValue;
        qualityDropdown.value = curSettingsOption.qualityValue;
        volumeSlider.value = curSettingsOption.volumeValue;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        settingsOption.volumeValue = volume;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        settingsOption.qualityValue = qualityIndex;
    }

    public void SetAR(bool isSetAR)
    {
        settingsOption.arToggleValue = isSetAR;
        isOnAR = isSetAR;
    }

    public void SetSound(bool isSetSound)
    {
        settingsOption.soundToggleValue = isSetSound;
        isOnSound = isSetSound;

        if (isSetSound)
        {
            volumeSlider.interactable = true;
            audioMixer.SetFloat("volume", settingsOption.volumeValue);
        }
        else
        {
            volumeSlider.interactable = false;
            audioMixer.SetFloat("volume", -80);
        }
    }

    public void OnPressResetButton()
    {
        settingsOption.arToggleValue = arToggle.isOn = true;
        settingsOption.soundToggleValue = soundToggle.isOn = true;
        settingsOption.qualityValue = qualityDropdown.value = 2;
        settingsOption.volumeValue = volumeSlider.value = 20;

        QualitySettings.SetQualityLevel(2);
        audioMixer.SetFloat("volume", 20);
    }

    public void OnPressApplyButton()
    {
        isApplyButton = true;
        SaveSettings();
    }

    public void OnPressXButton()
    {
        if(!isApplyButton)
        {
            isOnAR = curSettingsOption.arToggleValue;
            isOnSound = curSettingsOption.soundToggleValue;
            QualitySettings.SetQualityLevel(curSettingsOption.qualityValue);
            audioMixer.SetFloat("volume", curSettingsOption.volumeValue);
        }
        else
        {
            curSettingsOption.arToggleValue = settingsOption.arToggleValue;
            curSettingsOption.soundToggleValue = settingsOption.soundToggleValue;
            curSettingsOption.qualityValue = settingsOption.qualityValue;
            curSettingsOption.volumeValue = settingsOption.volumeValue;
        }

        settingMenu.SetActive(false);
    }

    //settingsOoption 저장
    private void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(settingsOption, true);
        PlayerPrefs.SetString("SavedSettings", jsonData);
    }

    //저장된 데이터 유무 확인
    private bool LoadSettings()
    {
        if (!PlayerPrefs.HasKey("SavedSettings"))
            return false;
        else
        {
            //저장된 값을 파싱해 loadSettingsOption 객체에 담는다.
            string loadData = PlayerPrefs.GetString("SavedSettings");
            loadSettingsOption = JsonUtility.FromJson<SettingsOption>(loadData);
            return true;
        }
    }
}
