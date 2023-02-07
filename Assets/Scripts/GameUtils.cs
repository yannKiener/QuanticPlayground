using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameUtils 
{
    private static bool isPlayerQuantum = false;
    private static float score = 0;
    private static float elapsedTime = 0;
    private static bool isGameOver = false;
    private static bool isGamePaused = false;

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

    public static void ReStartGame()
    {
        Debug.Log("restarting Game");
        isGameOver = false;
        score = 0;
        elapsedTime = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void StartRandomGame()
    {
        isGameOver = false;
        score = 0;
        elapsedTime = 0;
        SceneManager.LoadScene("SeededArena");
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

    public static void PauseGame(bool isPaused)
    {
        isGamePaused = isPaused;
    }

    public static bool IsGamePaused()
    {
        return isGamePaused;
    }

    public static void AddTime(float timeToAdd)
    {
        elapsedTime += timeToAdd;
    }

    public static float GetElapsedTime()
    {
        return elapsedTime;
    }
    
    public static bool IsRandomArena()
    {
        return "SeededArena".Equals(SceneManager.GetActiveScene().name);
    }
}
