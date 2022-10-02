using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float baseMoveSpeed;
    public bool isJumping;

    public Sprite neutral;
    public Sprite left;
    public Sprite right;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        this.isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isJumping)
        {
            float moveXDirection = Input.GetAxisRaw("Horizontal");
            float moveYDirection = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(moveXDirection, moveYDirection).normalized * baseMoveSpeed;

            animator.SetInteger("SkiDirection", (int)moveXDirection);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y).normalized * baseMoveSpeed;
        }
    }

    public void StartJumping()
    {
        this.isJumping = true;
        animator.SetTrigger("Jump");
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -2f);
        StartCoroutine(Jumping());
    }

    IEnumerator Jumping()
    {
        yield return new WaitForSeconds(2);
        this.isJumping = false;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -0.1f);
    }
}
