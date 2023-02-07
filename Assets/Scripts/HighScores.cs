using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScores
{
    private int scoreCountLimit = 10;
    Dictionary<int, Score> highScores;

    public HighScores(Dictionary<int, Score> highScores)
    {
        this.highScores = highScores;
    }

    public Dictionary<int, Score> GetDictionary()
    {
        return highScores;
    }

    public void AddScore(Score score)
    {
        int i = 0;
        foreach (KeyValuePair<int, Score> kv in highScores)
        {
            i++;
            if (kv.Value.GetCount() < score.GetCount())
            {
                //Debug.Log("Pushing score at position : " + kv.Key);
                pushScore(kv.Key, score);
                return;
            }
        }
        //If no HighScore is beaten and map isn't full, we add the score at the end.
        if (i < scoreCountLimit)
        {
            Debug.Log("New score at bottom.");
            highScores.Add(i + 1, score);
        }
    }

    //Used to "push down" lower existing scores
    private void pushScore(int position, Score score)
    {
        if (position < scoreCountLimit)
        {
            if (highScores.ContainsKey(position))
            {
                Score tempScore = highScores[position];
                highScores[position] = score;
                pushScore(position + 1, tempScore);
            }
            else
            {
                highScores[position] = score;
            }
        }
    }
}
