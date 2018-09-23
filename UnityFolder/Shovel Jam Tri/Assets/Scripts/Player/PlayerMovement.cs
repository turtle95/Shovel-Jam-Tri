using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private Vector3 defaultMoveDirection;
    public float defaultMoveSpeed;

    private Rigidbody rigidBodyComp;
    [HideInInspector] public float runTimeMoveSpeed;

    void Start()
    {
        rigidBodyComp = GetComponent<Rigidbody>();
        runTimeMoveSpeed = defaultMoveSpeed;
    }

    private void FixedUpdate()
    {
        //rigidBodyComp.AddRelativeForce(defaultMoveDirection);
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
        else if (rigidBodyComp.velocity.magnitude < runTimeMoveSpeed)
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

        if (pos.x < transform.position.x)
        {
            rigidBodyComp.AddForce((pos - transform.position).normalized * runTimeMoveSpeed * Time.deltaTime, ForceMode.Impulse);
            if (rigidBodyComp.velocity.magnitude > runTimeMoveSpeed)
            {
                rigidBodyComp.velocity = rigidBodyComp.velocity.normalized * runTimeMoveSpeed;
            }
        }

        //uncomment this section to slow down
        //if (rigidBodyComp.velocity.magnitude > defaultMoveSpeed)
        //{
        //    rigidBodyComp.velocity = rigidBodyComp.velocity.normalized * defaultMoveSpeed;
        //}
        //Debug.Log(rigidBodyComp.velocity.magnitude);
        RotatePlayer();
    }

    public void Dash()
    {
        runTimeMoveSpeed += 10;
        rigidBodyComp.AddForce(rigidBodyComp.velocity.normalized * runTimeMoveSpeed, ForceMode.Impulse);
        //if (rigidBodyComp.velocity.magnitude > runTimeMoveSpeed)
        //{
        //    rigidBodyComp.velocity = rigidBodyComp.velocity.normalized * runTimeMoveSpeed;
        //}
    }
   
    private void RotatePlayer()
    {
        if (rigidBodyComp.velocity.magnitude > 0)
            transform.rotation = Quaternion.LookRotation(rigidBodyComp.velocity);
    }
}
