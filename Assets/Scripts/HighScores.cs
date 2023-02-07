using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScores
{
    Dictionary<int, int> highScores;

    public HighScores(Dictionary<int, int> highScores)
    {
        this.highScores = highScores;
    }

    public void AddScore(int score)
    {
        int i = 0;
        foreach (KeyValuePair<int, int> kv in highScores)
        {
            i++;
            if (kv.Value < score)
            {
                //Debug.Log("Pushing score at position : " + kv.Key);
                pushScore(kv.Key, score);
                return;
            }
        }
        //If no HighScore is beaten and map isn't full (10), we add the score at the end.
        if (i < 10)
        {
            Debug.Log("New score at bottom.");
            highScores.Add(i + 1, score);
        }
    }

    public Dictionary<int,int> GetDictionary()
    {
        return highScores;
    }

    //Used to "push down" lower existing scores
    private void pushScore(int position, int score)
    {
        if (position < 10)
        {
            if (highScores.ContainsKey(position))
            {
                int tempScore = highScores[position];
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
