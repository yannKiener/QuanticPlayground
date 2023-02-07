using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    private float gravityScale;
    public PhysicsMaterial2D basicBallBounciness;
    public PhysicsMaterial2D quantumBallBounciness;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        gravityScale = playerRigidbody.gravityScale;
        switchToBasic();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameUtils.IsGamePaused())
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

    }

    private void switchToQuantum()
    {
        //Debug.Log("Switched to Quantum world");
        GameUtils.SetPlayerIsQuantum(true);
        GameController.getInstance().SwitchColors(true);
        gameObject.AddComponent<QuantumTrailingSystem>();
        playerRigidbody.sharedMaterial = quantumBallBounciness;
        playerRigidbody.gravityScale = 0.0f;
        MusicManager.PlayQuantumMusic();
    }

    private void switchToBasic()
    {
        //Debug.Log("Switched to Basic world");
        GameUtils.SetPlayerIsQuantum(false);
        GameController.getInstance().SwitchColors(false);
        QuantumTrailingSystem trailingSystem = GetComponent<QuantumTrailingSystem>();
        if (trailingSystem != null)
        {
            Destroy(trailingSystem);
        }
        playerRigidbody.sharedMaterial = basicBallBounciness;
        playerRigidbody.gravityScale = gravityScale;
        MusicManager.PlayBasicMusic();
    }
}
