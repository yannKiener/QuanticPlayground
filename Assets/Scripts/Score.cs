using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Score
{
    string seed;
    float time;

    public Score(string name, float elapsedTime)
    {
        this.seed = name;
        this.time = elapsedTime;
    }

    public float GetTime()
    {
        return time;
    }

    public string GetSeedName()
    {
        return seed;
    }
}
