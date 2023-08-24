using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy References")]
    public GameObject Gun; // for rotation
    public GameObject Aimtarget;
    public Vector3 InitialPosition;
    public int Damage;

    [Header("Shoot References")]
    public GameObject BulletEmitter;
    public GameObject BulletPrefab;
    public float ForwardForce;

    [Header("Max and Min Range")]
    public float ReloadTime;
    public Vector2 AttackTime;
    public float ShootInterval;

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
    public PlayerStats playerStats;
    private GameObject PlayerAim;

    private float currentTime;
    private float currentReloadTime;
    private float currentAttackTime;
    private float currentShootInterval;

    // private bool playerInRange; 

    private void Awake()
    {
        brain = GetComponent<StateMachine>();
        PlayerAim = GameObject.FindGameObjectWithTag("AimTarget");
        player = GameObject.FindGameObjectWithTag("Player").transform.root.gameObject;
    }
    void Start()
    {
        InitialPosition = Aimtarget.transform.position;
        brain.PushState(OnSearchEnter, OnSearchExit, Search);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerInRange = CanSeeTarget(player.transform, angle, radius);
    }

    void OnSearchEnter()
    {
        Debug.Log("Search Enter");
    }
    void Search()
    {
        // Rotate Turret

        if (playerInRange)
        {
            Debug.Log("Player detected, Leaving search");
            brain.PushState(OnAttackEnter, OnAttackExit, Attack);
        }
    }

    void OnSearchExit()
    {
        Debug.Log("Search Exit");
    }

    void OnAttackEnter()
    {
        Debug.Log("Attack Enter");
        currentAttackTime = Random.Range(AttackTime.x, AttackTime.y);
        currentTime = currentAttackTime;
        currentShootInterval = ShootInterval;
        if (playerStats == null)
        {
            playerStats = player.GetComponent<PlayerStats>();
        }

    }

    void Attack()
    {
        if (playerInRange)
        {
            if (currentTime >= 0)
            {
                Aimtarget.transform.position = PlayerAim.transform.position;
                if (currentShootInterval > 0)
                {
                    currentShootInterval -=Time.deltaTime;
                }
                else
                {
                    Debug.Log("Shooting");
                    Shoot();
                    playerStats.TakeDamage(Damage);
                    currentShootInterval = ShootInterval;
                }
                currentTime -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Pushing Reload");
                brain.PushState(OnReloadEnter, null, Reload);
            }
        }
        else
        {
            brain.PopState();
        }
    }

    void OnAttackExit()
    {
        Debug.Log("Attack Exit");
        Aimtarget.transform.position = InitialPosition;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(BulletPrefab, BulletEmitter.transform.position, Quaternion.identity);

        //Bullet angle correction.
        bullet.transform.Rotate(Vector3.left * 90);

        //Retrieve the Rigidbody component from the instantiated Bullet and control it.
        Rigidbody BulletRb;
        BulletRb = bullet.GetComponent<Rigidbody>();

        //Tell the bullet to be "pushed" forward by an amount set by ForwardForce. 
        BulletRb.AddForce(BulletEmitter.transform.forward * ForwardForce);

        //Basic Clean Up
        Destroy(bullet, 3f);
    }

    void OnReloadEnter()
    {
        Debug.Log("Reload Enter");
        currentReloadTime = ReloadTime;
    }

    void Reload()
    {
        if (currentReloadTime > 0)
        {
            currentReloadTime -= Time.deltaTime;
        }
        else
        {
            brain.PopState();
        }

    }

    bool CanSeeTarget(Transform target, float viewAngle, float viewRange)
    {
        Vector3 toTarget = target.position - BulletEmitter.transform.position;

        if (Vector3.Angle(BulletEmitter.transform.forward, toTarget) <= viewAngle)
        {
            if (Physics.Raycast(BulletEmitter.transform.position, toTarget, out RaycastHit hit, viewRange,targetMask))
            {
                Debug.DrawRay(BulletEmitter.transform.position, toTarget * hit.distance, Color.yellow);
                if (hit.transform.root == target)
                    return true;
            }
            else
            {
                Debug.DrawRay(BulletEmitter.transform.position, toTarget * 1000, Color.red);
            }
        }
        return false;

    }
}
