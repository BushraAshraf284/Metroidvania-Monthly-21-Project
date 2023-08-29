using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
	[field: Header("Player SFX")]
	[field: SerializeField] public EventReference playerFootsteps { get; private set; }
	[field: SerializeField] public EventReference playerJump { get; private set; }
	[field: SerializeField] public EventReference playerDash { get; private set; }

	[field: Header("Collectable SFX")]
	[field: SerializeField] public EventReference upgradeCollectedSound { get; private set; }

	[field: Header("NPC SFX")]
	[field: SerializeField] public EventReference npcSFX { get; private set; }

	public static FMODEvents instance { get; private set; }

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("Found more than one FMOD Events instance in the scene.");
		}

		instance = this;
	}

}
