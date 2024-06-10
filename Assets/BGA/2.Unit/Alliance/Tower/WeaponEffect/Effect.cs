using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private EffectMovement2D movement2D;
    private Transform target;
    private Animator animator;
    private float damage;

    public void Setup(Transform target, Animator animator, float damage)
    {
        movement2D = GetComponent<EffectMovement2D>();
        this.target = target;
        this.animator = animator;
        this.damage = damage;
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        if (collision.transform != target) return;
        //collision.GetComponent<M_Enemy>().OnDie();
        collision.GetComponent<M_Enemy_HP>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
