using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class GameUtils
{
    private static string highScoreFilePath = Application.persistentDataPath + "/highscores.save";
    private static HighScores highScores;

    private static bool isPlayerQuantum = false;
    private static float score = 0;
    private static float elapsedTime = 0;
    private static bool isGameOver = false;
    private static bool isGameWon = false;
    private static bool isGamePaused = false;
    private static string currentSeed = null;

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

    public static bool IsGameWon()
    {
        return isGameWon;
    }

    public static void ReStartGame()
    {
        resetGameInfo();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void StartGameWithSeed(string seed)
    {
        resetGameInfo();
        currentSeed = seed;
        SceneManager.LoadScene("RandomPlayground");
    }

    public static void StartRandomGame()
    {
        resetGameInfo();
        currentSeed = null;
        SceneManager.LoadScene("RandomPlayground");
    }

    public static void StartScene(string levelName)
    {
        resetGameInfo();
        currentSeed = null;
        SceneManager.LoadScene(levelName);
    }
    
    private static void resetGameInfo()
    {
        isGameOver = false;
        isGameWon = false;
        isGamePaused = false;
        Time.timeScale = 1f;
        score = 0;
        elapsedTime = 0;
    }

    public static void GameOver()
    {
        isGameOver = true;
        SoundManager.PlayGameOverSound();
    }

    public static void GameWon()
    {
        isGameWon = true;
        SoundManager.PlayGameWonSound();
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
    
    public static bool IsRandomPlayground()
    {
        return "RandomPlayground".Equals(SceneManager.GetActiveScene().name);
    }
    
    public static void SetCurrentSeed(string seed)
    {
        Debug.Log("Seed for this level : " + seed);
        currentSeed = seed;
    }
    public static string GetCurrentSeed()
    {
        return currentSeed;
    }

    //Saves a new HighScore
    public static void SaveScore(Score score)
    {
        highScores = LoadHighScoresFromSave();
        highScores.AddScore(score);
        SaveHighScores(highScores);
    }

    public static HighScores GetHighScores()
    {
        highScores = LoadHighScoresFromSave();
        return highScores;
    }

    private static void SaveHighScores(HighScores highScores)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(highScoreFilePath);
        Debug.Log("Saving highscores at : " + highScoreFilePath);
        bf.Serialize(file, highScores);
        file.Close();
    }

    private static HighScores LoadHighScoresFromSave()
    {
        HighScores scoreList = null;
        if (File.Exists(highScoreFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(highScoreFilePath, FileMode.Open);
            scoreList = (HighScores)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.Log("Save file " + highScoreFilePath + " not found. Creating fake highscores.");
            Dictionary<int, Score> highScoreList = new Dictionary<int, Score>();
            highScoreList.Add(1, new Score("N1C3 533d", 1337));
            highScoreList.Add(2, new Score("Seed", 512));
            highScoreList.Add(3, new Score("Well.", 42));

            scoreList = new HighScores(highScoreList); 
        }

        return scoreList;
    }

}
