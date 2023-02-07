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

    public bool isBasicCollider;
    public bool isQuantumCollider;

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

                if (isQuantumCollider)
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

                if (isBasicCollider)
                {
                    Debug.Log("You will go trough some other day.");
                }
            }

        }
    }
}
