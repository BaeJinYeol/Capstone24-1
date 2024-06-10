using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("Dash");
        }
    }
}
