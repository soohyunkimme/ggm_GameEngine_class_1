using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour, IDamageAble
{
    public float hp;
    private float damage;
    private float attackDelay;
    private float movespeed;

    private bool isAttack;
    private bool isDead;

    private Animator anim;

    [Header("공격 인식")]
    public LayerMask whatIsPlayer;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void InitEnemy()
    {
        hp = GameManager.instance.enemylevel * 20 + 20;
        damage = GameManager.instance.enemylevel * 14;
        attackDelay = 2f;
        movespeed = 1;
        isAttack = false;
        isDead = false;
        transform.position = new Vector3(transform.position.x, transform.position.y + Random.Range(-0.5f, 1f));
    }

    void Update()
    {
        if (isDead) return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 1.5f, whatIsPlayer);
        IEnumerator attack = Attack(hit);
        if(hit.collider != null && !isAttack)
        {
            anim.SetBool("isAttack", true);
            isAttack = true;
            StartCoroutine(attack);
        }
        else if(hit.collider == null && isAttack)
        {
            isAttack = false;
            anim.SetBool("isAttack", false);
            StopCoroutine(attack);
        }

        if (!isAttack)
        {
            transform.position = new Vector3(transform.position.x + -movespeed * Time.deltaTime, transform.position.y);
        }
    }

    IEnumerator Attack(RaycastHit2D hit)
    {
        while(isAttack)
        {
            anim.SetTrigger("OnAttack");
            yield return new WaitForSeconds(attackDelay);
            IDamageAble player = hit.transform.gameObject.GetComponent<IDamageAble>();
            player.OnDamage(damage);
        }
    }

    void IDamageAble.OnDamage(float damage)
    {
        if(isDead)
        {
            return;
        }

        hp -= damage;

        if(hp < 0)
        {
            isDead = true;
            Die();
        }
    }

    public void Die()
    {
        anim.SetTrigger("Die");;
        GameManager.instance.gold += GameManager.instance.enemylevel * 30;
        GameManager.instance.SetGold();
        GameManager.instance.enemies.Remove(this.gameObject);
        Destroy(this.gameObject, 0.4f);

    }
}
