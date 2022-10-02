using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ObstacleController : MonoBehaviour
{
    public int effectSeverity;
    public AudioClip impactClip;

    private AudioSource audioSource;

    private void Start()
    {
        this.audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!collision.gameObject.GetComponent<PlayerController>().isJumping)
            {
                this.audioSource.clip = impactClip;
                this.audioSource.Play();
            }

            if (this.gameObject.CompareTag("SlowingObstacle") && !collision.GetComponent<PlayerController>().isJumping)
            {
                GameManager.instance.SlowPlayer(effectSeverity);
            }
            else if (this.gameObject.CompareTag("JumpingObstacle"))
            {
                GameManager.instance.BoostSpeed();
                collision.GetComponent<PlayerController>().StartJumping();
            }
        }
    }
}
