using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoguePlayerUI : MonoBehaviour
{
    // RoguePlayerUI
    // ���⼭�� ������ �ϳ�? 
    /*
        TODO:
            1. ���� ȭ�鿡�� �������� HP�� ������ �� 
            2. �������� HP�� ���� Text ��ġ�� ������ ��
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
