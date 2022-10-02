using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float damage;
    public int scale;

    private BackgroundScroller scroller;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(scale, scale, 1);
        this.scroller = GameObject.FindObjectOfType<BackgroundScroller>();

        StartCoroutine(RemoveExplosion());
    }

    private void FixedUpdate()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.scroller.currentSpeed);
    }

    private IEnumerator RemoveExplosion()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Yeti"))
        {
            collision.gameObject.GetComponent<YetiController>().DealDamage(damage);
        }
    }
}
