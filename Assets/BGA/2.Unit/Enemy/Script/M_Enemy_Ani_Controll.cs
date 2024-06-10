using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Enemy_Ani_Controll : MonoBehaviour
{
    private Animator unit01;
    private bool stun = false;

    private void Awake()
    {
        unit01 = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!stun)
        {
            unit01.SetFloat("RunState", (float)0.5);
        }
        else
        {
            unit01.SetFloat("RunState", (float)1);
        }
        unit01.SetBool("Run", true);
    }
}
