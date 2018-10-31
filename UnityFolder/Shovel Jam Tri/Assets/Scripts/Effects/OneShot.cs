using UnityEngine;

/*
	GameObject that destroys itself after set time
*/
public class OneShot : MonoBehaviour {

	[SerializeField] public float lifetime;

	private float timeLeft = 10.0f;

    //if you are a bullet, expload when you die
    public bool bullet = false;

    public GameObject explosion;

	private void Awake() {
		timeLeft = lifetime;
	}
	
	private void FixedUpdate() {
		timeLeft -= Time.fixedDeltaTime;
		if (timeLeft <= 0.0f) {
            if (bullet)
                Instantiate(explosion, transform.position, transform.rotation);
			Destroy(gameObject);
		}

	}

}
