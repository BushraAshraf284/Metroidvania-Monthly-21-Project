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

    private StateMachine brain;
    private GameObject player; // to store target
    private bool playerInRange; // turn true when player can be detected

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
        
    }

    void Search()
    {

    }



}
