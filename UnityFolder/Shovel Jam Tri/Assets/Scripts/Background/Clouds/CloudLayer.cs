using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLayer : MonoBehaviour {

	public static CloudLayer instance;

	// Set this high for close clouds, low for far away ones
	[SerializeField] public float _movementMult = 0.2f;

	[SerializeField] public Vector2 _cloudVel;
	private Vector2 _cloudTexOffset = Vector2.zero;

	private MeshRenderer _renderer;
	private Vector3 _lastPosition;
	private Transform _myTransform;

	private Color origCol1;
	private Color origCol2;
	private float origIntens;

	private void Awake () {
		// Singleton pattern
		if (instance != null) {
			Destroy(this);
		} else {
			instance = this;
		}

		_myTransform = GetComponent<Transform>();
		_renderer = GetComponent<MeshRenderer>();
		_lastPosition = _myTransform.position;
		ScaleToScreen();

		origCol1 = _renderer.material.GetColor("_TintColor");
		origCol2 = _renderer.material.GetColor("_SwirlTintColor");
		origIntens = _renderer.material.GetFloat("_SwirlIntensity");

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

	public void SetSkyboxBlend(float blend) {
		// Change colors
		Color col1 = Color.Lerp(origCol1, new Color(0.75f, 0.1f, 1.0f, 0.7f), blend);
		Color col2 = Color.Lerp(origCol2, new Color(0.2f, 1.0f, 0.2f, 1.0f), blend);
		float intens = Mathf.Lerp(origIntens, 1.5f, blend);
		_renderer.material.SetColor("_TintColor", col1);
		_renderer.material.SetColor("_SwirlTintColor", col2);
		_renderer.material.SetFloat("_SwirlIntensity", intens);

	}

}
