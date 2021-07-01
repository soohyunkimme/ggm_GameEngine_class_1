using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab_1;

    public Transform spawnPoint;

    public bool isRoutine;

    public IEnumerator spawnCase;
    public void SetSpawn()
    {
        if (!isRoutine)
        {
            spawnCase = spawnCase_1();
            StartCoroutine(spawnCase);
            isRoutine = true;
        }
    }

    public void StopSpawn()
    {
        if (isRoutine)
        {
            StopCoroutine(spawnCase);
            isRoutine = false;
        }

    }

    public IEnumerator spawnCase_1()
    {
        while (true)
        {
            var enemy = Instantiate(enemyPrefab_1, spawnPoint.position, Quaternion.identity).GetComponent<Enemy>();
            enemy.InitEnemy();
            GameManager.instance.enemies.Add(enemy.gameObject);
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        }
    }
}
