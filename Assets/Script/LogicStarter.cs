using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicStarter : MonoBehaviour
{
    //spawn around the map and delete self (not only collider)  /////
    static GameObject player;
    static GameObject[] enemies;
    static GameObject[] spawners;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player")) return;
        Deactivate();
        SceneManager.LoadScene("Logic", LoadSceneMode.Additive);
        
    }
    void Deactivate()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach (GameObject spawn in spawners)
        {
            spawn.SetActive(false);
        }
        transform.position = new Vector2(Random.Range(-155.0f, 155.0f), Random.Range(130.0f, -130f));
    }
    public static void Activate()
    {
        player.SetActive(true);
        foreach (GameObject spawn in spawners)
        {
            spawn.SetActive(true);
        }
    }

}
