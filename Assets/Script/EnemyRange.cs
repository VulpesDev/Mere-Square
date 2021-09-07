using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyRange : MonoBehaviour
{
    // Make bullets do damage
    // Only stop if in the game area

    GameObject player;

    Rigidbody2D rb;
    float speed = 170f;
    int health = 100;
    float knockback = 20f;

    bool found = false;
    bool running = false;
    bool docked = false;

    GameObject bullet;

    Transform muzzle;

    void Start()
    {
        speed = Random.Range(130.0f, 200.0f);
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        bullet = Resources.Load<GameObject>("EBullet");
        muzzle = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        target = player.transform.position - transform.position;
        if (Vector2.Distance(player.transform.position, transform.position) < 100)
        {
            docked = true;
        }
        if(!docked)FollowPlayer();
        LookAtPlayer();
        if (docked && !running) StartCoroutine(Shootings());

        if (health <= 0) Die();

        if (transform.position.x <= target.x && transform.position.y == target.y)
        {
            docked = true;
            rb.velocity = Vector2.zero;
        }
    }


    Vector2 target;
    void FollowPlayer()
    {
        rb.velocity += target.normalized * speed * Time.fixedDeltaTime;
    }
    void LookAtPlayer()
    {
        float rotz = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotz + 90);
    }
    void Shoot()
    {
        Instantiate(bullet, muzzle.transform.position, muzzle.transform.rotation);
    }
    IEnumerator Shootings()
    {
        running = true;
        yield return new WaitForSeconds(Random.Range(0.2f, 1.1f));
        Shoot();
        running = false;
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
