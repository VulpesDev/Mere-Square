using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    static Transform expl, snare, acc, decl;
    public static Transform alert;
    void Start()
    {
        expl = transform.GetChild(0);
        snare = transform.GetChild(1);
        alert = transform.GetChild(2);
        acc = transform.GetChild(3);
        decl = transform.GetChild(4);
    }

    void Update()
    {
        
    }
    public static void Play(string sound)
    {
        switch(sound)
        {
            case "explosion":
                expl.GetChild(Random.Range(0, expl.childCount)).GetComponent<AudioSource>().Play();
                break;
            case "snare":
                snare.GetChild(Random.Range(0, snare.childCount)).GetComponent<AudioSource>().Play();
                break;
            case "alerthealth":
                alert.GetComponent<AudioSource>().Play();
                break;
            case "orderaccepted":
                acc.GetComponent<AudioSource>().Play();
                break;
            case "orderdeclined":
                decl.GetComponent<AudioSource>().Play();
                break;
        }
    }
}
