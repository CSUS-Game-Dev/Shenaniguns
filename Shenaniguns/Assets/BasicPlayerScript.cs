using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerScript : MonoBehaviour {

	[SerializeField] public float moveSpeed;
	private CharacterController controller;

	private Vector3 forwardDirection;
	public Quaternion currentRotation;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		currentRotation = Quaternion.identity;
		
	}
	
	// Update is called once per frame
	void Update () {
		move();
	}

	public void move(){
		controller.Move(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")));
	}

}
