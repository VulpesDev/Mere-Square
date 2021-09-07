using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //fix not spawning after the puzzle ////
    int currentLimit = 10;
    bool running = false;
    private void Start()
    {
        running = false;
    }
    private void Update()
    {
        if(!running)
        {
            StartCoroutine(Spawner());
        }
    }
    IEnumerator Spawner()
    {
        running = true;
        Debug.Log("Spawn");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        yield return new WaitForSeconds(Random.Range(1.01f, 2.05f));
        if (enemies.Length <= currentLimit)
        {
            int i = Random.Range(0, 2);
            switch(i)
            {
                case 0:
                    Instantiate(Resources.Load<GameObject>("Enemy"), transform.position,
            transform.rotation);
                    break;
                case 1:
                    Instantiate(Resources.Load<GameObject>("EnemyRange"), transform.position,
            transform.rotation);
                    break;
            }
            
        }
        running = false;
    }
    private void OnEnable()
    {
        running = false;
    }
}
