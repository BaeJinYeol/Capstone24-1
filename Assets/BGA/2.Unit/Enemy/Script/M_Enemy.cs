using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyDestroyType {Kill = 0, Arrive};

public class M_Enemy : MonoBehaviour
{
    private Animator unit;
    private int wayPointCount;
    private Transform[] wayPoints;
    private int currentIndex = 0;
    private M_Enemy_Movement movement;
    private EnemySpawner enemySpawner;
    [SerializeField]
    private int gold = 10;

    public AudioClip die_clip;

    private void Awake()
    {
        unit = GetComponentInChildren<Animator>();
    }

    public void Setup(EnemySpawner enemySpawner,Transform[] wayPoints)
    {
        movement = GetComponent<M_Enemy_Movement>();
        this.enemySpawner = enemySpawner;

        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        transform.position = wayPoints[currentIndex].position;
        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove()
    {
        NextMoveTo();
        float before = Vector3.Distance(transform.position, wayPoints[currentIndex].position);
        while (true) {
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement.MoveSpeed)
            {
                NextMoveTo();
            }
            else if(before - Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0)
            {
                NextMoveTo();
            }
            before = Vector3.Distance(transform.position, wayPoints[currentIndex].position);
            yield return null;
        }   
    }

    private void NextMoveTo()
    {
        if(currentIndex < wayPointCount - 1)
        {
            transform.position = wayPoints[currentIndex].position;
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement.MoveTo(direction);
        }
        else {
            gold = 0;
            OnDie(EnemyDestroyType.Arrive);
        }
    }

    public void OnDie(EnemyDestroyType type)
    {
        AudioManager.Instance.PlaySFXSound("die", die_clip);
        movement.MoveTo(new Vector3(0, 0, 0));
        unit.SetFloat("RunState", 0.0f);
        unit.SetTrigger("Die");
        StartCoroutine(Die(type));
    }

    private IEnumerator Die(EnemyDestroyType type)
    {
        yield return new WaitForSeconds(1.0f);
        enemySpawner.DestroyEnemy(type, this, gold);
    }
}
