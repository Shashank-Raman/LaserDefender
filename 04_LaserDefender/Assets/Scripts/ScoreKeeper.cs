using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

    public static int score = 0;
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
        Reset();
    }

    public void Score(int points)
    {
        score += points;
        text.text = score.ToString();
    }

    public static void Reset()
    {
        score = 0;
    }
}
