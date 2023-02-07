using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject playerGameObject;
    public float scoreSpeedMultiplier;
    public string scorePreText;
    public GameObject scoreTextGameObject;
    public GameObject gameOverGameObject;
    public GameObject gameOverScoreTextGameObject;

    public Color wallColor;
    public Color basicBreakableWallColor;
    public Color quantumBreakableWallColor;
    public Color alwaysBreakableWallColor;
    public Color basicKillerWallColor;
    public Color quantumKillerWallColor;
    public Color alwaysKillerWallColor;

    private Rigidbody2D playerRigidBody;
    private float playerSpeed;
    private Text scoreText;
    private Text gameOverScoreText;

    private static GameController instance;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        playerRigidBody = playerGameObject.GetComponent<Rigidbody2D>();
        scoreText = scoreTextGameObject.GetComponent<Text>();
        gameOverScoreText = gameOverScoreTextGameObject.GetComponent<Text>();
        gameOverGameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameUtils.IsGameOver())
        {
            playerSpeed = playerRigidBody.velocity.magnitude;
            GameUtils.AddScore(Time.deltaTime * playerSpeed * playerSpeed * playerSpeed * scoreSpeedMultiplier);
            scoreText.text = scorePreText + GameUtils.GetScore();
            gameOverScoreText.text = GameUtils.GetScore().ToString(); 
        } else
        {
            gameOverGameObject.SetActive(true);
        }
    }

    public static GameController getInstance()
    {
        return instance;
    }
}
