using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    // Singleton pattern
    public static CameraFollow instance;

    public float xOffset = -16;
    public int zOffset = 19;
    public Transform target;
    Vector3 targetPos, smoothedPos;
	public float smoothSpeed = 0.125f;
	Vector3 velocity = Vector3.one;

    private float screenshakeIntensity = 0.0f;
    private Vector3 screenshakeVector = Vector3.zero;
    private Vector3 screenshakeTargetVector = Vector3.zero;
    private float tilNewScreenshakeVector = 0.0f;

    private void Awake() {
        instance = this;
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update() {
        screenshakeIntensity = Mathf.Lerp(screenshakeIntensity, 0.0f, 2.5f * Time.deltaTime);
    }

    public void Screenshake(float intensity) {
        screenshakeIntensity = Mathf.Max(screenshakeIntensity, intensity);
    }

    // Update is called once per frame
    void FixedUpdate () {
        targetPos = new Vector3(target.position.x - xOffset, target.position.y, zOffset);
        
		smoothedPos = Vector3.SmoothDamp (transform.position, targetPos, ref velocity, smoothSpeed);
		smoothedPos = new Vector3 (targetPos.x, smoothedPos.y, targetPos.z);
		transform.position = smoothedPos + screenshakeVector * screenshakeIntensity;

        // Update randomness of screenshake
        tilNewScreenshakeVector -= Time.fixedDeltaTime;
        if (tilNewScreenshakeVector < 0.0f) {
            tilNewScreenshakeVector = Random.Range(0.015f, 0.0425f);

            // Make sure values aren't too close
            Vector3 prevVec = screenshakeTargetVector;
            while ((screenshakeTargetVector - prevVec).sqrMagnitude < 0.5f) {
                screenshakeTargetVector = Random.insideUnitSphere;
            }

        }
        // Slightly smooth motion out
        screenshakeVector = Vector3.Lerp(screenshakeVector, screenshakeTargetVector, Time.fixedDeltaTime * 30.0f);

	}

}
