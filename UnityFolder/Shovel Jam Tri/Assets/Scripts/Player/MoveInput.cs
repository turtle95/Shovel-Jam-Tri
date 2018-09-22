using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public enum MoveInputEventType
{
    Tap, SwipeUp, SwipeDown
}
[System.Serializable]
public class MoveInputEvent : UnityEvent<MoveInputEventType> { }
public class MoveInput : MonoBehaviour
{
    private struct Finger
    {
        public int ID;
        public Vector2 position;
        public TouchPhase touchPhase;

        public Finger(int id, Vector3 pos, TouchPhase phase)
        {
            ID = id;
            position = pos;
            touchPhase = phase;
        }
    }
    public Camera cam;
    [SerializeField] private MoveInputEvent _onUserTouched;

    [System.NonSerialized] private List<Finger> activeFingers = new List<Finger>();

    void Update()
    {
        //touch screen stuff
        foreach (Touch touch in Input.touches)
        {
            HandleTouch(new Finger(touch.fingerId, ComputeScreenToWorldPoint(touch.position), touch.phase));
        }

        if (Input.touchCount == 0)
        {
            //Mouse input
            if (Input.GetMouseButtonDown(0))
            {
                HandleTouch(new Finger(10, ComputeScreenToWorldPoint(Input.mousePosition), TouchPhase.Began));
            }
            if (Input.GetMouseButton(0))
            {
                HandleTouch(new Finger(10, ComputeScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved));
            }
            if (Input.GetMouseButtonUp(0))
            {
                HandleTouch(new Finger(10, ComputeScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended));
            }
        }
    }

    private Vector3 ComputeScreenToWorldPoint(Vector3 position)
    {
        Ray ray = cam.ScreenPointToRay(position);
        var point = ray.origin + (ray.direction * 10f);
        return point;
    }

    private void HandleTouch(Finger finger)
    {
        switch (finger.touchPhase)
        {
            case TouchPhase.Began:
                if (!activeFingers.Contains(finger))
                {
                    Debug.Log("touch id BEGAN " + finger.ID + " Position: " + finger.position);
                    activeFingers.Add(finger);
                }
                break;
            case TouchPhase.Ended:
                Finger registeredFinger = activeFingers.Find(f => f.ID == finger.ID);
                float distance = Vector3.Distance(registeredFinger.position, finger.position);
                if (distance < 0.01f) //tolerance
                {
                    Debug.Log("User tap/click detected");
                    _onUserTouched.Invoke(MoveInputEventType.Tap);
                }
                else
                {
                    Vector2 direct = finger.position - registeredFinger.position;

                    if (direct.y > 0)
                    {
                        Debug.Log("User swiped up");
                        _onUserTouched.Invoke(MoveInputEventType.SwipeUp);
                    }
                    else
                    {
                        Debug.Log("User swiped down");
                        _onUserTouched.Invoke(MoveInputEventType.SwipeDown);
                    }
                }
                activeFingers.Remove(registeredFinger);
                break;
        }
    }

}
