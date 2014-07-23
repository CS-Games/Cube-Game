using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float moveSpeed;
	public GameObject deathParticles;
	public float jumpHeight = 2;
	private bool isFalling = false; //need to figure out how to implement so you can't just keep pressing the spacebar

	private float maxSpeed = 5f;
	private Vector3 input;

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
		if (Input.GetKeyDown(KeyCode.Space)) 
		{
			gameObject.rigidbody.velocity += new Vector3(0,jumpHeight,0);
		}
	
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.transform.tag == "Enemy")
		{
			Die();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Goal")
		{
			GameManager.CompleteLevel();
		}
	}

	void Die()
	{
		Instantiate(deathParticles, transform.position, Quaternion.identity);
		transform.position = spawn;
	}

}
