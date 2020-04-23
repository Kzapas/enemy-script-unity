using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyFollow : MonoBehaviour
{
    public GameObject Player;
    private float TargetDistance;
    public float Allowed_Distance = 5;
    public float Spotting_Distance = 30;
    public GameObject Enemy;
    private float Follow_Speed = 1;
    public float Running_Speed = 0.1f;
    public RaycastHit Shot;

    public void Start()
    {
        Follow_Speed = 0;
        Enemy.GetComponent<Animator>().SetBool("Idle", true);
    }
    private void Update()
    {
        
        TargetDistance = Vector3.Distance(Player.transform.position, Enemy.transform.position);
        transform.LookAt(Player.transform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (TargetDistance > Allowed_Distance && TargetDistance <= Spotting_Distance)
        {
            Follow_Speed = Running_Speed;
            Enemy.GetComponent<Animator>().SetBool("Run", true);
            Enemy.GetComponent<Animator>().SetBool("Idle", false);
            Enemy.GetComponent<Animator>().SetBool("Attacking", false);
            Debug.Log("Running");
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Follow_Speed);

        }
        

       if(TargetDistance <= Allowed_Distance)
        {
            Follow_Speed = 0;
            Enemy.GetComponent<Animator>().SetBool("Attack", true);
            Enemy.GetComponent<Animator>().SetBool("Run", false);
            Enemy.GetComponent<Animator>().SetBool("Idle", false);
            Debug.Log("Attacking");
        }

        if (TargetDistance >= Allowed_Distance && Enemy.GetComponent<Animator>().GetBool("Attack") == true)
        {
            Follow_Speed = 0;
            Enemy.GetComponent<Animator>().SetBool("Attack", false);
            Enemy.GetComponent<Animator>().SetBool("Run", true);
            Enemy.GetComponent<Animator>().SetBool("Idle", false);
            Debug.Log("Attacking");
        }

        if (TargetDistance > Spotting_Distance)
        {
            Follow_Speed = 0;
            Enemy.GetComponent<Animator>().SetBool("Attack", false);
            Enemy.GetComponent<Animator>().SetBool("Run", false);
            Enemy.GetComponent<Animator>().SetBool("Idle", true);
            Debug.Log("Idle");
        }

    }

}