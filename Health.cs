using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Hunger Settings")]
    public float delay;
    public int hungerToRemove;
    public GameObject hungerBar;
    private bool hungerdamage;
    public int hunger;
    public int numOfHunger;
    public int hungerDamageToHealth;
    public Image[] HungerIcons;
    public Sprite fullHunger;
    public Sprite emptyHunger;

    [Header("Health Settings")]
    public GameObject PauseMenuHost;
    public GameObject DeathMenu;
    public Image[] HealthIcons;
    public int numOfHearts;
    public int health;
    
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public AudioClip[] hurtSounds;
    public AudioSource clipAudioSource;

    public float healthDamageRate;

    //coroutine
    void Start()
    {
        StartCoroutine(HungerCount(delay));
        StartCoroutine(HungerDamageCo());
        hungerdamage = false;
    }

    IEnumerator HungerCount(float delay)
    {
        while (true)
        {
            
            yield return new WaitForSeconds(delay);
            HungerHurt();

            if (hunger <= 0)
            {
                yield break;
            }
            else
            {
                yield return null;
            }
        }
        
            
    }


    IEnumerator HungerDamageCo()
    {
        while (true)
        {

            yield return new WaitForSeconds(0);
            hungerdamage = false;

            
        }


    }

    void HungerHurt()
    {
        hunger -= hungerToRemove;
        hungerdamage = true;
        
    }

    

    void Update()
    {
        if (hunger > numOfHunger)
        {
            hunger = numOfHunger;
        }

        for (int i = 0; i < HungerIcons.Length; i++)
        {
            if (i < hunger)
            {
                HungerIcons[i].sprite = fullHunger;
            }
            else
            {
                HungerIcons[i].sprite = emptyHunger;
            }

            if (i < numOfHunger)
            {
                HungerIcons[i].enabled = true;
            }
            else
            {
                HungerIcons[i].enabled = false;
            }

            //health
            if (health > numOfHearts)
            {
                health = numOfHearts;
            }

            for (int e = 0; e < HealthIcons.Length; e++)
            {
                if (e < health)
                {
                    HealthIcons[e].sprite = fullHeart;
                }
                else
                {
                    HealthIcons[e].sprite = emptyHeart;
                }

                if (i < numOfHearts)
                {
                    HealthIcons[e].enabled = true;
                }
                else
                {
                    HealthIcons[e].enabled = false;
                }
            }

            
            //HungerDamage TO health
            if (hunger <= 0)
            {
                StartCoroutine(HungerCount(healthDamageRate));

                IEnumerator HungerCount(float healthDamageRate)
                {
                    while (true)
                    {
                        yield return new WaitForSeconds(healthDamageRate);
                        HealthDamage();
                    }

                }
            }


            //death
            if (PauseMenuHost.GetComponent<PauseMenu>().paused == false && health <= 0)
            {
                DeathMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }

        //hurtsound
        if (hungerdamage == true && hunger <= 0)
        {
            int clipPick = Random.Range(0, hurtSounds.Length);
            clipAudioSource.PlayOneShot(hurtSounds[clipPick]);
        }

    }

    public void HealthDamage()
    {
        health -= hungerDamageToHealth;
    }

}
