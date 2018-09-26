using UnityEngine;

/*
	GameObject that destroys itself after set time
*/
public class OneShot : MonoBehaviour {

	[SerializeField] public float lifetime;

	private float timeLeft = 10.0f;

	private void Awake() {
		timeLeft = lifetime;
	}
	
	private void FixedUpdate() {
		timeLeft -= Time.fixedDeltaTime;
		if (timeLeft <= 0.0f) {
			Destroy(gameObject);
		}

	}

}
