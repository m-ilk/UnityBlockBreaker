using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    bool hasStart = false;
    // Start is called before the first frame update
    AudioSource audioSource;
    Rigidbody2D rigidbody2D;
    //cached component references

    Vector2 paddleToBallVector;
    void Start()
    {
        paddleToBallVector = transform.position - paddle.transform.position;
        audioSource = GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStart) {
            LockBallToPaddle();
            LanchOnMouseClick();
        }
    }

    private void LanchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0)) {
            rigidbody2D.velocity = new Vector2(xPush, yPush);
            hasStart = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 tweak = new Vector2(Random.Range(0, randomFactor), Random.Range(0, randomFactor));
        if (hasStart) {
       
            AudioClip clip = ballSounds[Random.Range(0,ballSounds.Length)];
            audioSource.PlayOneShot(clip);
            rigidbody2D.velocity += tweak;
        }
    }
}
