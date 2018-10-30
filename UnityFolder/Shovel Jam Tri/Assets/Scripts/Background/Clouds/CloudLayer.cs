using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLayer : MonoBehaviour {

	// Set this high for close clouds, low for far away ones
	[SerializeField] public float _movementMult = 0.2f;

	[SerializeField] public Vector2 _cloudVel;
	private Vector2 _cloudTexOffset = Vector2.zero;

	private MeshRenderer _renderer;
	private Vector3 _lastPosition;
	private Transform _myTransform;

	private void Awake () {
		_myTransform = GetComponent<Transform>();
		_renderer = GetComponent<MeshRenderer>();
		_lastPosition = _myTransform.position;
		ScaleToScreen();

	}

	private void ScaleToScreen() {
		// Orig scale: 1920x1080
		Vector2 ratio = new Vector2(Screen.width / 1080.0f, Screen.height / 1920.0f);

	}
	
	private void LateUpdate () {
		Vector2 frameMovement = GetMovementThisFrame();

		// Apply camera movement
		_cloudTexOffset += frameMovement * _movementMult;

		// Apply velocity
		_cloudTexOffset += _cloudVel * Time.deltaTime;

		_renderer.material.SetVector("_CameraOffset", _cloudTexOffset);

	}

	private Vector2 GetMovementThisFrame() {
		Vector2 movement = new Vector2(
				_myTransform.position.x - _lastPosition.x,
				_myTransform.position.y - _lastPosition.y
			);
		_lastPosition = _myTransform.position;

		return movement;
	}

}
