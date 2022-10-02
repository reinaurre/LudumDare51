using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ExplosionController : MonoBehaviour
{
    public float damage;
    public int scale;
    public AudioClip explosionClip;
    public float explosionLifetime = 5;

    private BackgroundScroller scroller;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(scale, scale, 1);
        this.scroller = GameObject.FindObjectOfType<BackgroundScroller>();
        this.audioSource = this.gameObject.GetComponent<AudioSource>();
        this.audioSource.clip = this.explosionClip;
        this.audioSource.Play();

        StartCoroutine(RemoveExplosion());
    }

    private void FixedUpdate()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.scroller.currentSpeed);
    }

    private IEnumerator RemoveExplosion()
    {
        yield return new WaitForSeconds(explosionLifetime);
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
