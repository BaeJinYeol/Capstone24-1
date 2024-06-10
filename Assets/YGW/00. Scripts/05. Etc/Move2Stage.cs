using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move2Stage : MonoBehaviour
{
    bool state = false;
    public bool isClaer = false;
    GameObject text;

    private void Awake()
    {
        text = transform.GetChild(0).gameObject;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Scene curScene = SceneManager.GetActiveScene();

        if (collision.CompareTag("Player") && RogueEnemyManager.Instance.GetEnemyCount() == 0)
        {
            // TODO: Door 오브젝트 아래에 TextMeshPro 출력
            state = true;
            if (state)
            {
                text.SetActive(true);
            }

            if (curScene.name.Equals("YGW1"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    SceneManager.LoadScene("RogueStage01_1");
                }
            }

            if (curScene.name.Equals("RogueStage01_1"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    SceneManager.LoadScene("RogueStage01_2");
                }
            }

            if (curScene.name.Equals("RogueStage01_2"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    isClaer = true;
                    RogueSceneManager.Instance.ClearRogue(1);
                }
            }

            // 로그라이크 2-1 이동
            if (curScene.name.Equals("YGW2"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    SceneManager.LoadScene("RogueStage02_1");
                }
            }

            if (curScene.name.Equals("RogueStage02_1"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    SceneManager.LoadScene("RogueStage02_2");
                }
            }

            if (curScene.name.Equals("RogueStage02_2"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    isClaer = true;
                    RogueSceneManager.Instance.ClearRogue(2);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            state = false;
            text.SetActive(false);
        }
    }
}
