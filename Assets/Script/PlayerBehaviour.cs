using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    /*
     * 1. Movement /////
     * 2. Shooting //////
     * 3. Ammo
     * 4. Pick up ammo
     * 5. Health /////
     * 6. Polish
    */

    Rigidbody2D rb;
    [SerializeField]float speed = 2000;
    int ammo;
    public static int health = 100;
    GameObject bullet;

    Transform muzzle;

    [SerializeField]Slider SliderHP;

    void Start()
    {
        Score.score = 0;
        Score.time = 0;
        health = 100;
        rb = GetComponent<Rigidbody2D>();
        muzzle = transform.GetChild(0).GetChild(0).transform;
        bullet = Resources.Load<GameObject>("Bullet");
    }

    void FixedUpdate()
    {

        Movement();
        Rotate();

        if(Input.GetAxis("Fire1") == 1)
        {
            Shoot();
        }

        if (health <= 0)
        {
            Die();
        }

        if(health<=40 && !SFX.alert.GetComponent<AudioSource>().isPlaying)
        {
            StartCoroutine(Alert());
        }

        SliderHP.value = health;
        
    }
    IEnumerator Alert()
    {
        yield return new WaitForSeconds(0.5f);
        SFX.Play("alerthealth");
    }
    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime,
            vertical * speed * Time.fixedDeltaTime);
    }
    void Rotate()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition) -
       transform.position;
        float rotz = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotz + 90);
    }
    void Shoot()
    {
        Instantiate(bullet, muzzle.transform.position, muzzle.transform.rotation);
    }
    public void DealDamage(int damage)
    {
        health -= damage;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("EBullet"))
        {
            DealDamage(10);
        }
    }
    void Die()
    {
        if(Score.score > Score.bestScore)
        {
            Score.bestScore = Score.score;
        }
        if (Score.time > Score.bestTime)
        {
            Score.bestTime = Score.time;
        }
        SceneManager.LoadScene("Death");
        Destroy(gameObject);
    }
}
