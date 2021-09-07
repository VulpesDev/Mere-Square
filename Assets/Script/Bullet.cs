using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 10000f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DieTime());
    }

    void FixedUpdate()
    {
        rb.velocity = speed * -transform.up * Time.fixedDeltaTime;
    }


    IEnumerator DieTime()
    {
        yield return new WaitForSeconds(1.25f);
        Destroy(gameObject);
    }
}
