using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InGameInputSeed : MonoBehaviour
{
    public GameObject input;

    public void PlayInputSeed()
    {
        GameUtils.StartGameWithSeed(input.GetComponent<Text>().text);
    }
}
