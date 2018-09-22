using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Manages scrolling image
/// </summary>
public class ScrollingBackgroundController : MonoBehaviour {

	[SerializeField] public GameObject _template;
	[SerializeField] public float _speedMult = 1.0f;

	private float _imageWidth;
	private float _currentScrollSpeed = 0.0f;
	private ScrollingBackgroundImage[] _scrollingImage;

	private Transform _myTransform;

	private void Awake () {
		Assert.IsNotNull(_template);

		_myTransform = GetComponent<Transform>();

		DoubleScrollingImage();

	}

	private void Start () {
		ApplyScrollSpeed();
	}

	/// <summary>
	/// Create another image to allow scrolling
	/// </summary>
	private void DoubleScrollingImage() {

		GameObject secondImageObject = Instantiate(_template) as GameObject;
		Transform secondImageTransform = secondImageObject.GetComponent<Transform>();
		secondImageTransform.parent = _myTransform;

		_imageWidth = _template.GetComponent<SpriteRenderer>().bounds.size.x;

		secondImageTransform.localPosition = _template.GetComponent<Transform>().localPosition + Vector3.right * _imageWidth;

		_scrollingImage = new ScrollingBackgroundImage[2];
		_scrollingImage[0] = _template.GetComponent<ScrollingBackgroundImage>();
		_scrollingImage[1] = secondImageObject.GetComponent<ScrollingBackgroundImage>();

		Assert.IsNotNull(_scrollingImage[0]);
		Assert.IsNotNull(_scrollingImage[1]);

		
	}

	/// <summary>
	/// Send scroll speed to image handlers
	/// </summary>
	public void SetBackgroundSpeed(float _speed) {
		_currentScrollSpeed = _speed * _speedMult;
		ApplyScrollSpeed();
	}

	/// <summary>
	/// Send scroll speed to image handlers
	/// </summary>
	private void ApplyScrollSpeed() {
		for (int i = 0; i < _scrollingImage.Length; ++i) {
			_scrollingImage[i].SetScrollSpeed(_currentScrollSpeed);
		}
		
	}

}
