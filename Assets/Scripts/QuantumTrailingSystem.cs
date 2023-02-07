using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumTrailingSystem : MonoBehaviour
{
    private float trailTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        trailTimer += Time.deltaTime;
        if(trailTimer >= GameController.getInstance().trailTimer)
        {
            spawnCopy();
            trailTimer = 0;
        }

    }

    private void spawnCopy()
    {
        GameObject tempGameObject = Instantiate(gameObject);
        //Remove unused components
        QuantumTrailingSystem trailingSystem = tempGameObject.GetComponent<QuantumTrailingSystem>();
        if (trailingSystem != null)
        {
            DestroyImmediate(trailingSystem);
        }
        Player playerScript = tempGameObject.GetComponent<Player>();
        if(playerScript != null)
        {
            DestroyImmediate(playerScript);
        }

        Rigidbody2D rigidbody = tempGameObject.GetComponent<Rigidbody2D>();
        if (rigidbody != null)
        {
            DestroyImmediate(rigidbody);
        }

        Collider2D collider = tempGameObject.GetComponent<Collider2D>();
        if (collider != null)
        {
            DestroyImmediate(collider);
        }

        tempGameObject.transform.position = gameObject.transform.position;

        tempGameObject.AddComponent<TrailingFade>();
        Destroy(tempGameObject, GameController.getInstance().trailDuration);
    }

}
