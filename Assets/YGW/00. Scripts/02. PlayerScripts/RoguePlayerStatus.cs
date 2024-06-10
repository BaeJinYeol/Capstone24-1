using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoguePlayerStatus : MonoBehaviour
{
    [Header("Player Status")]
    public int   maxHealth;
    public int   curHealth;
    public int   attackDamage;
    public float attackSpeed;
    public float moveSpeed;
    public float jumpPower;
    public float critical;
    public float curTime;
    public float coolTime = 0.5f;

    [Header("Player Behavior")]
    public bool attacked;
    public bool jumped = false;
    public bool isHurt = false;

    private void Awake()
    {
        //playerStatusSetting(100, 10, 1f, 7.5f, 15f, 0.05f);
        PlayerInfoManager playerInfo = PlayerInfoManager.Instance;
        playerStatusSetting(playerInfo.player_hp, playerInfo.player_power, playerInfo.player_attack_speed * 0.5f,
            playerInfo.player_speed * 0.55f, 15f, playerInfo.player_critical_rate / 100f);
    }

    void playerStatusSetting(int maxHealth, 
                             int attackDamage, 
                             float attackSpeed, 
                             float moveSpeed,
                             float jumpPower,
                             float critical)
    {
        this.maxHealth    = maxHealth;
        this.curHealth    = maxHealth;
        this.attackDamage = attackDamage;
        this.attackSpeed  = attackSpeed;
        this.moveSpeed    = moveSpeed;
        this.jumpPower    = jumpPower;
        this.critical     = critical;
    }
}
