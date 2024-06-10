using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoguePlayer : MonoBehaviour
{
    public RoguePlayerStatus stat;
    private Rigidbody2D      rigid;
    private Animator         animator;

    float moveX;
    public Transform colliderPos;
    public Vector2   boxSize;

    public GameObject root;

    public AudioClip attack_sound;
    public AudioClip jump_sound;
    public AudioClip land_sound;
    public AudioClip foot_sound;
    public float stepInterval = 0.3f;
    private float stepTimer;

    SPUM_SpriteList playerSprite;

    private void Awake()
    {
        LoadSprite();

        stat = GetComponent<RoguePlayerStatus>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        stepTimer = 0;

        // 마우스 커서를 숨깁니다.
        Cursor.visible = false;

        // 마우스 커서를 화면 중앙에 고정시킵니다.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(10);
        }

        Move();
        Jump();
        Attack();
    }

    private void LoadSprite()
    {
        playerSprite = root.GetComponent<SPUM_SpriteList>();

        PlayerInfoManager playerinfo = PlayerInfoManager.Instance;
        if (playerinfo.helmet.hp != 0)
            SetSprite(playerinfo.helmet.type, playerinfo.helmet, true);

        if (playerinfo.armor.hp != 0)
            SetSprite(playerinfo.armor.type, playerinfo.armor, true);

        if (playerinfo.back.hp != 0)
            SetSprite(playerinfo.back.type, playerinfo.back, true);

        if (playerinfo.weapon.power != 0)
            SetSprite(playerinfo.weapon.type, playerinfo.weapon, true);

        if (playerinfo.sub_weapon.hp != 0)
            SetSprite(playerinfo.sub_weapon.type, playerinfo.sub_weapon, true);

        if (playerinfo.hair.icon_path != null)
            SetSprite(playerinfo.hair.type, playerinfo.hair, true);

        if (playerinfo.face_hair.icon_path != null)
            SetSprite(playerinfo.face_hair.type, playerinfo.face_hair, true);

        if (playerinfo.cloth.icon_path != null)
            SetSprite(playerinfo.cloth.type, playerinfo.cloth, true);

        if (playerinfo.feet.icon_path != null)
            SetSprite(playerinfo.feet.type, playerinfo.feet, true);
    }

    void Move()
    {
        if (stat.curHealth <= 0) return;

        moveX = Input.GetAxisRaw("Horizontal");

        Vector3 moveVector = new Vector3(moveX, 0f, 0f);
        transform.Translate(moveVector.normalized * Time.deltaTime * stat.moveSpeed);
        
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
            if (stat.jumped == false)
            {
                stepTimer -= Time.deltaTime;
                if (stepTimer <= 0f)
                {
                    AudioManager.Instance.PlaySFXSound("FootStep", foot_sound);
                    stepTimer = stepInterval;
                }
            }
            else
            {
                stepTimer = 0f;
            }
            animator.SetFloat("RunState", stat.moveSpeed * 0.1f);
        }
        else
        {
            animator.SetFloat("RunState", 0f);
        }
    }

    void Attack()
    {
        if (stat.curHealth <= 0) return;

        if (stat.curTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(colliderPos.position, boxSize, 0);
                foreach (Collider2D item in collider2Ds)
                {
                    if (item.CompareTag("Monster"))
                    {
                        item.GetComponent<RogueEnemy>().TakeDamage(stat.attackDamage);
                    }
                }

                animator.SetTrigger("Attack");
                stat.curTime = stat.coolTime;

                AudioManager.Instance.PlaySFXSound("Attack", attack_sound);
            }
        }
        else
        {
            stat.curTime -= Time.deltaTime;
        }
    }

    void Jump()
    {
        if (stat.curHealth <= 0) return;

        if (Input.GetKeyDown(KeyCode.C) && !stat.jumped)
        {
            stat.jumped = true;
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector3.up * stat.jumpPower, ForceMode2D.Impulse);
            AudioManager.Instance.PlaySFXSound("jump", jump_sound);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") ||
            collision.gameObject.CompareTag("Monster"))
        {
            if (stat.jumped == true)
            {
                AudioManager.Instance.PlaySFXSound("Landing", land_sound);
            }
            stat.jumped = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttack"))
        {
            TakeDamage(collision.gameObject.GetComponentInParent<RogueEnemy>().atkDmg);
        }
    }

    public void AttackTrue()
    {
        stat.attacked = true;
    }

    public void AttackFalse()
    {
        stat.attacked = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(colliderPos.position, boxSize);
    }

    // TODO: 플레이어 피격 함수
    public void TakeDamage(int damage)
    {
        if (!stat.isHurt)
        {
            stat.isHurt = true;
            stat.curHealth -= damage;

            if (stat.curHealth <= 0)
            {
                // TODO: Player Dead
                animator.SetTrigger("Die");
                Invoke("CallFailRouge", 1f);
            }
            else
            {
                // TODO: Player Hurt Motion

                StartCoroutine(HurtRoutine());
                StartCoroutine(Blink());
            }

        }

    }

    private void CallFailRouge()
    {
        RogueSceneManager.Instance.FailRogue();
    }

    IEnumerator HurtRoutine()
    {
        Debug.Log("Player is Hitted");
        yield return new WaitForSeconds(1.2f);
        stat.isHurt = false;
    }

    IEnumerator Blink()
    {
        var childRender = GetComponentsInChildren<SpriteRenderer>();

        while (stat.isHurt)
        {
            yield return new WaitForSeconds(0.1f);
            foreach (var child in childRender)
            {
                child.color = new Color(1, 1, 1, 0);
            }

            yield return new WaitForSeconds(0.1f);
            foreach (var child in childRender)
            {
                child.color = new Color(1, 1, 1, 1);
            }
        }
    }

    public void SetSprite(ItemType type, Item item, bool flag)
    {
        Transform root_transform = root.transform;
        playerSprite = root_transform.GetComponent<SPUM_SpriteList>();
        if(type == ItemType.Armor && item.hp == 0)
        {
            return;
        }

        switch (type)
        {
            case ItemType.Armor:
                Debug.Log("armor");
                Sprite[] armor_sprites = Resources.LoadAll<Sprite>(item.icon_path);
                if (flag)
                {
                    if (armor_sprites.Length > 1)
                    {
                        playerSprite._armorList[0].sprite = armor_sprites[0];
                        playerSprite._armorList[1].sprite = armor_sprites[1];
                        playerSprite._armorList[2].sprite = armor_sprites[2];
                    }
                    else
                    {
                        playerSprite._armorList[0].sprite = armor_sprites[0];
                        playerSprite._armorList[1].sprite = null;
                        playerSprite._armorList[2].sprite = null;
                    }
                }
                else
                {
                    playerSprite._armorList[0].sprite = null;
                    playerSprite._armorList[1].sprite = null;
                    playerSprite._armorList[2].sprite = null;
                }
                break;
            case ItemType.Back:
                if (flag)
                {
                    playerSprite._backList[0].sprite = item.icon;
                }
                else
                {
                    playerSprite._backList[0].sprite = null;
                }
                break;
            case ItemType.Cloth:
                Sprite[] cloth_sprites = Resources.LoadAll<Sprite>(item.icon_path);
                if (flag)
                {
                    if (cloth_sprites.Length > 1)
                    {
                        playerSprite._clothList[0].sprite = cloth_sprites[0];
                        playerSprite._clothList[1].sprite = cloth_sprites[1];
                        playerSprite._clothList[2].sprite = cloth_sprites[2];
                    }
                    else
                    {
                        playerSprite._clothList[0].sprite = cloth_sprites[0];
                        playerSprite._clothList[1].sprite = null;
                        playerSprite._clothList[2].sprite = null;
                    }
                }
                else
                {
                    playerSprite._clothList[0].sprite = null;
                    playerSprite._clothList[1].sprite = null;
                    playerSprite._clothList[2].sprite = null;
                }
                break;
            case ItemType.Hair:
                if (flag)
                {
                    playerSprite._hairList[0].sprite = item.icon;
                }
                else
                {
                    playerSprite._hairList[0].sprite = null;
                }
                break;
            case ItemType.FaceHair:
                if (flag)
                {
                    playerSprite._hairList[3].sprite = item.icon;
                }
                else
                {
                    playerSprite._hairList[3].sprite = null;
                }
                break;
            case ItemType.Foot:
                Sprite[] foot_sprites = Resources.LoadAll<Sprite>(item.icon_path);
                if (flag)
                {
                    playerSprite._pantList[0].sprite = foot_sprites[0];
                    playerSprite._pantList[1].sprite = foot_sprites[1];
                }
                else
                {
                    playerSprite._pantList[0].sprite = null;
                    playerSprite._pantList[1].sprite = null;
                }
                break;
            case ItemType.Helmet:
                if (flag)
                {
                    playerSprite._hairList[1].sprite = item.icon;
                }
                else
                {
                    playerSprite._hairList[1].sprite = null;
                }
                break;
            case ItemType.Weapon:
                if (flag)
                {
                    playerSprite._weaponList[0].sprite = item.icon;
                }
                else
                {
                    playerSprite._weaponList[0].sprite = null;
                }
                break;
            case ItemType.SubWeapon:
                if (flag)
                {
                    playerSprite._weaponList[3].sprite = item.icon;
                }
                else
                {
                    playerSprite._weaponList[3].sprite = null;
                }
                break;
        }
    }
}
