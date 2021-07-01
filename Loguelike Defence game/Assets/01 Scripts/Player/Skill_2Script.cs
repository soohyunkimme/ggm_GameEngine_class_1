using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_2Script : MonoBehaviour
{  
    
    void Update()
    {
        transform.position = new Vector3(transform.position.x + (7 * Time.deltaTime), transform.position.y);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ENEMY")
        {
            IDamageAble e = collision.gameObject.GetComponent<IDamageAble>();
            if (e != null)
                e.OnDamage(25f + (GameManager.instance.skill_2Level * 25f));
        }
    }
}
