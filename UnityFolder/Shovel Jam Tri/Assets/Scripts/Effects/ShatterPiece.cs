using UnityEngine;

public class ShatterPiece : GetMyTransform {

	[SerializeField] public Vector3 defaultDirection = Vector3.forward;
	[SerializeField] public float directionRandomness = 0.5f;
	[SerializeField] public float minSpeed = 10.0f;
	[SerializeField] public float maxSpeed = 10.0f;
	[SerializeField] public float minRotationSpeed = 100.0f;
	[SerializeField] public float maxRotationSpeed = 100.0f;
	[SerializeField] public float lifetime = 10.0f;

	// Active vars
	private Vector3 vel;
	private Vector3 angVel;
	private float lifeLeft;

	protected override void Awake() {
		base.Awake();
		SetShatterMotion();
		lifeLeft = lifetime;
	}

	private void SetShatterMotion() {
		vel = Vector3.Lerp(defaultDirection, Random.insideUnitSphere, directionRandomness);
		vel = vel.normalized * Random.Range(minSpeed, maxSpeed);
		angVel = Random.insideUnitSphere * Random.Range(minRotationSpeed, maxRotationSpeed);
	}
	
	private void FixedUpdate () {
		myTransform.position += vel * Time.fixedDeltaTime;
		myTransform.rotation *= Quaternion.Euler(angVel * Time.fixedDeltaTime);
		lifeLeft -= Time.fixedDeltaTime;
		if (lifeLeft <= 0.0f) {
			// It's a good time to die
			Destroy(gameObject);
		}

	}

}
