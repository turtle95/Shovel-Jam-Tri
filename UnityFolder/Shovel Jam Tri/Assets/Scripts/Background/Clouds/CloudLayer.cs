using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLayer : MonoBehaviour {

	public enum CloudShaderType { multiLayer, swirls }

	// Set this high for close clouds, low for far away ones
	[SerializeField] public CloudShaderType _cloudShader;
	[SerializeField] public float _movementMult = 0.2f;

	[SerializeField] public Vector2 _cloudVel;
	[SerializeField] public Vector2 _maskVel1;
	[SerializeField] public Vector2 _maskVel2;

	private Vector2 _cloudTexOffset = Vector2.zero;
	private Vector2 _maskTexOffset1 = Vector2.zero;
	private Vector2 _maskTexOffset2 = Vector2.zero;

	private MeshRenderer _renderer;
	private Vector3 _lastPosition;
	private Transform _myTransform;

	private void Awake () {
		_myTransform = GetComponent<Transform>();
		_renderer = GetComponent<MeshRenderer>();
		_lastPosition = _myTransform.position;

	}
	
	private void LateUpdate () {
		Vector2 frameMovement = GetMovementThisFrame();

		// Apply camera movement
		_cloudTexOffset += frameMovement * _movementMult;
		_maskTexOffset1 += frameMovement * _movementMult * 0.25f;
		_maskTexOffset2 += frameMovement * _movementMult * 0.33f;

		// Apply velocity
		_cloudTexOffset += _cloudVel * Time.deltaTime;
		_maskTexOffset1 += _maskVel1 * Time.deltaTime;
		_maskTexOffset2 += _maskVel2 * Time.deltaTime;

		_renderer.material.SetTextureOffset("_MainTex", _cloudTexOffset);
		if (_cloudShader == CloudShaderType.multiLayer) {
			_renderer.material.SetTextureOffset("_MaskTex1", _maskTexOffset1);
			_renderer.material.SetTextureOffset("_MaskTex2", _maskTexOffset2);
		} else {
			_renderer.material.SetTextureOffset("_MaskTex", _maskTexOffset1);
		}
		

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
