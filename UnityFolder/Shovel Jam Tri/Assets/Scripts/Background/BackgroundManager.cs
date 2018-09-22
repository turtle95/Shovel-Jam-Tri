using UnityEngine;

public class BackgroundManager : MonoBehaviour {

	public static BackgroundManager instance;
	[Header("This singleton manages all moving background elements")]
	[SerializeField] public ScrollingBackgroundController[] _scrollingBackground;
	[SerializeField] public ParticleSystemWrapper[] _particles;

	[SerializeField] public float _startSpeed = 0.0f;

	private float _backgroundSpeed = 0.0f;

	private void Awake() {
		MakeSingleton();
	}

	/// <summary>
	/// Set self as BackgroundManager.instance
	/// Destroy self is there's already another instance
	/// </summary>
	private void MakeSingleton() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(this);
		}
	}

	private void Start() {
		SetBackgroundSpeed(_startSpeed);
	}

	/// <summary>
	/// Set background speed to use for all background elements
	/// </summary>
	/// <param name="_speed">Left/right speed</param>
	public void SetBackgroundSpeed(float _speed) {
		if (_speed != _backgroundSpeed) {
			// Remember background speed
			_backgroundSpeed = _speed;

			ApplyBackgroundSpeedToScrollElements();
			ApplyBackgroundSpeedToParticleSystems();

		}

	}

	private void ApplyBackgroundSpeedToScrollElements() {
		// Send to all scrolling backgrounds
		for (int i = 0; i < _scrollingBackground.Length; ++i) {
			if (_scrollingBackground[i] != null) {
				_scrollingBackground[i].SetBackgroundSpeed(_backgroundSpeed);
			}
			
		}
	}

	private void ApplyBackgroundSpeedToParticleSystems() {
		// Send to all particle systems
		for (int i = 0; i < _particles.Length; ++i) {
			if (_particles[i] != null) {
				_particles[i].SetBackgroundSpeed(_backgroundSpeed);
			}
			
		}
	}

}
