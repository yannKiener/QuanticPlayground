using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailingFade : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Color color;
    private float alpha = 1f;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= transform.localScale * Time.deltaTime;
        if (!GameUtils.IsPlayerinQuantumMode())
        {
            color = spriteRenderer.color;
            color.a = 0f;
            spriteRenderer.color = color;
        }
        else
        {
            color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
    }
}
