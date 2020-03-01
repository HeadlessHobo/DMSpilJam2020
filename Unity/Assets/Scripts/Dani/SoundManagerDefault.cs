using System.Collections;
using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;

public class SoundManagerDefault : Singleton<SoundManagerDefault>
{

	public AudioClip JumpSound;
	public AudioSource JumpSoundSource;
	
	public AudioClip PlayerLandSound;
	public AudioSource PlayerLandSoundSource;

	public AudioClip MonsterDeathSound;
	public AudioSource MonsterDeathSoundSource;
	
	public AudioClip MonsterShootSound;
	public AudioSource MonsterShootSoundSource;
	
	public AudioClip PortalSound;
	public AudioSource PortalSoundSource;

	public AudioClip ProjectileHitSound;
	public AudioSource ProjectileHitSoundSource;
	
	public AudioClip PlayerDeathSound;
	public AudioSource PlayerDeathSoundSource;
	
	public AudioClip EnterPhantSound;
	public AudioSource EnterPhantSoundSource;
	
	public AudioClip ExitPhantSound;
	public AudioSource ExitPhantSoundSource;
	
	public AudioClip BlocksExplodeSound;
	public AudioSource BlocksExplodeSoundSource;

	public void PlayJumpSound()
	{
		JumpSoundSource.clip = JumpSound;
		JumpSoundSource.Play();
	}
	
	public void PlayLandingSound()
	{
		PlayerLandSoundSource.clip = PlayerLandSound;
		PlayerLandSoundSource.Play();
	}
	
	public void PlayPortalSound()
	{
		PortalSoundSource.clip = PortalSound;
		PortalSoundSource.Play();
	}
	
	public void PlayMonsterDeathSound()
	{
		MonsterDeathSoundSource.clip = MonsterDeathSound;
		MonsterDeathSoundSource.Play();
	}
	
	public void PlayMonsterShootSound()
	{
		MonsterShootSoundSource.clip = MonsterShootSound;
		MonsterShootSoundSource.Play();
	}
	
	public void PlayPlayerDeathSound()
	{
		PlayerDeathSoundSource.clip = PlayerDeathSound;
		PlayerDeathSoundSource.Play();
	}
	
	public void PlayEntPhantSound()
	{
		EnterPhantSoundSource.clip = EnterPhantSound;
		EnterPhantSoundSource.Play();
	}
	
	public void PlayExitPhantSound()
	{
		ExitPhantSoundSource.clip = ExitPhantSound;
		ExitPhantSoundSource.Play();
	}
	
	public void PlayProjectileHitSound()
	{
		ProjectileHitSoundSource.clip = ProjectileHitSound;
		ProjectileHitSoundSource.Play();
	}	
	
	public void PlayBlocksExplodeSound()
	{
		BlocksExplodeSoundSource.clip = BlocksExplodeSound;
		BlocksExplodeSoundSource.Play();
	}
	
}
