using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    // Start is called before the first frame update
    int score = 0;
    Text scoreText;
    
    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }

    public void ScoreHit(int score)
    {
        this.score += score;
        scoreText.text = score.ToString();
    }
}