using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoguePlayerUI : MonoBehaviour
{
    // RoguePlayerUI
    // 여기서는 무엇을 하나? 
    /*
        TODO:
            1. 게임 화면에서 유동적인 HP를 보여줄 것 
            2. 유동적인 HP에 따른 Text 수치를 보여줄 것
    */

    [SerializeField]
    private RoguePlayerStatus stat;
    public TextMeshProUGUI    text;

    Image curHpBar;

    private void Awake()
    {
        stat = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<RoguePlayerStatus>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        curHpBar = transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        text.text = stat.curHealth + " / " + stat.maxHealth;
        curHpBar.fillAmount = (float)stat.curHealth / stat.maxHealth;
    }
}
