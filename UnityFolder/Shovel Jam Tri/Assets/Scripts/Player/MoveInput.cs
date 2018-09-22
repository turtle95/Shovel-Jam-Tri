using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public enum MoveInputEventType
{
    Tap, Move
}

[System.Serializable]
public class MoveInputEvent : UnityEvent<MoveInputEventType, Vector3> { }

public class MoveInput : MonoBehaviour
{
    private struct Finger
    {
        public int ID;
        public float timeStamp;
        public Vector2 position;
        public TouchPhase touchPhase;

        public Finger(int id, float time, Vector3 pos, TouchPhase phase)
        {
            ID = id;
            timeStamp = time;
            position = pos;
            touchPhase = phase;
        }
    }

    [SerializeField] private Camera _cam;
    [SerializeField] private MoveInputEvent _onUserTouched;

    [System.NonSerialized] private List<Finger> activeFingers = new List<Finger>();

    void Update()
    {
        //touch screen stuff
        foreach (Touch touch in Input.touches)
        {
            HandleTouch(new Finger(touch.fingerId, Time.time, ComputeScreenToWorldPoint(touch.position), touch.phase));
        }

        if (Input.touchCount == 0)
        {
            //Mouse input
            if (Input.GetMouseButtonDown(0))
            {
                HandleTouch(new Finger(10, Time.time, ComputeScreenToWorldPoint(Input.mousePosition), TouchPhase.Began));
            }
            if (Input.GetMouseButton(0))
            {
                HandleTouch(new Finger(10, Time.time, ComputeScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved));
            }
            if (Input.GetMouseButtonUp(0))
            {
                HandleTouch(new Finger(10, Time.time, ComputeScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended));
            }
        }
    }

    private Vector3 ComputeScreenToWorldPoint(Vector3 position)
    {
        Ray ray = _cam.ScreenPointToRay(position);
        var point = ray.origin + (ray.direction * 10f);
        return point;
    }

    private void HandleTouch(Finger finger)
    {
        switch (finger.touchPhase)
        {
            case TouchPhase.Began:
                if (!activeFingers.Contains(finger))
                    activeFingers.Add(finger);
                break;

            case TouchPhase.Moved:
                Finger oldFinger = activeFingers.Find(f => f.ID == finger.ID);

                if (Vector3.Distance(oldFinger.position, finger.position) > 1f) //tolerance
                {
                    oldFinger.touchPhase = TouchPhase.Moved;
                    oldFinger.position = finger.position;
                    _onUserTouched.Invoke(MoveInputEventType.Move, oldFinger.position);
                }
                break;

            case TouchPhase.Ended:
                Finger registeredFinger = activeFingers.Find(f => f.ID == finger.ID);

                if (registeredFinger.touchPhase == TouchPhase.Moved) //this is move/swipe input, remove it.
                {
                    activeFingers.Remove(registeredFinger);
                    return;
                }

                float distance = Vector3.Distance(registeredFinger.position, finger.position);
                if (distance < 1f && (Time.time - registeredFinger.timeStamp < 0.5f)) //tolerance
                {
                    _onUserTouched.Invoke(MoveInputEventType.Tap, finger.position);
                }
                activeFingers.Remove(registeredFinger);
                break;
        }
    }

}
