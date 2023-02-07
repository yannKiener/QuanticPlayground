using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public GameObject playerToFire;
    public Vector2 fireForce;
    public float moveSpeed;
    public float moveAfter;
    public float deleteAfterMove;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D playerRBody = playerToFire.GetComponent<Rigidbody2D>();
        playerRBody.AddForce(fireForce, ForceMode2D.Impulse);
        Destroy(gameObject, moveAfter + deleteAfterMove);

    }

    // Update is called once per frame
    void Update()
    {
        moveAfter -= Time.deltaTime;
        if (moveAfter < 0)
        {
            transform.position = transform.position - new Vector3(Time.deltaTime * moveSpeed, 0, 0);
        }
    }
}
