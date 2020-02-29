using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
	public Animator EnemyAnimator;

	private void Awake()
	{
		AnimEnIdle();
	}

	public void AnimShoot()
	{
		EnemyAnimator.Play("ShootPlayer");
	}
	
	public void AnimEnDeath()
	{
		
		EnemyAnimator.Play("EnemyDeath");
	}
	
	public void AnimEnIdle()
	{
		EnemyAnimator.Play("IdleEnemey");
	}
	
	public void IdleBlink()
	{
		EnemyAnimator.Play("IdleBlink");
	}
}
