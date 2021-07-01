using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Animations;

public class Player : MonoBehaviour, IDamageAble
{
    private SpriteRenderer spriteRenderer;
    private PlayerAnimation playerAnimation;


    public float moveDirection { get; private set; }

    [Header("플레이어 스탯")]
    private float maxHP;
    public float currentHP;

    private float maxMana;
    public float currentmana;
    private float manaRegen;

    private float damage;
    private float moveSpeed;

    public Image HpGage;
    public Image ManaGage;

    private float skill_1Cost;
    private float skill_2Cost;
    private float skill_3Cost;

    [Header("스킬 영역지정")]
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public Transform pointD;
    public LayerMask whatIsEnemy;
    public GameObject skill2Prefab;

    public bool isSkill = false;
    public bool isDead = false;

    public void OnEnable()
    {
        InitPlayer();
    }

    private void InitPlayer()
    {
        gameObject.transform.position = new Vector2(-5.25f, 0.25f);

        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimation = GetComponent<PlayerAnimation>();
        maxHP = 500f + (GameManager.instance.hpLevel * 50f);

        maxMana = 100f + (GameManager.instance.maxManaLevel * 20f);

        manaRegen = Mathf.Pow(0.8f, GameManager.instance.manaRengen);

        damage = 23f + (GameManager.instance.damageLevel * 13f);

        moveSpeed = 3f + GameManager.instance.moveSpeed;

        skill_1Cost = Mathf.Ceil(5 * ((5 - GameManager.instance.costDown) / 5));
        skill_2Cost = Mathf.Ceil(20 * ((5 - GameManager.instance.costDown) / 5));
        skill_3Cost = Mathf.Ceil(50 * ((5 - GameManager.instance.costDown) / 5));

        currentHP = maxHP;
        currentmana = maxMana * 0.2f;
        HpGage.fillAmount = currentHP / maxHP;

        isSkill = false;
        isDead = false;
        StartCoroutine(Mana());
    }

    IEnumerator Mana()
    {
        while (true)
        {
            if (currentmana < maxMana)
            {
                currentmana++; ;
                ManaGage.fillAmount = currentmana / maxMana;
            }
            yield return new WaitForSeconds(manaRegen);
        }
    }

    private void Update()
    {
        if (isSkill) return;

        moveDirection = Input.GetAxis("Horizontal");
        if (moveDirection > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveDirection < 0)
        {
            spriteRenderer.flipX = true;
        }

        transform.position = new Vector2(moveDirection * moveSpeed * Time.deltaTime + transform.position.x, transform.position.y);
    }

    //플레이어 기술

    public void OnSkill1()
    {
        if (currentmana >= skill_1Cost)
        {
            spriteRenderer.flipX = false;
            currentmana -= skill_1Cost;
            ManaGage.fillAmount = currentmana / maxMana;
            playerAnimation.Skill1();
            Collider2D[] cols = Physics2D.OverlapAreaAll(pointA.position, pointB.position, whatIsEnemy);
            if (cols.Length >= 1)
            {
                foreach (Collider2D c in cols)
                {
                    IDamageAble enemy = c.gameObject.GetComponent<IDamageAble>();
                    if (enemy != null)
                    {
                        enemy.OnDamage(damage * GameManager.instance.skill_1Level);
                    }
                }
            }
        }
    }

    public void OnSkill2()
    {
        if (currentmana > skill_2Cost)
        {
            spriteRenderer.flipX = false;
            currentmana -= skill_2Cost;
            ManaGage.fillAmount = currentmana / maxMana;

            playerAnimation.Skill1();
            GameObject s = Instantiate(skill2Prefab, transform.position, Quaternion.identity);

            Destroy(s, 3f);
        }
    }

    public void OnSkill3()
    {
        if (currentmana > skill_3Cost)
        {
            spriteRenderer.flipX = false;
            currentmana -= skill_3Cost;
            ManaGage.fillAmount = currentmana / maxMana;

            playerAnimation.Skill3();
            Collider2D[] cols = Physics2D.OverlapAreaAll(pointC.position, pointD.position, whatIsEnemy);
            if (cols.Length >= 1)
            {
                foreach (Collider2D c in cols)
                {
                    IDamageAble enemy = c.gameObject.GetComponent<IDamageAble>();
                    if (enemy != null)
                    {
                        enemy.OnDamage(50f + (GameManager.instance.skill_3Level * 50));
                    }
                }
                if ((maxHP - currentHP) < cols.Length * 17f + (GameManager.instance.skill_3Level * 17f))
                    currentHP = maxHP;
                else
                    currentHP += cols.Length * (17f + (GameManager.instance.skill_3Level * 17f));
            }
            HpGage.fillAmount = currentHP / maxHP;
        }
    }

    //데미지 처리
    void IDamageAble.OnDamage(float damage)
    {
        currentHP -= damage;
        HpGage.fillAmount = currentHP / maxHP;
        if (currentHP < 0 && !isDead)
        {
            Die();
        }
    }

    public void Die()//scene 바꾸기 추가
    {
        isDead = true;

        playerAnimation.OnDie();
        GameManager.instance.SceneChange(1);
        Invoke("Activefalse", 2f);
    }

    private void Activefalse()
    {
        gameObject.SetActive(false);
    }
}
