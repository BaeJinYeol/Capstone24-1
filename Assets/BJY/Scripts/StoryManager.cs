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
            "�ձ��� 243�� ��ȭ�Ӵ� �ձ��� ������ ħ���ߴ�. \n������ �ʹ����� �����Ͽ� ���踦 �����ų�� �Ͽ���.",
            "������ �ձ��� ���������ʰ� ���ձ��� �¼� �ο���.",
            "����� ����� ������ �ο����� �������̾���. \n���� �ǳ��ε��� ���ܳ��� �ٵ� ����ġ�⿡ �ٻ���.",
            "�׷��� �׷� �Ͽ��� ��Ȳ�ӿ��� ���� ����� �����ߴ�. \n������ ������ ���� ��簡 ź���߱� �����̴�!",
            "�ձ��� ���� ������ ���丸 ���� ��Ȳ... \n��翩 ������ ����ġ�� �ձ��� ��ã�ƶ�!"
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
                text_window.text = str;     // ��ü ���ڿ� ���
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
            Debug.Log("��� ����");
            SceneManager.LoadScene(3);
        }
    }

    public void PlayClickSound()
    {
        AudioManager.Instance.PlaySFXSound("DialogueClick", click_sound);
    }
}
