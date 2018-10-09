using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health;

    public GameObject scoreMenu;
    public AudioSource deathSoundSource;
    public AudioSource damageSound;
    public AudioSource damageSound2;


    public void TakeDamage(int damage)
    {
        _health -= damage;

        //TODO proper player dead handling
        if (_health <= 0)
        {
            Debug.Log("player died.");
            deathSoundSource.Play();
            
            scoreMenu.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            damageSound.Play();
            damageSound2.Play(); 
            Debug.Log("Current Health = " + _health);
        }
    }
}
