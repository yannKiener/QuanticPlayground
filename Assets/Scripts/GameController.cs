using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("General Balance")]
    public float scoreSpeedMultiplier;

    [Header("GameObjects instances")]
    public GameObject background;
    public GameObject playerGameObject;
    public GameObject scoreTextGameObject;
    public GameObject gameOverGameObject;
    public GameObject gameOverScoreTextGameObject;
    public string scorePreText;

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

    private Rigidbody2D playerRigidBody;
    private float playerSpeed;
    private Text scoreText;
    private Text gameOverScoreText;
    private SpriteRenderer backgroundSpRenderer;
    private SpriteRenderer playerSpRenderer;

    private static GameController instance;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        playerRigidBody = playerGameObject.GetComponent<Rigidbody2D>();
        scoreText = scoreTextGameObject.GetComponent<Text>();
        gameOverScoreText = gameOverScoreTextGameObject.GetComponent<Text>();
        gameOverGameObject.SetActive(false);
        backgroundSpRenderer = background.GetComponent<SpriteRenderer>();
        playerSpRenderer = playerGameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameUtils.IsGameOver())
        {
            if(playerRigidBody != null)
            {
                playerSpeed = playerRigidBody.velocity.magnitude;
                GameUtils.AddScore(Time.deltaTime * playerSpeed * playerSpeed * playerSpeed * scoreSpeedMultiplier);
                scoreText.text = scorePreText + GameUtils.GetScore();
                gameOverScoreText.text = GameUtils.GetScore().ToString();
            }
            else
            {
                Debug.Log("Player rigidBody is null ! Let's hope it's only this frame..?");
            }
        } else
        {
            gameOverGameObject.SetActive(true);
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
}
