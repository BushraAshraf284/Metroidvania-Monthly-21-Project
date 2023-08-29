using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
	[field: Header("Player Animation SFX")]
	[field: SerializeField] public EventReference playerFootsteps { get; private set; }
	[field: SerializeField] public EventReference playerJump { get; private set; }
	[field: SerializeField] public EventReference playerJumpLand { get; private set; }
	[field: SerializeField] public EventReference playerFalling { get; private set; }
	[field: SerializeField] public EventReference playerDash { get; private set; }
	[field: SerializeField] public EventReference playerDashStart { get; private set; }
	[field: SerializeField] public EventReference playerDashStop { get; private set; }
	[field: SerializeField] public EventReference playerDashJump { get; private set; }
	[field: SerializeField] public EventReference playerJogJumpStart { get; private set; }
	[field: SerializeField] public EventReference playerJogJumpStop { get; private set; }
	[field: SerializeField] public EventReference playerJogJumpLand { get; private set; }
	[field: SerializeField] public EventReference playerShootMissle { get; private set; }
	
	[field: Header("Player SFX")]
	[field: SerializeField] public EventReference playerDeath { get; private set; }
	[field: SerializeField] public EventReference playerHeal { get; private set; }
	[field: SerializeField] public EventReference playerHurt { get; private set; }
	[field: SerializeField] public EventReference playerBatteryCharge { get; private set; }

	[field: Header("Collectable SFX")]
	[field: SerializeField] public EventReference upgradeCollectedSound { get; private set; }

	[field: Header("NPC SFX")]
	[field: SerializeField] public EventReference npcSFX { get; private set; }

	[field: Header("Platform SFX")]
	[field: SerializeField] public EventReference doorSFX { get; private set; }

	[field: Header("Interact SFX")]
	[field: SerializeField] public EventReference interactSFX { get; private set; }

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
