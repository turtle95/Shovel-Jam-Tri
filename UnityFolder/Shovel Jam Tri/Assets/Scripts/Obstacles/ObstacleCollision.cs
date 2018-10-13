using UnityEngine;

/// <summary>
/// Take damange from Player (objects with "player" tag) on collision. 
/// </summary>
public class ObstacleCollision : MonoBehaviour
{
    [SerializeField] private int _damage;

    public GameObject explosion;

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Health healthComp = collision.gameObject.GetComponent<Health>();
            healthComp.TakeDamage(_damage);

            Rigidbody playerRigid = collision.gameObject.GetComponent<Rigidbody>();
            playerRigid.velocity = Vector3.zero;

            Transform playerT = collision.gameObject.GetComponent<Transform>();
            playerT.position = new Vector3(playerT.position.x, playerT.position.y, 0);

            collision.gameObject.BroadcastMessage("ResetCombo");

            Instantiate(explosion, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
