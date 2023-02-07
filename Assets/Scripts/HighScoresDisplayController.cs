using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresDisplayController : MonoBehaviour
{
    public GameObject positions;
    public GameObject seeds;
    public GameObject scores;
    public GameObject ggPanel;

    private Text positionText;
    private Text scoreText;
    private Text seedText;

    // Awake is called before the first frame update and before OnEnable
    void Awake()
    {
        positionText = positions.GetComponent<Text>();
        seedText = seeds.GetComponent<Text>();
        scoreText = scores.GetComponent<Text>();

        ggPanel.SetActive(false);
    }

    private void OnEnable()
    {
        Score currentScore = new Score(GameUtils.GetCurrentSeed(), GameUtils.GetScore());
        if (!GameController.getInstance().isTutorial)
        {
            GameUtils.SaveScore(currentScore);
        }
        if (IsBestScore(GameUtils.GetScore())) {
            ggPanel.SetActive(true);
        }
        string positionString = "";
        string seedString = "";
        string scoreString = "";
        foreach (KeyValuePair<int, Score> kv in GameUtils.GetHighScores().GetDictionary())
        {
            positionString += kv.Key + "\r\n";
            seedString += kv.Value.GetSeedName() + "\r\n";
            scoreString += kv.Value.GetCount() + "\r\n";
        }

        scoreText.text = scoreString;
        seedText.text = seedString;
        positionText.text = positionString;
    }

    private bool IsBestScore(int curScore)
    {
        return GameUtils.GetHighScores().GetDictionary()[1].GetCount() == curScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
