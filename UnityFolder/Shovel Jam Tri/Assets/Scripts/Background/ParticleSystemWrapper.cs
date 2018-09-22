using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Component to make changing particle system properties easier
/// The particle system itself should be set up in the inspector
/// to match a background speed of 1.0
/// </summary>
public class ParticleSystemWrapper : MonoBehaviour {

	[Header("Set values in inspector to match rate of 1.0")]
	private ParticleSystem _sys;
	private ParticleSystem.MainModule _main;
	private ParticleSystem.EmissionModule _emission;
	private ParticleSystemInfo _info;

	private float _backgroundSpeed = 1.0f;

	private void Awake () {
		_sys = GetComponent<ParticleSystem>();
		Assert.IsNotNull(_sys);

		GetParticleSystemInfo();

	}

	private void GetParticleSystemInfo() {
		_main = _sys.main;
		_emission = _sys.emission;

		_info = new ParticleSystemInfo(
				_main.startSpeed.constant,
				_emission.rate.constant,
				_main.startLifetime.constant
			);
	}
	
	public void SetBackgroundSpeed(float _speed) {
		if (_backgroundSpeed != _speed) {
			_backgroundSpeed = _speed;

			float absSpeed = Mathf.Abs(_backgroundSpeed);

			// Set new velocity
			_main.startSpeed = _info._defaultSpeed * absSpeed;

			// Set new emission rate
			_emission.rate = _info._defaultRate * absSpeed;

			// Set lifetime
			if (absSpeed > 0.0f) {
				_main.startLifetime = _info._defaultLifetime / absSpeed;
			} else {
				// The system will not be emitting here anyways
				_main.startLifetime = 1.0f;
			}

		}

	}

}
