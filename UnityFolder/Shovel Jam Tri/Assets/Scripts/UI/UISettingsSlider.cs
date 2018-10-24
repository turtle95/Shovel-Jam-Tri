using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettingsSlider : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image _primaryBtn;
    [SerializeField] private RectTransform _scrollView;
    [SerializeField] private GridLayoutGroup _contentGroup;

    [Header("Values")]
    [SerializeField] private float _slideTime = 0.5f;
    [SerializeField] private bool _showOnStart;

    private float _minSize;
    private float _maxHeight;
    private float _topPos;

    private bool _shouldShowUp;
    private float _elapsed;

	void Start ()
    {
        var btnSize = _primaryBtn.GetPixelAdjustedRect().size;
        _minSize = Mathf.Min(btnSize.x, btnSize.y);

        _scrollView.sizeDelta = new Vector2(_minSize, _minSize);

        float childBtnSize = _minSize - 0.2f * _minSize;
        float vertSpacing = childBtnSize * 0.5f;

        _contentGroup.cellSize = new Vector2(childBtnSize, childBtnSize);
        _contentGroup.spacing = new Vector2(0, vertSpacing);
        _contentGroup.padding = new RectOffset(0, 0, (int)vertSpacing/2, (int)vertSpacing);

        //height = childCount * (size + spacing)  + top padding + botton padding (including primary btn)
        int childCount = _contentGroup.transform.childCount;
        _maxHeight = childCount * (childBtnSize + vertSpacing) + (int)vertSpacing/2 + childBtnSize/2;
        _topPos = _maxHeight / 2f;

        _shouldShowUp = _showOnStart;
    }
	
	// Update is called once per frame
	void Update ()
    {
        _elapsed += Time.deltaTime;
        if (_shouldShowUp && _scrollView.sizeDelta.y != _maxHeight && _scrollView.localPosition.y != _topPos)
        {
            float newHeight = Mathf.Lerp(_minSize, _maxHeight, _elapsed / _slideTime);
            float newTop = Mathf.Lerp(0, _topPos, _elapsed / _slideTime);

            _scrollView.sizeDelta = new Vector2(_scrollView.sizeDelta.x, newHeight);
            _scrollView.localPosition = new Vector3(_scrollView.localPosition.x, newTop, _scrollView.localPosition.z);
        }
        else if (!_shouldShowUp && _scrollView.sizeDelta.y != _minSize && _scrollView.localPosition.y != 0)
        {
            float newHeight = Mathf.Lerp(_maxHeight, _minSize, _elapsed / _slideTime);
            float newTop = Mathf.Lerp(_topPos, 0, _elapsed / _slideTime);

            _scrollView.sizeDelta = new Vector2(_scrollView.sizeDelta.x, newHeight);
            _scrollView.localPosition = new Vector3(_scrollView.localPosition.x, newTop, _scrollView.localPosition.z);
        }
        else
        {
            _elapsed = 0;
        }

    }

    public void ToggleVisibility()
    {
        _shouldShowUp = !_shouldShowUp;
    }

    public void ToggleHard()
    {
        _shouldShowUp = !_shouldShowUp;

        if (_shouldShowUp)
        {
            _scrollView.sizeDelta = new Vector2(_scrollView.sizeDelta.x, _maxHeight);
            _scrollView.localPosition = new Vector3(_scrollView.localPosition.x, _topPos, _scrollView.localPosition.z);
        }
        else
        {
            _scrollView.sizeDelta = new Vector2(_scrollView.sizeDelta.x, _minSize);
            _scrollView.localPosition = new Vector3(_scrollView.localPosition.x, 0, _scrollView.localPosition.z);
        }
    }
}
