using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private TextMeshProUGUI result;
    [SerializeField]
    private PlayerHP playerHP;

    public AudioClip button_click_clip;

    private void Awake()
    {
        panel.SetActive(false);
        result.gameObject.SetActive(false);
    }

    private void Start()
    {
        panel.SetActive(false);
        result.gameObject.SetActive(false);
    }

    public void GameResult(bool clear)
    {
        if (clear)
        {
            result.text = "Clear";
            if (SceneManager.GetActiveScene().name == "DMap1")
            {
                GameManager.Instance.clear_zone[1] = true;
            }
            else if (SceneManager.GetActiveScene().name == "DMap2")
            {
                GameManager.Instance.clear_zone[2] = true;
            }
            else if (SceneManager.GetActiveScene().name == "DMap2")
            {
                GameManager.Instance.clear_zone[3] = true;
            }
        }
        else
        {
            result.text = "Fail";
        }
        panel.SetActive(true);
        result.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoWorldmap()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void PlaySfxSound()
    {
        AudioManager.Instance.PlaySFXSound("BtnSound", button_click_clip);
    }
}
