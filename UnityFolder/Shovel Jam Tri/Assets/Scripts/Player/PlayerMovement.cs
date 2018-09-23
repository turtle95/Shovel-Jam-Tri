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
            Dash(playerInput.position);
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
    }

    public void MoveToward(Vector3 pos)
    {
        pos.z = transform.position.z;
        rigidBodyComp.AddForce((pos - transform.position).normalized  * Time.deltaTime, ForceMode.Impulse);

        if (rigidBodyComp.velocity.magnitude > defaultMoveSpeed)
        {
            rigidBodyComp.velocity = rigidBodyComp.velocity.normalized * defaultMoveSpeed;
        }

        RotatePlayer();
        Debug.Log("Veclocity mag " + rigidBodyComp.velocity.magnitude);
    }

    public void Dash(Vector3 pos)
    {
        pos.z = transform.position.z;
        rigidBodyComp.AddForce(transform.forward * 100f * Time.deltaTime, ForceMode.Impulse);

        Debug.Log("Veclocity mag " + rigidBodyComp.velocity.magnitude);
    }
   
    private void RotatePlayer()
    {
        if (rigidBodyComp.velocity.magnitude > 0)
            transform.rotation = Quaternion.LookRotation(rigidBodyComp.velocity);
    }
}
