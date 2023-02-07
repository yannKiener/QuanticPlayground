using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject background;
    public Color basicBackgroundColor;
    public Color quantumBackgroundColor;

    private SpriteRenderer backgroundSpRenderer;

    private SpriteRenderer playerSpRenderer;
    private Rigidbody2D playerRigidbody;
    private float gravityScale;
    public Color playerBasicColor;
    public Color playerQuantumColor;
    public PhysicsMaterial2D basicBallBounciness;
    public PhysicsMaterial2D quantumBallBounciness;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        gravityScale = playerRigidbody.gravityScale;
        playerSpRenderer = GetComponent<SpriteRenderer>();
        backgroundSpRenderer = background.GetComponent<SpriteRenderer>();
        switchToBasic();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
        {
            if (GameUtils.IsPlayerinQuantumMode())
            {
                switchToBasic();
            } else
            {
                switchToQuantum();
            }

        }
        
    }

    private void switchToQuantum()
    {
        //Debug.Log("Switched to Quantum world");
        GameUtils.SetPlayerIsQuantum(true);
        backgroundSpRenderer.color = quantumBackgroundColor;
        playerSpRenderer.color = playerQuantumColor;
        playerRigidbody.sharedMaterial = quantumBallBounciness;
        playerRigidbody.gravityScale = 0.0f;
    }

    private void switchToBasic()
    {
        //Debug.Log("Switched to Basic world");
        GameUtils.SetPlayerIsQuantum(false);
        backgroundSpRenderer.color = basicBackgroundColor;
        playerSpRenderer.color = playerBasicColor;
        playerRigidbody.sharedMaterial = basicBallBounciness;
        playerRigidbody.gravityScale = gravityScale;
    }
}
