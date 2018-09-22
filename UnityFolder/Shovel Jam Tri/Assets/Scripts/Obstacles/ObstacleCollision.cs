using UnityEngine;

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
        }
    }
}
