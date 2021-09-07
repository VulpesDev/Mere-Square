using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneManagerr : MonoBehaviour
{
    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Death")
        {
            GameObject.Find("Cscore").GetComponent<TextMeshProUGUI>().text = Score.score.ToString();
            GameObject.Find("Bscore").GetComponent<TextMeshProUGUI>().text = Score.bestScore.ToString();
            GameObject.Find("Ctime").GetComponent<TextMeshProUGUI>().text = Score.time.ToString("F2");
            GameObject.Find("Btime").GetComponent<TextMeshProUGUI>().text = Score.bestTime.ToString("F2");
        }
    }
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
