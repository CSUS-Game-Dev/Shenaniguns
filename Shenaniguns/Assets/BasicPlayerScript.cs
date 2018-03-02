using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerScript : MonoBehaviour {

	[SerializeField] public float moveSpeed;
	private CharacterController controller;


	//The maximum and minimum angles the player can look vertically
	public static float MAX_PITCH = 80f;
	public static float MIN_PITCH = -80f;

	//Keeps track of the pitch the character is looking
	private float currentPitch = 0;

	//Directions the player is looking
	private Vector3 lookDirection = new Vector3(0f, 0f, 0f);
	private Vector3 directionXZ = new Vector3(0f, 0f, 0f);

	private Vector3 cameraTarget = new Vector3(0f, 0f, 0f);

	//Height above the player that the camera sits
	private float cameraHeight = 10f;

	public float targetDistance = 5f;
	private float rotationSpeed = 10f;

	public Camera playerCamera;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		lookDirection = new Vector3(0f, 0f, 0f);
		playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	// It is important that the rotation happens before the movement - it will feel more responsive that way
	void Update () {
		rotateWithMouse();
		move();
		updateCamera();
	}

	public void move(){
		//Vector3 moveDirection = Vector3.Scale(transform.rotation * new Vector3(0f, 0f, 1f), new Vector3(1f, 0f, 1f)).normalized;

		Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

		inputDirection = Vector3.Scale(transform.rotation * inputDirection, new Vector3(1f, 0f, 1f)).normalized;
		controller.Move(inputDirection* moveSpeed);
	}

	public void rotateWithMouse(){
		 yaw(Input.GetAxis("Mouse X") * rotationSpeed);
		 pitch(Input.GetAxis("Mouse Y") * -rotationSpeed);
	}

	//Rotates the character around the absolute Y axis
	public void yaw(float eulerAngle){
		
		Quaternion rotU = Quaternion.AngleAxis(eulerAngle, Vector3.up);
		transform.rotation = rotU * transform.rotation;

		directionXZ = Vector3.Scale(transform.rotation * new Vector3(0f, 0f, 1f), new Vector3(1f, 0f, 1f)).normalized;
	}

	//Rotates the character up and down locally
	public void pitch(float eulerAngle){
		float newFloat = eulerAngle;

		if(currentPitch + eulerAngle > MAX_PITCH){
			newFloat = MAX_PITCH - currentPitch;
		}
		else if(currentPitch + eulerAngle < MIN_PITCH){
			newFloat = MIN_PITCH - currentPitch;
		}

		currentPitch += newFloat;

		Quaternion rotR = Quaternion.AngleAxis(newFloat, Vector3.right);
		transform.rotation = transform.rotation * rotR;
		lookDirection = transform.rotation.eulerAngles.normalized;
	}

	private void updateCamera(){
		updateCameraPosition();
		updateCameraTarget();
	}

	private void updateCameraPosition(){
		Vector3 playerCenter = transform.position + new Vector3(0f, cameraHeight, 0f) -(directionXZ * 20f);
		playerCamera.transform.position = playerCenter;
	}

	//Rotates the playerCamera to look at the correct location
	private void updateCameraTarget(){
		cameraTarget = transform.rotation * Vector3.forward * targetDistance;
		playerCamera.transform.LookAt(cameraTarget + transform.position);
	}

	public Vector3 getMoveDirection(){
		return directionXZ;
	}

	public Vector3 getLookDirection(){
		return lookDirection;
	}

}
