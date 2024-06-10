using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RogueEnemyAi : MonoBehaviour
{
    public Transform target;
    float attackDelay;

    RogueEnemy  enemy;
    Animator    animator;
    Rigidbody2D rigid;
    public BoxCollider2D box;

    public AudioClip monster_sound;
    private bool monster_sfx;
    private GameObject monster_audio;

    private void Start()
    {
        monster_sfx = false;
    }

    private void Update()
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

                if (!monster_sfx)
                {
                    StartCoroutine("PlayerMonsterSound");
                    monster_sfx = true;
                }
                
            }
        }
        else
        {
            animator.SetFloat("RunState", 0f);
        }
    }

    IEnumerator PlayerMonsterSound()
    {
        monster_audio = AudioManager.Instance.PlaySFXSound("Monster", monster_sound);
        yield return new WaitForSeconds(6f);
        monster_sfx = false;
    }

    public void InitComponent()
    {
        enemy = GetComponent<RogueEnemy>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Transform>();
    }

    private void Move2Target()
    {
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        rigid.velocity = new Vector2(dir * enemy.moveSpeed, rigid.velocity.y);
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

    public void Enbox()
    {
        box.enabled = true;
    }

    public void Debox()
    {
        box.enabled = false;
    }

    private void OnDestroy()
    {
        Destroy(monster_audio);
    }
}
