using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Player playermove;

    private readonly int hashMove = Animator.StringToHash("Move");
    private readonly int hashSkill1 = Animator.StringToHash("Skill1");
    private readonly int hashSkill3 = Animator.StringToHash("Skill3");
    private readonly int hashisDead = Animator.StringToHash("IsDead");
    private readonly int hashDie = Animator.StringToHash("Die");

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playermove = GetComponent<Player>();
    }

    private void Update()
    {
        anim.SetFloat(hashMove, Mathf.Abs(playermove.moveDirection));
    }

    public void Skill1()
    {
        anim.SetTrigger(hashSkill1);
    }
    public void Skill3()
    {
        anim.SetTrigger(hashSkill3);
    }
    public void OnDie()
    {
        anim.SetBool(hashisDead, true);
        anim.SetTrigger(hashDie);
    }
}
