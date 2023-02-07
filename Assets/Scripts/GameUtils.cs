using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtils 
{
    private static bool isPlayerQuantum = false;
    private static float score = 0;
    private static bool isGameOver = false;

    public static bool IsPlayerinQuantumMode()
    {
        return isPlayerQuantum;
    }

    public static void SetPlayerIsQuantum(bool isQuantum)
    {
        isPlayerQuantum = isQuantum;
    }

    public static bool IsGameOver()
    {
        return isGameOver;
    }

    public static void StartGame()
    {
        isGameOver = false;
    }

    public static void GameOver()
    {
        isGameOver = true;
        SoundManager.PlayGameOverSound();
    }

    public static int GetScore()
    {
        return (int)(score);
    }

    public static void AddScore(float scoreToAdd)
    {
        score += scoreToAdd;
    }
}
