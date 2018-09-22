using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health;
    
    public void TakeDamage(int damage)
    {
        _health -= damage;

        //TODO proper player dead handling
        if (_health <= 0)
        {
            Debug.Log("player died.");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Current Health = " + _health);
        }
    }
}
