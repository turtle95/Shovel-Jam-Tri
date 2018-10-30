using UnityEngine;

/// <summary>
/// Take damange from Player (objects with "player" tag) on collision. 
/// </summary>
public class ObstacleCollision : MonoBehaviour
{
    [SerializeField] private int _damage;

    public GameObject explosion;

    private void OnCollisionEnter(Collision other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("Player"))
        {
            Score score = obj.GetComponent<Score>();
            if (score) {
                score.ResetCombo();
            }

            Health healthComp = obj.GetComponent<Health>();
            if (healthComp) {
                healthComp.TakeDamage(_damage, true);
            }

            Rigidbody playerRigid = obj.GetComponent<Rigidbody>();
            if (playerRigid) {
                playerRigid.velocity = Vector3.zero;
            }

            Transform playerT = obj.GetComponent<Transform>();
            if (playerT) {
                playerT.position = new Vector3(playerT.position.x, playerT.position.y, 0);
            }

            if (explosion) {
                Instantiate(explosion, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
