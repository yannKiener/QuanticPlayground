using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject background;
    public Color basicBackgroundColor;
    public Color quantumBackgroundColor;

    private SpriteRenderer backgroundSpRenderer;
    private bool isInQuantum = false;

    // Start is called before the first frame update
    void Start()
    {
        backgroundSpRenderer = background.GetComponent<SpriteRenderer>();
        //backgroundSpRenderer.color = basicBackgroundColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (isInQuantum)
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
        Debug.Log("Switched to Quantum world");
        isInQuantum = true;
        backgroundSpRenderer.color = quantumBackgroundColor;
    }

    private void switchToBasic()
    {
        Debug.Log("Switched to Basic world");
        isInQuantum = false;
        backgroundSpRenderer.color = basicBackgroundColor;
    }
}
