using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : Singleton<SettingManager>
{
    FullScreenMode screenMode;
    public Dropdown resoltionDropdown;
    public Toggle fullscreenToggle;
    List<Resolution> resolutions = new List<Resolution>();
    int resolutionNum;

    public AudioClip btn_clip;

    // Start is called before the first frame update
    void Start()
    {
        InitUI();
    }

    void InitUI()
    {
        //for (int i = 0; i < Screen.resolutions.Length; i++)
        //{
        //    if (Screen.resolutions[i].refreshRate == 60)   60hzÀÎ°Í¸¸ Ãß°¡
        //        resolutions.Add(Screen.resolutions[i]);
        //}
        resolutions.AddRange(Screen.resolutions);
        resoltionDropdown.options.Clear();

        int optionNum = 0;
        foreach (Resolution item in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + "x" + item.height + " " + (int)item.refreshRateRatio.value + "Hz";
            resoltionDropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
                resoltionDropdown.value = optionNum;
            optionNum++;
        }
        resoltionDropdown.RefreshShownValue();

        fullscreenToggle.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;

        //GameObject.Find("BgSoundInputField").GetComponent<InputField>().text = ((int)(AudioManager.Instance.bgVolume * 100)).ToString();
        //GameObject.Find("SfxSoundInputField").GetComponent<InputField>().text = ((int)(AudioManager.Instance.sfxVolume * 100)).ToString();
        //GameObject.Find("BgSoundSlider").GetComponent<Slider>().value = AudioManager.Instance.bgVolume;
        //GameObject.Find("SfxSoundSlider").GetComponent<Slider>().value = AudioManager.Instance.sfxVolume;
    }

    public void ChangeDropboxOption(int value)
    {
        resolutionNum = value;
    }

    public void OnClickFullScreenToggle()
    {
        // Screen.fullScreen = !Screen.fullScreen;
        screenMode = fullscreenToggle.isOn ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void OnClickOk()
    {
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode);
    }

    public void ChangeBgVolume(bool flag)
    {
        InputField bgInputField = GameObject.Find("BgSoundInputField").GetComponent<InputField>();
        if (flag)
        {
            float bgValue = AudioManager.Instance.bgVolume;
            bgInputField.text = ((int)(bgValue * 100)).ToString();
            return;
        }
        Slider bgSlider = GameObject.Find("BgSoundSlider").GetComponent<Slider>();
        bgSlider.value = float.Parse(bgInputField.text) / 100;
    }
    public void ChangeSfxVolume(bool flag)
    {
        InputField sfxInputField = GameObject.Find("SfxSoundInputField").GetComponent<InputField>();
        if (flag)
        {
            float sfxValue = AudioManager.Instance.sfxVolume;
            sfxInputField.text = ((int)(sfxValue * 100)).ToString();
            return;
        }
        Slider sfxSlider = GameObject.Find("SfxSoundSlider").GetComponent<Slider>();
        sfxSlider.value = float.Parse(sfxInputField.text) / 100;
    }

    public void ClickBtn()
    {
        AudioManager.Instance.PlaySFXSound("Button", btn_clip);
    }
}
