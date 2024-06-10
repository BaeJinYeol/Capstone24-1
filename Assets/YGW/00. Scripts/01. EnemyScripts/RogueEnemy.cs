using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RogueEnemy : MonoBehaviour
{
    [Header("RogueLike-Enemy Information")]
    public string enemyName;
    public int    maxHp;
    public int    curHp;
    public int    atkDmg;
    public float  atkSpeed;
    public float  moveSpeed;
    public float  atkRange;
    public float  fieldOfVision;

    [Header("Enemy HP-Bar Information")]
    public float      height = 1.6f;
    RectTransform     hpBar;
    public GameObject prfHpBar;
    public GameObject canvas;

    public RoguePlayer player;
    Image curHpBar;

    [Header("HUD-Text")]
    public GameObject hudDamageText;
    public Transform  hudTextPos;

    public AudioClip die_sound;

    private void Update()
    {
        if(RogueSceneManager.Instance.isOver == true)
        {
            Destroy(hpBar.gameObject);
            Destroy(this);
        }
        else
        {
            Vector3 _hpBarPos =
    Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
            hpBar.position = _hpBarPos;
            curHpBar.fillAmount = (float)curHp / maxHp;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("RoguePlayerWeapon"))
    //    {
    //        if (player.stat.attacked)
    //        {
    //            curHp -= player.stat.attackDamage;
    //            player.AttackFalse();
    //            Debug.Log("Current Enemy-HP : " + curHp);

    //            if (curHp <= 0)
    //            {
    //                Destroy(gameObject);
    //                Destroy(hpBar.gameObject);
    //            }
    //        }
    //    }  
    //}

    public void TakeDamage(int damage)
    {
        GameObject hudText = Instantiate(hudDamageText);
        hudText.transform.position = hudTextPos.position;
        hudText.GetComponent<DamageText>().damage = damage;

        curHp -= damage;
        if (curHp <= 0)
        {
            RogueEnemyManager.Instance.DeclineEnemyCount();
            AudioManager.Instance.PlaySFXSound("die", die_sound);
            Destroy(hpBar.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void InitEnemyUI()
    {
        canvas = GameObject.Find("RogueCanvas");
        hpBar = Instantiate(prfHpBar, canvas.transform).GetComponent<RectTransform>();
        curHpBar = hpBar.transform.GetChild(0).GetComponent<Image>();
    }

    public void FindTargetPlayer()
    {
        GameObject obj = GameObject.Find("RoguePlayer");
        player = obj.GetComponentInChildren<RoguePlayer>();
    }
}
