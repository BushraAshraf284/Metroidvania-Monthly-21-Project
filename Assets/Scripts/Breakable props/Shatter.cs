using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
//handles "breaking" a breakable object, as wella s exploding explodable objects. upon being called, it will delete the original object, then spawn a prefab of debris with a force added to it, giving 
// the effect of a shatter. if its explosive, this force also effects the environment as well as the shards. after that, the shards despawn after a set amount of time 
//Travis Parks
public class Shatter : MonoBehaviour
{

    bool boomBlocked;
    public GameObject explosionEffect;
    public GameObject[] shatterPrefab;
    [Tooltip("What shattered mesh spawns")]
    public GameObject shatterSpawnPos;
    [Tooltip("Where the shattered mesh spawns")]
    Renderer color;
    float darken = 1f;
    [SerializeField]
    [Tooltip("How many hits a prop will take before breaking")]
    float hitPoints = 2;
    int Damagestate = 0;
    [SerializeField]
    float breakSpeed = 40f;
    GameObject player;
    GameObject sounds;
    [SerializeField]
    bool throwableBreak;
    [SerializeField]
	public bool bombBreak;
	[SerializeField]
	bool hasEvent;
	public string breakSoundEvent;
	public bool destroyRoot;
	bool broken;
	public void setBoomBlocked(bool plug){
		
        boomBlocked = plug;
    }
    void OnCollisionEnter(Collision other) {
        // does this object have a ridigbody? is the object colliding with another object past the breaking speed? if so, break it. dont let that object be a player. that colliding objects mass must be greater than or equal to the current obejcts mass
        //consider doing better calculations here, ie dot product of collision normal and collision velocity(relative velocity of both bodies) times the mass of the other collider
        if(throwableBreak){
            if((other.gameObject.GetComponent<Rigidbody>() != null && (other.gameObject.GetComponent<Rigidbody>().velocity.magnitude > breakSpeed) || (this.gameObject.GetComponent<Rigidbody>().velocity.magnitude > breakSpeed )) ){
                oneShot(0);
                //if (other.gameObject.tag == "Explosive") {
                //  bombAudioSource.Play(); }

            }
        }
    }

    void Start() {

        color = GetComponent<Renderer>();
    }
    public void oneShot(float time){
        AudioManager.instance.PlayOneShot(FMODEvents.instance.objectDestorySound, this.transform.position);
        if (!boomBlocked){
	        Invoke("spawnShatter", time);

        }
    }
    public void takeDamage(){
        if (Damagestate < hitPoints){
            AudioManager.instance.PlayOneShot(FMODEvents.instance.objectBreakingSound, this.transform.position);

            foreach (Material m in color.materials ){
                //m.SetColor("_EmissionColor", Color.grey * darken);
                m.SetColor("_Color", Color.grey * darken);
                Damagestate++;
                darken -= .2f;
            }
        }
        else if ( Damagestate >= hitPoints){
	        Invoke("spawnShatter", 0);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.objectDestorySound, this.transform.position);

        }
    }
	void spawnShatter(){
		if(!broken){
			broken = true;
			if(hasEvent){
				if(this.gameObject.GetComponent<Console>()!= null){
					this.gameObject.GetComponent<Console>().Interact();
				}
			}
	        GameObject realShatter;
	        realShatter = shatterPrefab[Random.Range(0, shatterPrefab.Length-1)];
	        Instantiate(realShatter, shatterSpawnPos.transform.position, shatterSpawnPos.transform.rotation);
	        if(explosionEffect != null){
	            Instantiate(explosionEffect, transform.position, transform.rotation);
	
	
	        }
	        
			
			if(destroyRoot){
				Destroy(this.transform.parent.gameObject);
			}
			else{
				Destroy(this.gameObject);
			}
		}
		
    }
}
