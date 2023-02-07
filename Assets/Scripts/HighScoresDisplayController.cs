using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresDisplayController : MonoBehaviour
{
    public GameObject positions;
    public GameObject scores;
    public GameObject ggPanel;

    private Text positionText;
    private Text scoreText;

    // Awake is called before the first frame update and before OnEnable
    void Awake()
    {
        scoreText = scores.GetComponent<Text>();
        positionText = positions.GetComponent<Text>();
        ggPanel.SetActive(false);
    }

    private void OnEnable()
    {
        int currentScore = GameUtils.GetScore();
        GameUtils.SaveScore(currentScore);
        if (IsBestScore(currentScore)) {
            ggPanel.SetActive(true);
        }
        string positionString = "";
        string scoreString = "";
        foreach (KeyValuePair<int, int> kv in GameUtils.GetHighScores().GetDictionary()){
            positionString += kv.Key + "\r\n";
            scoreString += kv.Value +  "\r\n";
        }

        scoreText.text = scoreString;
        positionText.text = positionString;
    }

    private bool IsBestScore(int curScore)
    {
        return GameUtils.GetHighScores().GetDictionary()[1] == curScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
