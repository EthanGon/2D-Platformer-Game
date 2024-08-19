using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikedBallScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10.0f;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ProjectSpikedBall(Vector2 dir)
    {
        rb.AddForce(dir * this.speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

}
