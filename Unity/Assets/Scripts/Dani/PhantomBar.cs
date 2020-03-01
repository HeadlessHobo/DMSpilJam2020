using System.Collections;
using System.Collections.Generic;
using Gameplay.Player;
using UnityEngine;
using UnityEngine.UI;

public class PhantomBar : MonoBehaviour
{
	public Image phantomEnergyImage;
	public GameObject BarRootGo;

    void Update()
    {
	    PlatformPlayerPhantom platformPlayerPhantom = MyGameManager.Instance.PlatformPlayer.PlatformPlayerPhantom;
	    if (platformPlayerPhantom != null)
	    {
		    BarRootGo.SetActive(platformPlayerPhantom.IsPhantomModeEnabled);
		    phantomEnergyImage.fillAmount = platformPlayerPhantom.CurrentFillProcent;
		/*	
			if(platformPlayerPhantom.IsPhantomModeActive)
		{
			
		}
		else
		{
			
			
		}
		*/
	    }
		
		
    }
}
