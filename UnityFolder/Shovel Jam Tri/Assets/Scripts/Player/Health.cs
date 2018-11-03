using UnityEngine;

public class Health : MonoBehaviour
{
    public int startHealth;
    public int maxHealth;

    
    public GameObject scoreMenu;
    public AudioSource deathSoundSource;
    public AudioSource damageSound;
    public AudioSource damageSound2;

    public GameObject[] fullHealth;
    public GameObject[] injured;
    public GameObject[] dyin;


    public Material[] whaleMats;
    public Color[] fHealthColors;
    public Color[] injuredColors;
    public Color[] dyinColors;

    private int health; //runtime health



    void Start()
    {
        health = startHealth;
        for (int j = 0; j < whaleMats.Length; j++)
        {
            whaleMats[j].color = fHealthColors[j];
        }
    }

    public void GetHealed(int healing) {
        SetHealth(health + healing);
        
    }

	public void TakeDamage(int damage, bool playSounds)
    {
        SetHealth(health - damage);

        //TODO proper player dead handling
        if (health <= 0)
        {
          //  Debug.Log("player died.");
            deathSoundSource.Play();
            
            scoreMenu.SetActive(true);
            gameObject.SetActive(false);
        }
		else if(playSounds)
        {
            damageSound.Play();
            damageSound2.Play(); 
          //  Debug.Log("Current Health = " + health);
        }

    }

    private void SetHealth(int newHealth) {
        health = newHealth;

        if (health > maxHealth) {
            health = maxHealth;
        }

        if (health == 3)
        {
            for(int j = 0; j<whaleMats.Length; j++)
            {
                whaleMats[j].color = fHealthColors[j];
            }
            for (int i = 0; i < 4; i++)
            {
                fullHealth[i].SetActive(true);
                injured[i].SetActive(false);
                dyin[i].SetActive(false);
            }
        }
        else if (health == 2)
        {
            for (int j = 0; j < whaleMats.Length; j++)
            {
                whaleMats[j].color = injuredColors[j];
            }
            for (int i =0; i < 4; i++)
            {
                fullHealth[i].SetActive(false);
                injured[i].SetActive(true);
                dyin[i].SetActive(false);
            }
        }
        else if (health == 1)
        {
            for (int j = 0; j < whaleMats.Length; j++)
            {
                whaleMats[j].color = dyinColors[j];
            }
            for (int i = 0; i < 4; i++)
            {
                fullHealth[i].SetActive(false);
                injured[i].SetActive(false);
                dyin[i].SetActive(true);
            }
        }

    }

}
