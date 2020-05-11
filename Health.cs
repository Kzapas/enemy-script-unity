using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Hunger Settings")]
    public float delay;
    public int hungerToRemove;
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
    public float secondsUntilHeal;
    public int HealValue;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private bool damage;
    public AudioClip[] hurtSounds;
    public AudioSource clipAudioSource;

    public float healthDamageRate;

    [Header("Other Settings")]
    public GameObject Player;

    //coroutine
    void Start()
    {
        StartCoroutine(HungerCount(delay));
        
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
                StartCoroutine(HungerCount2(healthDamageRate));
                yield break;
            }
            else
            {
                yield return null;
            }
        }
        
            
    }


    

    void HungerHurt()
    {
        hunger -= hungerToRemove;
    }

    

    void Update()
    {
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

            if (e < numOfHearts)
            {
                HealthIcons[e].enabled = true;
            }
            else
            {
                HealthIcons[e].enabled = false;
            }
        }

        //hunger
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
        }



        //---------------------------Section 2-----------------------------


        

        //DEATH
        if (PauseMenuHost.GetComponent<PauseMenu>().paused == false && health <= 0)
        {
        DeathMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Player.GetComponent<PlayerController>().enabled = false;
        PauseMenuHost.GetComponent<PauseMenu>().enabled = false;
                
        }





        //hurtsound
        if (hungerdamage == true && hunger <= 0 && health >= 1)
        {
            int clipPick = Random.Range(0, hurtSounds.Length);
            clipAudioSource.PlayOneShot(hurtSounds[clipPick]);
        }

        
    }

    //Health Hunger Damage Continued
    IEnumerator HungerCount2(float healthDamageRate)
    {
        while (true)
        {
            hungerdamage = false;
            yield return new WaitForSeconds(healthDamageRate);
            Subtract();

            if (health <= 0)
            {
                yield break;
            }
            else
            {
                yield return null;
            }
        }


    }



    void Subtract()
    {
        health -= hungerDamageToHealth;
        hungerdamage = true;
        Debug.Log("Hurtsounds");
    }

}
