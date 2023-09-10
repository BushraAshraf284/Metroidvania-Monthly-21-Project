﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileTracking : MonoBehaviour
{
    [SerializeField]
    GameObject homingIcon1, homingIcon2, homingIcon3;
    [SerializeField]
    GameObject lockedIcon1, lockedIcon2, lockedIcon3;
    [SerializeField]
    GameObject trackingIcon1, trackingIcon2, trackingIcon3;
    Transform player;
    [SerializeField]
    [Tooltip("How long it takes to lock on to a target")]
    float lockingCap = 2f;
    float lockingCount1, lockingCount2, lockingCount3;
    public List<GameObject> inRangeObjects = new List<GameObject>();
    float shortestDistance = 9999f;
    [SerializeField]
    public GameObject nearestTarget, nearestTarget2, nearestTarget3;
    [SerializeField]
    public GameObject target1, target2, target3;
    [SerializeField]
    LayerMask mask;
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
	    if((other.gameObject.tag == "Breakable" || other.gameObject.tag == "Targetable") && other.gameObject.GetComponent<Targetable>() != null){
            if(inRangeObjects.Contains(other.gameObject)){
            }
            else{
                RaycastHit hit;
                if(Physics.Raycast(player.transform.position, (other.gameObject.GetComponent<Targetable>().targetFocusPoint.position - player.transform.position), out hit, Mathf.Infinity, mask)){
                    Debug.DrawRay(player.transform.position, (other.gameObject.GetComponent<Targetable>().targetFocusPoint.position - player.transform.position), Color.green, 5f);
	                if((hit.collider.gameObject.tag == "Breakable" || hit.collider.gameObject.tag == "Targetable") && hit.collider.gameObject.GetComponent<Targetable>() != null){
                        inRangeObjects.Add(other.gameObject);
                        FindClosestTarget();
                        FindSecondClosestTarget();
                        FindThirdClosestTarget();
                    }
                }

            }
        }
    }
    void OnTriggerExit(Collider other)
    {
	    if(other.gameObject.tag == "Targetable" || other.gameObject.tag == "Breakable"){
            if(inRangeObjects.Contains(other.gameObject)){
                inRangeObjects.Remove(other.gameObject);
                FindClosestTarget();
                FindSecondClosestTarget();
                FindThirdClosestTarget();
            }
            else{
            }
        }
    }
    public void ClearList(){
        nearestTarget = null;
        nearestTarget2 = null;
        nearestTarget3 = null;
        lockingCount1 = 0f;
        lockingCount2 = 0f;
        lockingCount3 = 0f;
        target1 = null;
        target2 = null;
        target3 = null;
        inRangeObjects.Clear();
        homingIcon1.SetActive(false);
        homingIcon2.SetActive(false);
        homingIcon3.SetActive(false);

    }
	public void ClearHud(){
		homingIcon1.SetActive(false);
		homingIcon2.SetActive(false);
		homingIcon3.SetActive(false);
	}
    // Start is called before the first frame update
    void Start()
    {
        ClearList();
        player = GameObject.Find("3rd Person Character").GetComponent<Movement>().playerCenter;
    }
    void FindClosestTarget(){
        shortestDistance = 9999f;
        nearestTarget = null;
        foreach(GameObject g in inRangeObjects){
            if(Vector3.Distance(g.transform.position, player.transform.position) < shortestDistance){
                nearestTarget = g.GetComponent<Targetable>().targetFocusPoint.gameObject;
                shortestDistance = Vector3.Distance(g.transform.position, player.transform.position);
                lockingCount1 = 0f;
                target1 = null;
                homingIcon1.SetActive(true);
                trackingIcon1.SetActive(true);
                lockedIcon1.SetActive(false);
                homingIcon1.GetComponent<TrackUI>().subject = nearestTarget.transform;
            }
        }
    }
    void FindSecondClosestTarget(){
        shortestDistance = 9999f;
        nearestTarget2 = null;
        foreach(GameObject g in inRangeObjects){
            if(g.GetComponent<Targetable>().targetFocusPoint.gameObject != nearestTarget){
                if(Vector3.Distance(g.transform.position, player.transform.position) < shortestDistance){
                    nearestTarget2 = g.GetComponent<Targetable>().targetFocusPoint.gameObject;
                    shortestDistance = Vector3.Distance(g.transform.position, player.transform.position);
                    lockingCount2 = 0f;
                    target2 = null;
                    homingIcon2.SetActive(true);
                    trackingIcon2.SetActive(true);
                    lockedIcon2.SetActive(false);
                    homingIcon2.GetComponent<TrackUI>().subject = nearestTarget2.transform;
                }
            }

        }
    }

    void FindThirdClosestTarget(){
        shortestDistance = 9999f;
        nearestTarget3 = null;
        foreach(GameObject g in inRangeObjects){
            if(g.GetComponent<Targetable>().targetFocusPoint.gameObject != nearestTarget && g.GetComponent<Targetable>().targetFocusPoint.gameObject != nearestTarget2){
                if(Vector3.Distance(g.transform.position, player.transform.position) < shortestDistance){
                    nearestTarget3 = g.GetComponent<Targetable>().targetFocusPoint.gameObject;
                    shortestDistance = Vector3.Distance(g.transform.position, player.transform.position);
                    lockingCount3 = 0f;
                    target3 = null;
                    homingIcon3.SetActive(true);
                    trackingIcon3.SetActive(true);
                    lockedIcon3.SetActive(false);
                    homingIcon3.GetComponent<TrackUI>().subject = nearestTarget3.transform;
                }
            }

        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if(nearestTarget != null){
            lockingCount1 += Time.deltaTime;
            if(lockingCount1 > lockingCap){
                lockingCount1 = 0f;
                target1 = nearestTarget;
                lockedIcon1.SetActive(true);
                homingIcon1.GetComponent<TrackUI>().subject = nearestTarget.transform;
                trackingIcon1.SetActive(false);
            }
        }
        if(nearestTarget2 != null){
            lockingCount2 += Time.deltaTime;
            if(lockingCount2 > lockingCap){
                lockingCount2 = 0f;
                target2 = nearestTarget2;
                lockedIcon2.SetActive(true);
                homingIcon2.GetComponent<TrackUI>().subject = nearestTarget2.transform;
                trackingIcon2.SetActive(false);
            }
        }
        if(nearestTarget3 != null){
            lockingCount3 += Time.deltaTime;
            if(lockingCount3 > lockingCap){
                lockingCount3 = 0f;
                target3 = nearestTarget3;
                lockedIcon3.SetActive(true);
                homingIcon3.GetComponent<TrackUI>().subject = nearestTarget3.transform;
                trackingIcon3.SetActive(false);
            }
        }
    }
}
