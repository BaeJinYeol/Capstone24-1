using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Enemy_Movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    public float MoveSpeed => moveSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
        if(direction.x < 0)
        {
            transform.localScale = new Vector3(2, 2, 2);
            
        }
        else if(direction.x > 0)
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }
    }
}
