using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public GameObject playerToFire;
    public Vector2 fireForce;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D playerRBody = playerToFire.GetComponent<Rigidbody2D>();
        playerRBody.AddForce(fireForce, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
