using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy References")]
    public GameObject Gun; // for rotation
    public GameObject Aimtarget;

    [Header("Max and Min Range")]
    public Vector2 ReloadTime;
    public Vector2 AttackTime;

    [Header("Field of View References")]
    public float radius;
    [Range(0, 360)]
    public float angle;
    public GameObject playerRef;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    private bool playerInRange; // turn true when player can be detected

    private StateMachine brain;
    private GameObject player; // to store target
   // private bool playerInRange; 

    private void Awake()
    {
        brain = GetComponent<StateMachine>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FieldOfViewCheck();
    }
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    playerInRange = true;
                    player = target.transform.gameObject;
                }
                    
                else
                {
                    playerInRange = false;
                }
                   
            }
            else
                playerInRange = false;
        }
        else if (playerInRange)
            playerInRange = false;
    }

    void OnSearchEnter()
    {

    }
    void Search()
    {
        // Rotate Turret
       
        if (playerInRange)
        {
            brain.PushState(OnAttackEnter, OnAttackExit, Attack);
        }
    }

    void OnSearchExit()
    {

    }

    void OnAttackEnter()
    {
        Aimtarget.transform.position = player.transform.position;
    }

    void Attack()
    {
        if(playerInRange)
        {

        }
        else
        {
            brain.PopState();
        }
    }

    void OnAttackExit()
    {
        //target = 
    }



}
