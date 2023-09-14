using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroy : MonoBehaviour
{
	protected void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}
}
