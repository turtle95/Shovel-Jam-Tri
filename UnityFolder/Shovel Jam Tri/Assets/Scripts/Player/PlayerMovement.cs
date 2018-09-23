using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private Vector3 defaultMoveDirection;
    [SerializeField] private float defaultMoveSpeed;

    private Rigidbody rigidBodyComp;

    void Start()
    {
        rigidBodyComp = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rigidBodyComp.AddRelativeForce(defaultMoveDirection);
    }

    private void Update()
    {
        if (playerInput.type == MoveInputEventType.Tap)
        {
            Dash();
        }
        else if (playerInput.type == MoveInputEventType.Move)
        {
            MoveToward(playerInput.position);
        }
        else if (rigidBodyComp.velocity.magnitude < defaultMoveSpeed)
        {
            MoveToward(transform.position + defaultMoveDirection);
        }
    }

    struct PlayerInput
    {
        public MoveInputEventType type;
        public Vector3 position;

        public PlayerInput(MoveInputEventType t, Vector3 pos)
        {
            type = t;
            position = pos;
        }
    }

    private PlayerInput playerInput = new PlayerInput(MoveInputEventType.None, Vector3.zero);

    public void OnUserInputChangedEventHandler(MoveInputEventType type, Vector3 position)
    {
        playerInput.type = type;
        playerInput.position = position;

        if (type == MoveInputEventType.Tap)
            Dash();
    }

    public void MoveToward(Vector3 pos)
    {
        pos.z = transform.position.z;

        if (pos.x > transform.position.x)
        {
            rigidBodyComp.AddForce((pos - transform.position).normalized * Time.deltaTime, ForceMode.Impulse);
        }

        //uncomment this section to slow down
        //if (rigidBodyComp.velocity.magnitude > defaultMoveSpeed)
        //{
        //    rigidBodyComp.velocity = rigidBodyComp.velocity.normalized * defaultMoveSpeed;
        //}

        RotatePlayer();
    }

    public void Dash()
    {
        rigidBodyComp.AddForce(rigidBodyComp.velocity.normalized, ForceMode.Impulse);
    }
   
    private void RotatePlayer()
    {
        if (rigidBodyComp.velocity.magnitude > 0)
            transform.rotation = Quaternion.LookRotation(rigidBodyComp.velocity);
    }
}
