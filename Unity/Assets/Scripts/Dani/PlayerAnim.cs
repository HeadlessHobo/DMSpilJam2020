using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
	
	
	public Animator PlayerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.A))
		{
			Debug.Log("Nej");
			AnimPlayerJump();
		} 
	 if(Input.GetKeyDown(KeyCode.S))
		{
			Debug.Log("Nej");
			AnimPlayerRun();
		}	
    }
	
	public void AnimPlayerJump()
	{
		PlayerAnimator.Play("PlayerJump");
	}
	
	public void AnimPlayerRun()
	{
		PlayerAnimator.Play("RunningAnimation");
	}
	
}
