using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public GameObject option_ui;
    public GameObject inven_ui;
    public GameObject stage_ui;
    public TMP_Text gold_text;
    public Stage current_stage;

    private GameObject setting_ui;

    private bool is_option_ui;
    private bool is_setting_ui;
    public bool is_inven_ui;
    public bool is_stage_ui;

    public AudioClip button_click_clip;
    public AudioClip equip_click_clip;

    public Transform status;

    void Start()
    {
        Time.timeScale = 1f;

        is_option_ui = false;
        is_inven_ui = false;
        is_stage_ui = false;
        option_ui.SetActive(false);
        inven_ui.SetActive(false);
        stage_ui.SetActive(false);

        setting_ui = SettingManager.Instance.gameObject.transform.GetChild(0).gameObject;
        is_setting_ui = false;
            
        UpdateGold();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (is_inven_ui == false)
            {
                if (is_setting_ui == true)
                {
                    setting_ui.SetActive(false);
                    is_setting_ui = false;
                }
                else
                {
                    option_ui.SetActive(!is_option_ui);
                    is_option_ui = !is_option_ui;
                }
            }
            else
            {
                inven_ui.SetActive(false);
                is_inven_ui = false;
            }
        }
    }

    public void ClickInvenButton()
    {
        inven_ui.SetActive(true);
        is_inven_ui = true;
    }
    public void ClickSaveButton()
    {
        DataManager.Instance.JsonSave();
        Debug.Log("ÀúÀå ¹öÆ°");
    }
    public void ClickHomeButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ClickInvenBackButton()
    {
        inven_ui.SetActive(false);
        is_inven_ui = false;
    }

    public void ClickSettingButton()
    {
        setting_ui.SetActive(true);
        is_setting_ui = true;
    }

    public void UpdateGold()
    {
        gold_text.text = GameManager.Instance.player_gold.ToString();
    }

    public void ClickStartButton()
    {
        switch (current_stage.stage_id)
        {
            case 0:
                break;
            case 1:
                SceneManager.LoadScene("DMap1");
                break;
            case 2:
                SceneManager.LoadScene("DMap2");
                break;
            case 3:
                SceneManager.LoadScene("DMap3");
                break;
            default:
                break;
        }

        switch (current_stage.stage_name.text)
        {
            case "¿þ¼Ò½ºÀÇ ½£":
                SceneManager.LoadScene("YGW1");
                break;
            case "Çï¾ÆÀÇ ½£":
                SceneManager.LoadScene("YGW2");
                break;
            default :
                break;
        }
    }

    public void TestRogue()
    {
        SceneManager.LoadScene(4);
    }

    public void PlaySfxSound(bool flag)
    {
        if (flag)
        {
            AudioManager.Instance.PlaySFXSound("BtnSound", button_click_clip);
        }
        else
        {
            AudioManager.Instance.PlaySFXSound("EquipSound", equip_click_clip);
        }
    }
}
