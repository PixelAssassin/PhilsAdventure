﻿using UnityEngine;
using System.Collections;

public class GroundMovement : MonoBehaviour {
	
	public bool moveRight = true; //If enemy is moving right
	public float velocity = 1f; //Velocity of the enemy
	
	private bool atWall; //If the the enemy is hitting a wall
	private bool atEdge; //If the enemy is standing at an edge
	public float wallCheckRadius = 0.1f; //Radius in which a wall is searched
	public bool returnAtEdge; //Wheather or not the enemy returns if it is at an edge
	public float groundCheckRadius = 0.1f; //Radius in which a ground is searched
	public LayerMask whatIsWall; //Layer which defines the wall
	public LayerMask whatIsEnemy; //Layer which defines enemies
	public Transform wallCheck; //Point of the wall check
	public Transform groundCheck; //Point of the ground check
	
	private Rigidbody2D body; //Physical body of the enemy

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		//Check for a wall or an edge
		atWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, whatIsWall);
		if(!atWall)
			//atWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, whatIsEnemy);
		atEdge = !Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsWall);
		
		//If enemy is hitting a wall turn
		if (atWall) {
			moveRight = !moveRight;
		} else if (returnAtEdge && atEdge) //If enemy should return at an edge and he is at one return
			moveRight = !moveRight;
		
		
		
		if (moveRight) {
			transform.localScale = new Vector3 (-System.Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			body.velocity = new Vector2 (velocity, body.velocity.y );
			
		} else {
			transform.localScale = new Vector3 (System.Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			body.velocity = new Vector2 (-velocity, body.velocity.y);
			
		}
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemy")
			moveRight = !moveRight;
	}
}
