using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
	public Animator PlayerAnimator;
	
	public Animator GroundEffect;

	private static readonly int Speed = Animator.StringToHash("Speed");

	public void SetSpeed(float speed)
    {
	    PlayerAnimator.SetFloat(Speed, speed);
    }
	
	public void AnimPlayerJump()
	{
		PlayerAnimator.Play("PlayerJump");
	}
	
	public void AnimPlayerRun()
	{
		PlayerAnimator.Play("RunningAnimation");
	}
	
	public void AnimPlayerIdle()
	{
		PlayerAnimator.Play("PlayerIdle");
	}
	
	public void GroundedEffect()
	{
		GroundEffect.Play("GroundedAnimation");
	}
	
}
