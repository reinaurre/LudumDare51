using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public int effectSeverity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
