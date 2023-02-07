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

    private Rigidbody2D playerRigidBody;
    private float playerSpeed;
    private Text scoreText;



    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = playerGameObject.GetComponent<Rigidbody2D>();
        scoreText = scoreTextGameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameUtils.IsGameOver())
        {
            playerSpeed = playerRigidBody.velocity.magnitude;
            GameUtils.AddScore(Time.deltaTime * playerSpeed * playerSpeed * playerSpeed * scoreSpeedMultiplier);
            scoreText.text = scorePreText + GameUtils.GetScore();
        }

    }
}
