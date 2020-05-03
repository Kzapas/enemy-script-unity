using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBorder : MonoBehaviour
{

    public GameObject Enemy;
    public float Spotting_Distance_Enter;
    public float Spotting_Distance_Exit;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slender"))
        {
            Enemy.GetComponent<EnemyFollow>().Spotting_Distance = Spotting_Distance_Enter;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Slender"))
        {
            Enemy.GetComponent<EnemyFollow>().Spotting_Distance = Spotting_Distance_Exit;
        }
    }
}
