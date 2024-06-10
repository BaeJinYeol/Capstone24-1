using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Enemy_HP : MonoBehaviour
{
    [SerializeField]
    private float maxHP;
    private float currentHP;
    private bool isDie = false;
    private M_Enemy enemy;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    void Awake()
    {
        currentHP = maxHP;
        enemy = GetComponent<M_Enemy>();
    }

    public void TakeDamage(float damage)
    {
        if (isDie == true) return;
        currentHP -= damage;

        if(currentHP <= 0)
        {
            isDie = false;
            enemy.OnDie(EnemyDestroyType.Kill);
        }
    }
}
