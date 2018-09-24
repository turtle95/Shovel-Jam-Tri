using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCopy : MonoBehaviour {

    //the direction and speed you move when nothing is being pressed
    [SerializeField] private Vector3 defaultMoveDirection;
    public float defaultMoveSpeed;

    //your rigid body
    private Rigidbody rigidBodyComp;

    //your current speed?
    [HideInInspector] public float runTimeMoveSpeed;


    //complicated programmer stuff
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




    //initialize speed and rigidbody
    void Start()
    {
        rigidBodyComp = GetComponent<Rigidbody>();
        runTimeMoveSpeed = defaultMoveSpeed;
    }

    
    private void Update()
    {
        if (playerInput.type == MoveInputEventType.Tap) //if tap input is detected run dash function
        {
            Dash();
        }
        else if (playerInput.type == MoveInputEventType.Move) //if Move input is detected add force towards the touch position
        {
            MoveToward(playerInput.position);
        }
        else if (rigidBodyComp.velocity.magnitude < runTimeMoveSpeed) //if moving slower than run speed add force forward
        {
            MoveToward(transform.position + defaultMoveDirection);
        }
    }

   //called by something else, updates the input type info
    public void OnUserInputChangedEventHandler(MoveInputEventType type, Vector3 position)
    {
        playerInput.type = type;
        playerInput.position = position;

        //another call to dash on tap...why?
        if (type == MoveInputEventType.Tap)
            Dash();
    }

    //adds force in the direction of the move touch
    public void MoveToward(Vector3 pos)
    {
        pos.z = transform.position.z; //sets z of target position to the same as the player's current position

        //if the touch is in front of the player
        if (pos.x < transform.position.x)
        {
            //ads a force in the direction of the touch, at the run speed ...using impulse
            rigidBodyComp.AddForce((pos - transform.position).normalized * runTimeMoveSpeed * Time.deltaTime, ForceMode.Impulse);
            if (rigidBodyComp.velocity.magnitude > runTimeMoveSpeed) //if player's vel is higher than the move speed then set the velocity to moveSpeed
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
        runTimeMoveSpeed += 10; //adds 10 to your normal speed, doesn't look like this is ever reset
        rigidBodyComp.AddForce(rigidBodyComp.velocity.normalized * runTimeMoveSpeed, ForceMode.Impulse);  //adds a force in the current direction at the run speed using impulse

        //stuffs
        //if (rigidBodyComp.velocity.magnitude > runTimeMoveSpeed)
        //{
        //    rigidBodyComp.velocity = rigidBodyComp.velocity.normalized * runTimeMoveSpeed;
        //}
    }

    private void RotatePlayer()
    {
        if (rigidBodyComp.velocity.magnitude > 0) //if you are moving
            transform.rotation = Quaternion.LookRotation(rigidBodyComp.velocity); //rotate to match your velocity, should probably change this to only work when you aren't pressing something...really good code though
    }
}
