using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
	[field: Header("Ambience")]
	[field: SerializeField] public EventReference windAudio { get; private set; }

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
	[field: SerializeField] public EventReference playerVerticalBoost { get; private set; }
	[field: SerializeField] public EventReference playerShootMissle { get; private set; }
	[field: SerializeField] public EventReference playerShockProngStart { get; private set; }
	[field: SerializeField] public EventReference playerShockProngMid { get; private set; }
	[field: SerializeField] public EventReference playerShockProngEnd { get; private set; }
	[field: SerializeField] public EventReference playerSwordAttack { get; private set; }
	[field: SerializeField] public EventReference playerSwordAttackTwo { get; private set; }
	[field: SerializeField] public EventReference playerSwordAttackThree { get; private set; }

	[field: Header("Player Animation SFX - Flavor")]
	[field: SerializeField] public EventReference playerServoSmall { get; private set; }
	[field: SerializeField] public EventReference playerServoMedium { get; private set; }
	[field: SerializeField] public EventReference playerServoHeavy { get; private set; }
	[field: SerializeField] public EventReference playerServoCranking { get; private set; }

	[field: Header("Player SFX")]
	[field: SerializeField] public EventReference playerDeath { get; private set; }
	[field: SerializeField] public EventReference playerHeal { get; private set; }
	[field: SerializeField] public EventReference playerHurt { get; private set; }
	[field: SerializeField] public EventReference playerBatteryCharge { get; private set; }
	// [field: SerializeField] public EventReference playerBatteryDrain { get; private set; }

	[field: Header("Collectable SFX")]
	[field: SerializeField] public EventReference upgradeCollectedSound { get; private set; }
	[field: SerializeField] public EventReference batteryEmitterSound { get; private set; }

	[field: Header("NPC SFX")]
	[field: SerializeField] public EventReference npcSFX { get; private set; }
	[field: SerializeField] public EventReference npcPreChatSFX { get; private set; }
	[field: SerializeField] public EventReference npcRepairFailSFX { get; private set; }
	[field: SerializeField] public EventReference npcRepairSuccessSFX { get; private set; }
	[field: SerializeField] public EventReference turrentShoot { get; private set; }
	// [field: SerializeField] public EventReference turrentRotationSound { get; private set; }

	[field: Header("World SFX")]
	[field: SerializeField] public EventReference doorSFX { get; private set; }
	[field: SerializeField] public EventReference objectBreakingSound { get; private set; }
	[field: SerializeField] public EventReference objectDestorySound { get; private set; }
	[field: SerializeField] public EventReference gravitySwitchSound { get; private set; }

	[field: Header("UI SFX")]
	[field: SerializeField] public EventReference interactSFX { get; private set; }
	[field: SerializeField] public EventReference pauseGameAudio { get; private set; }
	[field: SerializeField] public EventReference resumeGameAudio { get; private set; }


	[field: Header("Testing Audio")]
	[field: SerializeField] public EventReference testOneShot { get; private set; }
	[field: SerializeField] public EventReference testOneShotWorld { get; private set; }
	[field: SerializeField] public EventReference testLooping { get; private set; }
	[field: SerializeField] public EventReference testLoopingWorld { get; private set; }

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
