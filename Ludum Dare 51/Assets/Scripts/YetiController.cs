using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class YetiController : MonoBehaviour
{
    public float baseSpeed;
    public float accelerationModifier;
    public float baseHealth;
    public float currentHealth;

    public GameObject bloodSplatterPrefab;
    public AudioClip yetiDeathClip;
    public AudioClip yetiEatingClip;

    private AudioSource audioSource;

    private GameObject target;
    private BackgroundScroller scroller;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        currentHealth = baseHealth * GameManager.instance.yetiLevel;
        baseSpeed = baseSpeed + (GameManager.instance.yetiLevel * 0.2f);
        scroller = GameObject.FindObjectOfType<BackgroundScroller>();
        this.audioSource = this.gameObject.GetComponent<AudioSource>();
        this.audioSource.clip = this.yetiEatingClip;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlayerActive)
        {
            float speedModifier = scroller.currentSpeed;
            speedModifier = speedModifier > baseSpeed + 0.01f ? baseSpeed + 0.01f : speedModifier;

            float step = (baseSpeed - speedModifier) * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);

            if (transform.position.y > 9)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 9), baseSpeed/4 * Time.deltaTime);
            }
            if (transform.position.x < -7)
            {
                transform.position = new Vector3(-7, transform.position.y, -.1f);
            }
            if (transform.position.x > 7)
            {
                transform.position = new Vector3(7, transform.position.y, -.1f);
            }

            transform.position = new Vector3(transform.position.x, transform.position.y, -.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !GameManager.instance.noDeathMode)
        {
            if (!collision.GetComponent<PlayerController>().isJumping)
            {
                this.audioSource.Play();
                GameManager.instance.KillPlayer();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !GameManager.instance.noDeathMode)
        {
            if (!collision.GetComponent<PlayerController>().isJumping)
            {
                this.audioSource.Play();
                GameManager.instance.KillPlayer();
            }
        }
    }

    public void DealDamage(float damage)
    {
        if (currentHealth <= 0) return;

        this.currentHealth -= damage;

        if (this.currentHealth <= 0)
        {
            this.Die();
        }
    }

    private void Die()
    {
        GameObject splatter = Instantiate(this.bloodSplatterPrefab);
        splatter.transform.position = this.transform.position;
        splatter.GetComponent<ExplosionController>().explosionClip = this.yetiDeathClip;

        GameManager.instance.SpawnNewYeti();
        Destroy(this.gameObject);
    }
}
