using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health;

    public GameObject scoreMenu;
    public AudioSource deathSoundSource;
    public AudioSource damageSound;
    public AudioSource damageSound2;

    public GameObject[] fullHealth;
    public GameObject[] injured;
    public GameObject[] dyin;

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health == 3)
        {
            for (int i = 0; i < 4; i++)
            {
                fullHealth[i].SetActive(true);
                injured[i].SetActive(false);
                dyin[i].SetActive(false);
            }
        }
        else if (_health == 2)
        {
            for(int i =0; i < 4; i++)
            {
                fullHealth[i].SetActive(false);
                injured[i].SetActive(true);
                dyin[i].SetActive(false);
            }
        }
        else if (_health == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                fullHealth[i].SetActive(false);
                injured[i].SetActive(false);
                dyin[i].SetActive(true);
            }
        }

        
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
