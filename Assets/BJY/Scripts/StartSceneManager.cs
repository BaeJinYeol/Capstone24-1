using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public AudioClip click_sound;

    private GameObject setting_ui;

    [SerializeField]
    private GameObject load_btn;

    // Start is called before the first frame update
    void Start()
    {
        string path = Application.persistentDataPath + "/" + "userdata.json";
        if (File.Exists(path))
        {
            GameManager.Instance.is_save_data = true;
        }

        setting_ui = SettingManager.Instance.gameObject.transform.GetChild(0).gameObject;
        if (GameManager.Instance.is_save_data == false)
        {
            load_btn.SetActive(false);
        }
        else
        {
            load_btn.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            setting_ui.SetActive(false);
        }
    }

    public void ClickNewButton()
    {
        GameManager.Instance.NewGame();
        SceneManager.LoadScene(2);
    }

    public void ClickLoadButton()
    {
        GameManager.Instance.LoadGame();
        SceneManager.LoadScene(3);
    }

    public void ClickSettingButton()
    {
        setting_ui.SetActive(true);
    }

    public void PlayButtonSound()
    {
        AudioManager.Instance.PlaySFXSound("BtnClick", click_sound);
    }
    /// <summary>
    /// 게임종료. 전처리기를 이용해 에디터 아닐때 종료.
    /// </summary>
    public void gameExitBtn()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
