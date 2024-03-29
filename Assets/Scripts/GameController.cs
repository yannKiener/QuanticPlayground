﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public bool isTutorial;
    [Header("General Balance")]
    public float scoreSpeedMultiplier;
    [Range(0.0f, 1f)]
    public float hideWallAlpha;
    public float startGameDelay;
    public float startGameDelayAfterFadeOut;
    public GameObject transitionScreen;
    public string nextSceneName;

    [Header("BackGround and player colors")]
    public Color basicBackgroundColor;
    public Color quantumBackgroundColor;
    public Color playerBasicColor;
    public Color playerQuantumColor;

    [Header("Wall colors")]
    public Color wallColor;
    public Color basicBreakableWallColor;
    public Color quantumBreakableWallColor;
    public Color alwaysBreakableWallColor;
    public Color basicKillerWallColor;
    public Color quantumKillerWallColor;
    public Color alwaysKillerWallColor;
    public Color basicGhostWallColor;
    public Color quantumGhostWallColor;

    [Header("Quantum Trailing")]
    public float trailTimer;
    public float trailDuration;

    [Header("Seeding settings")]
    public string seedOrRandomIfEmpty;
    public List<GameObject> prefabsList;
    [Range(0, 100)]
    public int density;
    public float maxPositionX;
    public float maxPositionY;
    public float offsetX;
    public float offsetY;
    public float minScale;
    public float maxScale;

    [Header("GameObjects instances")]
    public GameObject background;
    public GameObject playerGameObject;
    public GameObject gameOverScreen;
    public GameObject gameOverTimeNumberGameObject;
    public GameObject tutorialWonScreen;
    public GameObject tutorialTimeNumberGameObject;
    public string scorePreText;
    public GameObject scoreTextGameObject;
    public string timerPreText;
    public GameObject timeTextGameObject;
    public GameObject RandomLevelGameOverButton;
    public GameObject RandomLevelMenuButton;


    private Rigidbody2D playerRigidBody;
    private float playerSpeed;
    private Text timeText;
    private Text gameOverTimeText;
    private Text tutorialTimeText;
    private SpriteRenderer backgroundSpRenderer;
    private SpriteRenderer playerSpRenderer;
    private SpriteRenderer transitionScreenSpRenderer;
    private SpriteRenderer transitionScreenTitle;
    private SpriteRenderer transitionScreenLogo;
    private SpriteRenderer credits;
    public GameObject[] objectivesToBreak;

    private static GameController instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = playerGameObject.GetComponent<Rigidbody2D>();
        if (isTutorial)
        {
            RandomLevelGameOverButton.SetActive(false);
            scoreTextGameObject.SetActive(false);
        }
        timeText = timeTextGameObject.GetComponent<Text>();
        gameOverTimeText = gameOverTimeNumberGameObject.GetComponent<Text>();
        tutorialTimeText = tutorialTimeNumberGameObject.GetComponent<Text>();
        gameOverScreen.SetActive(false);
        tutorialWonScreen.SetActive(false);
        backgroundSpRenderer = background.GetComponent<SpriteRenderer>();
        playerSpRenderer = playerGameObject.GetComponent<SpriteRenderer>();

        if (GameUtils.IsRandomPlayground())
        {
            GenerateMap();
        }

        objectivesToBreak = GameObject.FindGameObjectsWithTag("breakable");

        HandleTransitionEffect();
    }

    private void HandleTransitionEffect()
    {
        //Transition Screen effects 
        GameObject transitionGO = Instantiate(transitionScreen);
        transitionScreenSpRenderer = transitionGO.GetComponent<SpriteRenderer>();
        transitionScreenTitle = transitionGO.transform.GetChild(0).GetComponent<SpriteRenderer>();
        transitionScreenLogo = transitionGO.transform.GetChild(1).GetComponent<SpriteRenderer>();
        credits = transitionGO.transform.GetChild(2).GetComponent<SpriteRenderer>();

        StartCoroutine(UnlockTimeAfterDelay(startGameDelay + startGameDelayAfterFadeOut));
        StartCoroutine(FadeOutAlphaOverTime(transitionScreenSpRenderer, startGameDelay));
        StartCoroutine(FadeOutAlphaOverTime(transitionScreenTitle, startGameDelay));
        StartCoroutine(FadeOutAlphaOverTime(transitionScreenLogo, startGameDelay));
        StartCoroutine(FadeOutAlphaOverTime(credits, startGameDelay));
        GameUtils.PauseGame(true);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        HandleGameWinCondition();
        if (!GameUtils.IsGameOver() && !GameUtils.IsGameWon())
        {
            if(playerRigidBody != null)
            {
                // Manages timer update
                GameUtils.AddTime(Time.deltaTime);
                timeText.text = timerPreText + GameUtils.GetElapsedTime().ToString("F2");
                gameOverTimeText.text = GameUtils.GetElapsedTime().ToString("F2") + " s";
                tutorialTimeText.text = GameUtils.GetElapsedTime().ToString("F2") + " s";

            }
            else
            {
                Debug.Log("Player rigidBody is null ! Let's hope it's only this frame..?");
            }
        } else
        {
            MusicManager.PlayEndMusic();
            if (GameUtils.IsGameWon())
            {
                tutorialWonScreen.SetActive(true);
            } else
            {
                gameOverScreen.SetActive(true);
            }
        }
    }

    // Usable for next level button
    public void StartScene()
    {
        GameUtils.StartScene(nextSceneName);
    }

    private void HandleGameWinCondition()
    {
        if (Array.FindAll(objectivesToBreak, (GameObject go) => go != null).Length == 0 && !GameUtils.IsGamePaused() && !GameUtils.IsGameWon() && GameUtils.GetElapsedTime() > 1f)
        {
            DestroyImmediate(playerGameObject);
            GameUtils.GameWon();
        }
    }

    public void SwitchColors(bool isQuantum)
    {
        if (isQuantum)
        {
            backgroundSpRenderer.color = GameController.getInstance().quantumBackgroundColor;
            playerSpRenderer.color = GameController.getInstance().playerQuantumColor;
        } else
        {
            backgroundSpRenderer.color = GameController.getInstance().basicBackgroundColor;
            playerSpRenderer.color = GameController.getInstance().playerBasicColor;
        }
    }

    public static GameController getInstance()
    {
        return instance;
    }

    public static void GenerateMap()
    {
        string generationSeed = instance.seedOrRandomIfEmpty;
        if(!IsEmptyString(GameUtils.GetCurrentSeed()))
        {
            generationSeed = GameUtils.GetCurrentSeed();
        }
        if (IsEmptyString(generationSeed))
        {
            generationSeed = Time.realtimeSinceStartup.ToString();
        }
        
        GameUtils.SetCurrentSeed(generationSeed);
        System.Random pseudoRandom = new System.Random(generationSeed.GetHashCode());
        
        for(float x = -instance.maxPositionX; x <= instance.maxPositionX; x++)
        {
            for (float y = -instance.maxPositionY; y <= instance.maxPositionY; y++)
            {
                if (!isInOffsetZone(x,y) && pseudoRandom.Next(0, 100) <= instance.density)
                {
                    GameObject randomPrefab = instance.prefabsList[pseudoRandom.Next(0, instance.prefabsList.Count)];
                    randomPrefab = Instantiate(randomPrefab);
                    randomPrefab.transform.position = new Vector3(x, y, 0);
                    float rotationZ = getNextRandomBetween(pseudoRandom, -1f, 1f) * 360;
                    randomPrefab.transform.Rotate(new Vector3(0, 0, rotationZ));

                    float scaleX = getNextRandomBetween(pseudoRandom, instance.minScale, instance.maxScale);
                    float scaleY = getNextRandomBetween(pseudoRandom, instance.minScale, instance.maxScale);
                    randomPrefab.transform.localScale = new Vector3(scaleX, scaleY, 1);
                }
            }
        }
    }
    
    private static bool IsEmptyString(string str)
    {
        return (str == null || str.Trim().Equals(""));
    }

    private static float getNextRandomBetween(System.Random pseudoRand, float minusValue, float maxValue) {
        return Mathf.Lerp(minusValue, maxValue, (float)pseudoRand.Next(0, 100) / 100); ;
    }

    private static bool isInOffsetZone(float x, float y)
    {
        return x < -instance.maxPositionX + instance.offsetX &&
            y > instance.maxPositionY - instance.offsetY;
    }

    IEnumerator UnlockTimeAfterDelay(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1f;
        GameUtils.PauseGame(false);
    }

    IEnumerator FadeOutAlphaOverTime(SpriteRenderer sprite, float duration)
    {
        Color startColor = sprite.color;
        Color endColor = startColor;
        endColor.a = 0f;
        Color currentColor;
        for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
        {
            float normalizedTime = t / duration;
            currentColor = Color.Lerp(startColor, endColor, normalizedTime);
            sprite.color = currentColor;
            yield return null;
        }
        sprite.color = endColor;
    }
}
