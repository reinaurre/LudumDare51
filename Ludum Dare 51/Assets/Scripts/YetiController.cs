using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiController : MonoBehaviour
{
    public float speed;
    public float baseHealth;
    public float currentHealth;

    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        currentHealth = baseHealth * GameManager.instance.yetiLevel;
        speed = speed * ((GameManager.instance.yetiLevel + 1) / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlayerActive)
        {
            float step = speed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
            transform.position = new Vector3(transform.position.x, transform.position.y, -.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.KillPlayer();
        }
    }

    public void DealDamage(float damage)
    {
        this.currentHealth -= damage;

        if (this.currentHealth <= 0)
        {
            this.Die();
        }
    }

    private void Die()
    {
        GameManager.instance.SpawnNewYeti();
        Destroy(this.gameObject);
    }
}
