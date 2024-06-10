using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using KoreanTyper;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    private string[] dialog_set;
    public Sprite[] image_set;

    public float typing_speed;
    public TMP_Text text_window;
    public Image bg_image;

    public AudioClip click_sound;

    private int current_dialog_index = 0;
    private bool is_typing = false;
    private bool is_complete = false;

    public List<AudioClip> voice_clips;
    private GameObject voice_object;

    private void Start()
    {
        dialog_set = new string[]
        {
            "왕국력 243년 평화롭던 왕국에 마왕이 침공했다. \n마왕은 너무나도 강력하여 세계를 집어삼킬듯 하였지.",
            "하지만 왕국은 포기하지않고 마왕군과 맞서 싸웠다.",
            "용맹한 병사와 기사들이 싸웠지만 역부족이었지. \n많은 피난민들이 생겨나고 다들 도망치기에 바빴어.",
            "그러나 그런 암울한 상황속에도 아직 희망이 존재했다. \n여신의 선택을 받은 용사가 탄생했기 때문이다!",
            "왕국은 이제 마지막 영토만 남은 상황... \n용사여 마왕을 물리치고 왕국을 되찾아라!"
        };
        DisplayNextDialog();
    }

    public void ClickDialogueWindow()
    {
        if (is_typing)
        {
            is_complete = true;
        }
        else
        {
            DisplayNextDialog();
        }
    }

    IEnumerator TypingRoutine(string str)
    {
        is_typing = true;
        is_complete = false;
        text_window.text = "";

        int typing_length = str.GetTypingLength();

        for (int index = 0; index <= typing_length; index++)
        {
            text_window.text = str.Typing(index);
            if (is_complete)
            {
                text_window.text = str;     // 전체 문자열 출력
                break;
            }
            yield return new WaitForSeconds(typing_speed);
        }

        is_typing = false;
    }

    private void DisplayNextDialog()
    {
        if (current_dialog_index < dialog_set.Length)
        {
            switch (current_dialog_index)
            {
                case 0:
                    voice_object = AudioManager.Instance.PlayeVoice("voice1", voice_clips[0]);
                    bg_image.sprite = image_set[0];
                    break;
                case 1:
                    Destroy(voice_object);
                    voice_object = AudioManager.Instance.PlayeVoice("voice2", voice_clips[1]);
                    bg_image.sprite = image_set[1];
                    break;
                case 2:
                    Destroy(voice_object);
                    voice_object = AudioManager.Instance.PlayeVoice("voice3", voice_clips[2]);
                    bg_image.sprite = image_set[2];
                    break;
                case 3:
                    Destroy(voice_object);
                    voice_object = AudioManager.Instance.PlayeVoice("voice4", voice_clips[3]);
                    bg_image.sprite = image_set[3];
                    break;
                case 4:
                    Destroy(voice_object);
                    voice_object = AudioManager.Instance.PlayeVoice("voice5", voice_clips[4]);
                    bg_image.sprite = image_set[4];
                    break;
                default:
                    break;
            }
            StartCoroutine(TypingRoutine(dialog_set[current_dialog_index]));
            current_dialog_index++;
        }
        else
        {
            Debug.Log("대사 종료");
            SceneManager.LoadScene(3);
        }
    }

    public void PlayClickSound()
    {
        AudioManager.Instance.PlaySFXSound("DialogueClick", click_sound);
    }
}
