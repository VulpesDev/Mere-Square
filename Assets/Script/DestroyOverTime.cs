using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeleteSelf());
    }
    IEnumerator DeleteSelf()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
