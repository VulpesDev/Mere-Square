using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    /*
     * 1. Follow Player //
     * 2. Look at Player //
     * 3. Shoot/DealMelee
     * 4. Health          // 
     * 
    */

    GameObject player;

    Rigidbody2D rb;
    float speed = 170f;
    int health = 100;
    float knockback = 20f;

    void Start()
    {
        speed = Random.Range(130.0f, 200.0f);
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        FollowPlayer();
        LookAtPlayer();

        if (health <= 0) Die();
    }

    Vector2 target;
    void FollowPlayer()
    {
        target = player.transform.position - transform.position;
        rb.velocity += target.normalized * speed * Time.fixedDeltaTime;
    }
    void LookAtPlayer()
    {
        float rotz = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotz + 90);
    }
    void Die()
    {
        Instantiate(Resources.Load<GameObject>("DeadEnemy"), transform.position,
            (transform.rotation));
        Score.score += 1;
        GameObject.Find("Score(TMP)").GetComponent<TextMeshProUGUI>().text = "Score: " + Score.score;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamBehaviour>().TriggerShake
                   (0.1f, 3f, 2f);
        SFX.Play("explosion");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            player.GetComponent<PlayerBehaviour>().DealDamage(10);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            SFX.Play("snare");
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamBehaviour>().TriggerShake
                (0.1f, 0.8f, 2f);
            Vector2 direction = player.transform.position - transform.position;
            health -= 20;
            rb.AddForce(-direction.normalized * knockback, ForceMode2D.Impulse);
            Destroy(collider.gameObject);
        }
    }
}
