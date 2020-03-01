using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
	public Animator PlayerAnimator;

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
	
}
