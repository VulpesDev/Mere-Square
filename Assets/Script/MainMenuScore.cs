using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Btime").GetComponent<TextMeshProUGUI>().text = Score.bestTime.ToString("F2");
        GameObject.Find("Bscore").GetComponent<TextMeshProUGUI>().text = Score.bestScore.ToString();
    }
}
