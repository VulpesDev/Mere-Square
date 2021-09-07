using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Puzzle : MonoBehaviour
{
    /*
     * 1. get all the text
     * 2. choose one of the words
     * 3. get the first and the last letter
     * 4. make timer
     * 5. check answear
     */

    string text;
    string[] words;
    string chWord;
    char[] whWord;
    char f, l;
    float time;
    bool countTime = true;

    [SerializeField]Text timer;
    [SerializeField]Text questWord;
    [SerializeField]InputField inputField;
    string input;

    bool chosenWord = false;

    void Start()
    {
        text = Resources.Load<TextAsset>("words").text;
        words = text.Split('\n');
        chWord = words[UnityEngine.Random.Range(0, words.Length)];
        whWord = chWord.ToCharArray();
        f = chWord[0];
        l = chWord[chWord.Length - 2];
        time = 20.0f;
        chosenWord = false;
    }

    void Update()
    {
        //>=5
        if(countTime)
        {
            if (time > 0) time -= Time.deltaTime;
            else
            { time = 0; CheckIfCorrect(); }
        }
        else
        {
            //do nothing
        }
        timer.text = time.ToString("F2");


        if (!chosenWord) WriteWord();




    }
    void WriteWord()
    {
        int r = UnityEngine.Random.Range(0, chWord.Length - 3);
        int r2 = r;
        if (whWord.Length >= 6)
        {
            r2 = UnityEngine.Random.Range(0, chWord.Length - 3);
        }
        questWord.text = f.ToString();
        for (int i = 0; i < chWord.Length - 3; i++)
        {
            if (i==r||i==r2) questWord.text += whWord[i + 1].ToString();
            else
                questWord.text += " _ ";
        }
        questWord.text += l.ToString();
        chosenWord = true;
    }
    bool Correct()
    {
        if(input.ToLower().Trim() == chWord.ToLower().Trim())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void CheckIfCorrect()
    {
        input = inputField.text;
        if (Correct()) True();
        else False();
        StartCoroutine(ExitFromScene());
    }
    IEnumerator ExitFromScene()
    {
        countTime = false;
        questWord.text = chWord;
        yield return new WaitForSeconds(3f);
        LogicStarter.Activate();
        SceneManager.UnloadSceneAsync("Logic");
    }
    void True()
    {
        Score.time = time;
        SFX.Play("orderaccepted");
        questWord.color = Color.green;
        PlayerBehaviour.health += 20;
    }
    void False()
    {
        SFX.Play("orderdeclined");
        questWord.color = Color.red;
        PlayerBehaviour.health -= 20;
    }
}