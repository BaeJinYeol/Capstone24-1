using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int stage_id;
    public bool stage_clear;
    public string stage_name;
    public string stage_level;
    public string stage_info;

    // 0: 타워 디펜스, 1: 로그라이크, 2: 보스 로그라이크
    public int stage_type;

    public Image flag;

    public Sprite green_flag;
    public Sprite red_flag;
    public Sprite stage_image;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (stage_type == 1)
            return;
        flag.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (stage_type == 1)
            return;
        flag.gameObject.SetActive(false);
    }

    void Start()
    {
        if (stage_type == 1)
            return;

        stage_clear = GameManager.Instance.clear_zone[stage_id];
        if (stage_type == 2)
        {
            if (stage_clear == true)
            {
                Color color = gameObject.GetComponent<Image>().color;
                color.a = 0.5f;
                gameObject.GetComponent<Image>().color = color;
            }
            return;
        }

        if (stage_clear == true)
        {
            flag.sprite = green_flag;
        }
        else
        {
            flag.sprite = red_flag;
        }
    }

    public void StageButton()
    {
        if (stage_clear) return;
        Stage stagePanel = GameObject.Find("Canvas").transform.GetChild(5).GetComponent<Stage>();
        stagePanel.SetStage(stage_id ,stage_name, stage_level, stage_info, stage_image);
        stagePanel.gameObject.SetActive(true);
    }
}
