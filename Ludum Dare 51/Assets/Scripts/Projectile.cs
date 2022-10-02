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

    [Space]
    public GameObject optionalExplosionPrefab;
    public int optionalExplosionSize;

    private Rigidbody2D rb;
    private float elapsedLifespan;

    private BackgroundScroller scroller;

    // Start is called before the first frame update
    void Start()
    {
        scroller = GameObject.FindObjectOfType<BackgroundScroller>();
        rb = GetComponent<Rigidbody2D>();
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -0.9f);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedLifespan += Time.deltaTime;

        if (elapsedLifespan >= projectileLifespan && this.optionalExplosionPrefab)
        {
            this.CreateExplosion(damage);
        }
        else if (this.transform.position.y > 20 || this.transform.position.x < -10 || this.transform.position.x > 10)
        {
            Destroy(this.gameObject);
        }

        rb.velocity = (transform.up * projectileSpeed) * scroller.currentSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Yeti"))
        {
            if (this.optionalExplosionPrefab)
            {
                this.CreateExplosion();
            }
            collision.GetComponent<YetiController>().DealDamage(this.damage);
            Destroy(this.gameObject);
        }
    }

    private void CreateExplosion(float damage = 0)
    {
        GameObject explosion = Instantiate(this.optionalExplosionPrefab);
        explosion.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -1f);
        explosion.GetComponent<ExplosionController>().damage = damage;
        explosion.GetComponent<ExplosionController>().scale = optionalExplosionSize;
        Destroy(this.gameObject);
    }
}
