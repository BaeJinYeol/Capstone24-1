using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Player Information
    public int atkDmg;
    public bool attacked = false;

    public float speed;
    public float jumpPower = 6.5f;
    private float moveX;
    private bool isJump = false;

    private Animator animator;
    private Rigidbody2D rigid;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();

        Attack();

        Jump();
    }

    private void Move()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        Vector3 moveVector = new Vector3(moveX, 0f, 0f);
        transform.Translate(moveVector.normalized * Time.deltaTime * speed);

        if (moveX > 0)
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }
        else if (moveX < 0) 
        {
            transform.localScale = new Vector3(2, 2, 2);
        }
        if (moveX != 0)
        {
            animator.SetFloat("RunState", speed * 0.1f);
        }
        else
        {
            animator.SetFloat("RunState", 0);
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("Attack");
        }
    }

    public void AttackTrue()
    {
        attacked = true;
    }

    public void AttackFalse()
    {
        attacked = false;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isJump)
        {
            isJump = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            rigid.gravityScale = 2;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            rigid.gravityScale = 1;
        }
    }
}
