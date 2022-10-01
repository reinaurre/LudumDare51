using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public float projectileLifespan;
    public float damage;

    private Rigidbody2D rb;
    private float elapsedLifespan;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedLifespan += Time.deltaTime;

        if (elapsedLifespan >= projectileLifespan)
        {
            // do explosion or whatever
        }
        else if (this.transform.position.y > 20 || this.transform.position.x < -10 || this.transform.position.x > 10)
        {
            Destroy(this.gameObject);
        }

        rb.velocity = new Vector2(0, projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Yeti"))
        {
            collision.GetComponent<YetiController>().DealDamage(this.damage);
            Destroy(this.gameObject);
        }
    }
}
