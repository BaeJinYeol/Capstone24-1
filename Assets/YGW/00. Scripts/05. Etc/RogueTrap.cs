using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueTrap : MonoBehaviour
{
    [SerializeField]
    private int trapDmg = 10;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = GameObject.Find("RoguePlayer").GetComponentInChildren<RoguePlayer>();
            player.TakeDamage(trapDmg);
        }
    }
}
