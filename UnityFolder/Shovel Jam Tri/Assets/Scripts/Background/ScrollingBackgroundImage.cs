using UnityEngine;
using UnityEngine.Assertions;

public class ScrollingBackgroundImage : MonoBehaviour {

	private Rigidbody2D _rigidbody;
	private float _imageWidth;
	private bool _isScrollingRight;

	private void Awake() {
		_rigidbody = GetComponent<Rigidbody2D>();
		Assert.IsNotNull(_rigidbody);

		_imageWidth = GetComponent<SpriteRenderer>().bounds.size.x;

	}

	private void Update() {
		StayOnScreen();
	}

	/// <summary>
	/// Sets scroll speed of image
	/// </summary>
	/// <param name="_scrollSpeed">Left/right speed</param>
	public void SetScrollSpeed(float _scrollSpeed) {
		// Set velocity
		_rigidbody.velocity = new Vector2(_scrollSpeed, 0.0f);

		// Remember direction
		_isScrollingRight = (_scrollSpeed > 0.0f);

	}

	/// <summary>
	/// Scrolling behavior to stay on screen
	/// </summary>
	private void StayOnScreen() {
		float rightScreenPos = 20.0f;

		// Adjust position to stay on screen
		if (_isScrollingRight) {

			if (_rigidbody.position.x > _imageWidth) {
				_rigidbody.position = new Vector2(
						_rigidbody.position.x - _imageWidth * 2.0f,
						_rigidbody.position.y
					);
			}

		} else {

			if (_rigidbody.position.x < -_imageWidth) {
				_rigidbody.position = new Vector2(
						_rigidbody.position.x + _imageWidth * 2.0f,
						_rigidbody.position.y
					);
			}

		}

	}

}
