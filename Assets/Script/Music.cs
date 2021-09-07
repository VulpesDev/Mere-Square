using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    AudioSource[] layers;

    void Start()
    {
        layers = GetComponentsInChildren<AudioSource>();
        StartCoroutine(StopGuitar());

        acceleration = baseAcceleration;
    }

    int a;
    int b;
    [SerializeField]float baseAcceleration;
    float acceleration;
    [SerializeField]float delay;
    IEnumerator StopGuitar()
    {
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < layers.Length; i++)
        {
            a = Random.Range(1, layers.Length);
            b = Random.Range(1, a);
            if (i >=b && i<=a)
            {
                if(i == 5 || i == 6)
                {
                    acceleration = baseAcceleration * 0.5f;
                }
                else
                {
                    acceleration = baseAcceleration;
                }
                if (layers[i].volume >= 0.9f)
                {
                    while (!doneDecreasing) yield return null;
                    StartCoroutine(DecreaseVolume(layers[i]));
                }
                else if (layers[i].volume <= 0.1f)
                {
                    while (!doneIncreasing) yield return null;
                    StartCoroutine(IncreaseVolume(layers[i]));
                }
            }
        }
        StartCoroutine(StopGuitar());
    }
    bool doneDecreasing = true;
    IEnumerator DecreaseVolume( AudioSource layersi)
    {
        if (layersi.volume > 0.01f)
        {
            layersi.volume -= Time.deltaTime * acceleration;
            yield return new WaitForSeconds(delay);
            doneDecreasing = false;
            StartCoroutine(DecreaseVolume(layersi));
        }
        else
        {
            doneDecreasing = true;
        }
    }
    bool doneIncreasing = true;
    IEnumerator IncreaseVolume(AudioSource layersi)
    {
        if (layersi.volume < 0.99f)
        {
            layersi.volume += Time.deltaTime * acceleration;
            yield return new WaitForSeconds(delay);
            doneIncreasing = false;
            StartCoroutine(IncreaseVolume(layersi));
        }else
        {
            doneIncreasing = true;
        }
    }
}
