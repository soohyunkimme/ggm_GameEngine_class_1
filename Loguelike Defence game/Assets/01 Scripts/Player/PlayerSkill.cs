using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    private int skill_1Count;
    public LayerMask whatIsEnemy;

    [Header("스킬 범위")]
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public Transform pointD;

    [Header("버튼")]
    public Button skill_1;
    public Button skill_2;
    public Button skill_3;

    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void OnSkill1(float damage)
    {
        Collider2D[] cols = Physics2D.OverlapAreaAll(pointA.position, pointB.position, whatIsEnemy);
        if (cols.Length >= 1)
        {
            foreach (Collider2D c in cols)
            {
                IDamageAble enemy = c.gameObject.GetComponent<IDamageAble>();
                if (enemy != null)
                {
                    enemy.OnDamage(damage);
                }
            }
        }
        StartCoroutine(CoolTime(0.5f));
    }

    public void OnSkill3(float damage)
    {
        Collider2D[] cols = Physics2D.OverlapAreaAll(pointC.position, pointD.position, whatIsEnemy);
        if (cols.Length >= 1)
        {
            foreach (Collider2D c in cols)
            {
                IDamageAble enemy = c.gameObject.GetComponent<IDamageAble>();
                if (enemy != null)
                {
                    enemy.OnDamage(damage);
                }
            }
            player.currentHP += damage * 10;
        }
        StartCoroutine(CoolTime(0.6f));
    }

    IEnumerator CoolTime(float coolTime)
    {
        float time = 0;
        skill_1.interactable = false;
        while (time > coolTime)
        {
            time += Time.deltaTime;
        }
        skill_1.interactable = true;
        player.isSkill = true;
        yield return null;
    }
}
