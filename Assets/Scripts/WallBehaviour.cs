using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{

    private static float destroyDelay = 0.01f;

    public bool isBasicKiller;
    public bool isBasicBreakable;

    public bool isQuantumKiller;
    public bool isQuantumBreakable;

    public bool isBasicGhostly;
    public bool isQuantumGhostly;

    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    private void Start()
    {
        SetSpriteRenderers();

        SetColors(GameController.getInstance().wallColor);

        if (isQuantumKiller)
        {
            SetColors(GameController.getInstance().quantumKillerWallColor);
        }

        if (isQuantumBreakable)
        {
            SetColors(GameController.getInstance().quantumBreakableWallColor);
        }

        if (isBasicKiller)
        {
            SetColors(GameController.getInstance().basicKillerWallColor);
        }

        if (isBasicBreakable)
        {
            SetColors(GameController.getInstance().basicBreakableWallColor);
        }

        if(isBasicBreakable && isQuantumBreakable)
        {
            SetColors(GameController.getInstance().alwaysBreakableWallColor);
        }

        if (isBasicKiller && isQuantumKiller)
        {
            SetColors(GameController.getInstance().alwaysKillerWallColor);
        }

        if (isQuantumGhostly)
        {
            SetColors(GameController.getInstance().quantumGhostWallColor);
        }

        if (isBasicGhostly)
        {
            SetColors(GameController.getInstance().basicGhostWallColor);
        }
    }

   private void SetSpriteRenderers()
    {
        AddSpriteRenderer(this.gameObject);
        foreach (Transform child in transform)
        {
            AddSpriteRenderer(child.gameObject);
        }
    }

    private void AddSpriteRenderer(GameObject go)
    {
        SpriteRenderer selfSpriteRenderer = go.GetComponent<SpriteRenderer>();
        if (selfSpriteRenderer != null)
        {
            spriteRenderers.Add(selfSpriteRenderer);
        }
    }

    private void SetColors(Color col)
    {
        foreach(SpriteRenderer sp in spriteRenderers)
        {
            sp.color = col;
        }
    }



    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Player")
        {
            if (GameUtils.IsPlayerinQuantumMode())
            {
                if (isQuantumKiller)
                {
                    Destroy(col.gameObject, destroyDelay);
                    GameUtils.GameOver();
                }

                if (isQuantumBreakable)
                {
                    Destroy(gameObject, destroyDelay);
                }

                if (isQuantumGhostly)
                {
                    Debug.Log("You will go trough some other day.");
                }

            } else
            {
                if (isBasicKiller)
                {
                    Destroy(col.gameObject, destroyDelay);
                    GameUtils.GameOver();
                }

                if (isBasicBreakable)
                {
                    Destroy(gameObject, destroyDelay);
                }

                if (isBasicGhostly)
                {
                    Debug.Log("You will go trough some other day.");
                }
            }

        }
    }
}
