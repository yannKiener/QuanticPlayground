using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameStartRandomLevel : MonoBehaviour
{
    public void StartRandomLevel()
    {
        GameUtils.StartRandomGame();
    }
}
