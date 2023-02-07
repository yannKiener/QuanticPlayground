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
    public GameObject gameOverScoreNumberGameObject;
    public GameObject gameOverTimeNumberGameObject;
    public GameObject tutorialWonScreen;
    public GameObject tutorialTimeNumberGameObject;
    public string scorePreText;
    public GameObject scoreTextGameObject;
    public string timerPreText;
    public GameObject timeTextGameObject;
    public GameObject RandomLevelGameOverButton;


    private Rigidbody2D playerRigidBody;
    private float playerSpeed;
    private Text scoreText;
    private Text gameOverScoreText;
    private Text timeText;
    private Text gameOverTimeText;
    private Text tutorialTimeText;
    private SpriteRenderer backgroundSpRenderer;
    private SpriteRenderer playerSpRenderer;

    private static GameController instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        playerRigidBody = playerGameObject.GetComponent<Rigidbody2D>();
        scoreText = scoreTextGameObject.GetComponent<Text>();
        gameOverScoreText = gameOverScoreNumberGameObject.GetComponent<Text>();
        if (isTutorial)
        {
            RandomLevelGameOverButton.SetActive(false);
            scoreTextGameObject.SetActive(false);
            gameOverScoreText.text = "-";
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
        StartCoroutine(UnlockTimeAfterDelay(startGameDelay));
        GameUtils.PauseGame(true);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameUtils.IsGameOver() && !GameUtils.IsGameWon())
        {
            if(playerRigidBody != null)
            {
                if (!isTutorial)
                {
                    // Manages score update
                    playerSpeed = playerRigidBody.velocity.magnitude;
                    GameUtils.AddScore(Time.deltaTime * playerSpeed * playerSpeed * playerSpeed * scoreSpeedMultiplier);
                    scoreText.text = scorePreText + GameUtils.GetScore();
                    gameOverScoreText.text = GameUtils.GetScore().ToString();
                }
               

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
            if (GameUtils.IsGameWon())
            {
                tutorialWonScreen.SetActive(true);
            } else
            {
                gameOverScreen.SetActive(true);
            }
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
        
        for(float x = -instance.maxPositionX; x < instance.maxPositionX; x++)
        {
            for (float y = -instance.maxPositionY; y < instance.maxPositionY; y++)
            {
                if (!isInOffsetZone(x,y) && pseudoRandom.Next(0, 100) <= instance.density)
                {
                    GameObject randomPrefab = instance.prefabsList[pseudoRandom.Next(0, instance.prefabsList.Count)];
                    randomPrefab = Instantiate(randomPrefab);
                    randomPrefab.transform.position = new Vector3(x, y, 0);
                    float rotationZ = getNextRandomBetween(pseudoRandom, -1f, 1f) * 360;
                    randomPrefab.transform.Rotate(new Vector3(0, 0, rotationZ));
                    // Updates local scales, but distors the sprite!
                    //float scaleX = getNextRandomBetween(pseudoRandom, instance.minScale, instance.maxScale);
                    //float scaleY = getNextRandomBetween(pseudoRandom, instance.minScale, instance.maxScale);
                    //randomPrefab.transform.localScale = new Vector3(scaleX, scaleY, 1);
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
}
