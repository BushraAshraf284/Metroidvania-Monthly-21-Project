using UnityEngine;
using System.Collections;
 
public class RandomOscilation : MonoBehaviour
{
	[SerializeField]
	Movement move;
	[SerializeField]
	AnimationStateController cont;
	Transform startingPos;
	int randomX;
	int randomY;
	int randomZ;
	float timerCount = 0f;
	[SerializeField]
	float timerCap = .5f;
	int range;
	[SerializeField]
	int movingRange;
	[SerializeField]
	int dashingRange;
	[SerializeField]
	int startingRange;
	bool flipFlop;
	void Start(){
		startingPos = this.transform;
		
	}
	void Update(){
		
		if(cont.movementPressed){
			range = movingRange;
		}
		else{
			range = startingRange;
		}
		if(move.delayedIsDashing){
			range = dashingRange;
		}
		else{
			range = startingRange;
		}
		
		if(timerCount < timerCap){
			timerCount += Time.deltaTime;
		}
		else{
			timerCount = 0f;
			if(flipFlop){
				randomX = (Random.Range(-range,range));
				randomY = (Random.Range(-range,range));
				randomZ = (Random.Range(-range,range));
				this.transform.position = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + randomZ);
				flipFlop = !flipFlop;
			}
			else{
				this.transform.position = this.transform.parent.transform.position;
				flipFlop = !flipFlop;
			}
		}
		

		
	}
}
