using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class M_EnemyHP_View : MonoBehaviour
{
    private M_Enemy_HP enemyHP;
    private Slider hpSlider;

    public void Setup(M_Enemy_HP enemyHP)
    {
        this.enemyHP = enemyHP;
        hpSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = enemyHP.CurrentHP / enemyHP.MaxHP;
    }
}
