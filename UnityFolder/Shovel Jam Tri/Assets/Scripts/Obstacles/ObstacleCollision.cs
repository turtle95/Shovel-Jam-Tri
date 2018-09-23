﻿using UnityEngine;

/// <summary>
/// Take damange from Player (objects with "player" tag) on collision. 
/// </summary>
public class ObstacleCollision : MonoBehaviour
{
    [SerializeField] private int _damage;

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Health healthComp = collision.gameObject.GetComponent<Health>();
            healthComp.TakeDamage(_damage);

            PlayerMovement playerMove = collision.gameObject.GetComponent<PlayerMovement>();
            playerMove.runTimeMoveSpeed = playerMove.defaultMoveSpeed;

            Rigidbody playerRigid = collision.gameObject.GetComponent<Rigidbody>();
            playerRigid.velocity = Vector3.zero;

            Destroy(gameObject);
        }
    }
}
