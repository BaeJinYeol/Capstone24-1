using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueEnemyBehavior : MonoBehaviour
{
    public float      attackDelay;
    public Transform   target;
    private RogueEnemy enemy;
    private Animator   animator;

    private void Update()
    {
        Behave();   
    }

    public void InitComponent()
    {
        enemy = GetComponent<RogueEnemy>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Transform>();
    }

    private void Move2Target()  // 플레이어에게 이동
    {
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        transform.Translate(new Vector2(dir, 0) * enemy.moveSpeed * Time.deltaTime);
        animator.SetFloat("RunState", enemy.moveSpeed * 0.1f);
    }

    private void Face2Target()  // 플레이어 바라보기
    {
        if (target.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(2, 2, 2);
        }
        else
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        attackDelay = enemy.atkSpeed;
    }

    private void Behave()
    {
        attackDelay -= Time.deltaTime;
        if (attackDelay < 0) attackDelay = 0;

        float distance = Vector3.Distance(transform.position, target.position);

        if (attackDelay == 0 && distance <= enemy.fieldOfVision)
        {
            Face2Target();

            if (distance <= enemy.atkRange)
            {
                Attack();
            }
            else
            {
                Move2Target();
            }
        }
        else
        {
            animator.SetFloat("RunState", 0f);
        }
    }
}
