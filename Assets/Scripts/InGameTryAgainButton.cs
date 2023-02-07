using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameTryAgainButton : MonoBehaviour
{
    public void TryAgain()
    {
        GameUtils.StartGame();
    }
}
