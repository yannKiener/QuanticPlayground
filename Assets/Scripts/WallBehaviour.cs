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

    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    private List<Collider2D> colliders = new List<Collider2D>();
    private List<SpriteMask> spriteMasks = new List<SpriteMask>();
    private bool isQuantumKillerOnly = false;
    private bool isBasicKillerOnly = false;


    private void Awake()
    {
        if ((isQuantumBreakable || isBasicBreakable) && !isQuantumKiller && !isBasicKiller)
        {
            tag = "breakable";
        }
    }

    private void Start()
    {
        SetSpriteRenderers();
        SetColliders();
        SetBreakableMasks();
        isQuantumKillerOnly = isQuantumKiller && (!isBasicKiller && !isBasicBreakable);
        isBasicKillerOnly = isBasicKiller && (!isQuantumKiller && !isQuantumBreakable);

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

        if (!isBasicBreakable && !isQuantumBreakable)
        {
            foreach(SpriteMask spriteMask in spriteMasks)
            {
                DestroyImmediate(spriteMask);
            }
        }

       
    }

    private void Update()
    {
        if (GameUtils.IsPlayerinQuantumMode())
        {
            if (isQuantumKillerOnly)
            {
                ShowGameObject();
            }
            if (isBasicKillerOnly)
            {
                HideGameObject();
            }
           
        } else
        {
            if (isQuantumKillerOnly)
            {
                HideGameObject();
            }
            if (isBasicKillerOnly)
            {
                ShowGameObject();
            }
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        handleCollision(col, false);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        handleCollision(col, true);
    }

    private void handleCollision(Collision2D col, bool enterCollision)
    {
        if (col.gameObject.name == "Player")
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
                    SoundManager.PlayBreakSound();
                }
            }
            else
            {
                if (isBasicKiller)
                {
                    Destroy(col.gameObject, destroyDelay);
                    GameUtils.GameOver();
                }

                if (isBasicBreakable)
                {
                    Destroy(gameObject, destroyDelay);
                    SoundManager.PlayBreakSound();
                }
            }
            if (enterCollision)
            {
                if (!isQuantumKiller && !isBasicKiller)
                {
                    SoundManager.PlayBounceSound();
                }
            }
        }
    }

    private void SetBreakableMasks()
    {
        AddSpriteMask(this.gameObject);
        foreach (Transform child in transform)
        {
            AddSpriteMask(child.gameObject);
        }
    }

    private void AddSpriteMask(GameObject go)
    {
        SpriteMask selfSpriteMask = go.GetComponent<SpriteMask>();
        if (selfSpriteMask != null)
        {
            spriteMasks.Add(selfSpriteMask);
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

    private void SetColliders()
    {
        AddCollider(this.gameObject);
        foreach (Transform child in transform)
        {
            AddCollider(child.gameObject);
        }
    }

    private void AddCollider(GameObject go)
    {
        Collider2D selfCollider = go.GetComponent<Collider2D>();
        if (selfCollider != null)
        {
            colliders.Add(selfCollider);
        }
    }

    private void SetColors(Color col)
    {
        foreach (SpriteRenderer sp in spriteRenderers)
        {
            sp.color = col;
        }
    }

    private void HideGameObject()
    {
        foreach(SpriteRenderer sp in spriteRenderers)
        {
            Color tmpCol = sp.color;
            tmpCol.a = GameController.getInstance().hideWallAlpha;
            sp.color = tmpCol;
        }

        foreach (Collider2D col in colliders)
        {
            col.isTrigger = true;
        }
    }

    private void ShowGameObject()
    {
        foreach (SpriteRenderer sp in spriteRenderers)
        {
            Color tmpCol = sp.color;
            tmpCol.a = 1f;
            sp.color = tmpCol;
        }

        foreach (Collider2D col in colliders)
        {
            col.isTrigger = false;
        }
    }
}
