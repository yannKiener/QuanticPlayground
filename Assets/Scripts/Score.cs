using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Score
{
    string seed;
    int count;

    public Score(string name, int count)
    {
        this.seed = name;
        this.count = count;
    }

    public int GetCount()
    {
        return count;
    }

    public string GetSeedName()
    {
        return seed;
    }
}
