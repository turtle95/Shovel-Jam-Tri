using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private Vector3 defaultMoveDirection;

    private Rigidbody rigidBodyComp;

    void Start()
    {
        rigidBodyComp = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rigidBodyComp.AddRelativeForce(defaultMoveDirection);
    }

    public void OnUserInputChangedEventHandler(MoveInputEventType type, Vector3 position)
    {
        Debug.Log(type + " Position: " + position);
    }
}
