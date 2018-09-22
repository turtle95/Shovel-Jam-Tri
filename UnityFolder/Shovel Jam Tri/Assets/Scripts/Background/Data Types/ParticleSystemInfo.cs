using UnityEngine;

/// <summary>
/// Stores information about a particle system
/// </summary>
public struct ParticleSystemInfo {

	public float _defaultSpeed;
	public float _defaultRate;
	public float _defaultLifetime;

	// Constructor
	public ParticleSystemInfo(float _startSpeed, float _rate, float _defaultLifetime) {
		this._defaultSpeed = _startSpeed;
		this._defaultRate = _rate;
		this._defaultLifetime = _defaultLifetime;
	}

}
