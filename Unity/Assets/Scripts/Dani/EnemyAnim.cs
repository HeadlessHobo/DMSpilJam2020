using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
	
	public Animator EnemyAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
		{
			IdleBlink();
		}
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
