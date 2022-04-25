using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPad : MonoBehaviour
{
    public float bounce = 10f;
    public AudioSource JumpPadSFX;
    [SerializeField] private string Direction;
    [SerializeField] private string PlayerDirection;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            JumpPadSFX.Play();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);

            if (PlayerDirection == "R")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * (bounce / 2), ForceMode2D.Impulse);
            }

            if (PlayerDirection == "L")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce((-Vector2.right) * (bounce / 2), ForceMode2D.Impulse);
            }
        }

        if (collision.gameObject.CompareTag("Box"))
        {
            JumpPadSFX.Play();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * (bounce * 0.7f), ForceMode2D.Impulse);

            if (Direction == "R")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * (bounce / 6), ForceMode2D.Impulse);
            }
            if (Direction == "L")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce((-Vector2.right) * (bounce / 6), ForceMode2D.Impulse);
            }
        }
    }
}
