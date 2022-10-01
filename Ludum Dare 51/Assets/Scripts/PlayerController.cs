using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveXDirection = Input.GetAxisRaw("Horizontal");
        float moveYDirection = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(moveXDirection, moveYDirection).normalized * moveSpeed;
    }
}
