using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float moveSpeed;
	public GameObject deathParticles;
	public float jumpHeight = 2;
	private bool onGround;
	private bool touchWall;
	private Vector3 input;
	public float pickUpTimer = 10;

	private float maxSpeed = 5f;
	private Vector3 spawn;

	// Use this for initialization
	void Start () 
	{
		spawn = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

		if (rigidbody.velocity.magnitude < maxSpeed)
		{
			rigidbody.AddForce(input * moveSpeed);
		}

		if (transform.position.y < -2) 
		{
			Die ();
		}

		// Initiating the jump
		if (Input.GetKeyDown(KeyCode.Space) && onGround && !touchWall) 
		{
			gameObject.rigidbody.velocity += new Vector3(0,jumpHeight,0);
			onGround = false;
		}

	
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.transform.tag == "Enemy")
		{
			Die();
		}
		if(other.transform.tag == "Ground") 
		{
			onGround = true;
		}
		if(other.transform.tag == "Wall")
		{
			touchWall = true;
		}
	}

	void OnCollisionExit(Collision other)
	{
		if(other.transform.tag == "Wall")
		{
			touchWall = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Goal")
		{
			GameManager.CompleteLevel();
		}
		if (other.transform.tag == "SpeedPickUp")
		{
			moveSpeed = 100;
			Destroy (other.gameObject);
		}
		if (other.transform.tag == "BigPickUp")
		{
			transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
			Destroy (other.gameObject);
		}
	}

	void Die()
	{
		Instantiate(deathParticles, transform.position, Quaternion.identity);
		transform.position = spawn;
	}

}
