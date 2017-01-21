using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 2.0f;

    Vector3 currentDirection = Vector3.zero;

    private Rigidbody2D player;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
		player.freezeRotation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        player.velocity = move * speed * Time.deltaTime;

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        currentDirection = Vector3.zero;
    	player.velocity = Vector3.zero;
    }
}
