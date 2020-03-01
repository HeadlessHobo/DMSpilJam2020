using System.Collections;
using System.Collections.Generic;
using Gameplay.Player;
using UnityEngine;
using UnityEngine.UI;

public class PhantomBar : MonoBehaviour
{
	public Image phantomEnergyImage;
	public GameObject BarRootGo;
	
	public Image PhantomOverlay;
	
	
	

    void Update()
    {
	    PlatformPlayerPhantom platformPlayerPhantom = MyGameManager.Instance.PlatformPlayer.PlatformPlayerPhantom;
	    if (platformPlayerPhantom != null)
	    {
		    BarRootGo.SetActive(platformPlayerPhantom.IsPhantomModeEnabled);
		    phantomEnergyImage.fillAmount = platformPlayerPhantom.CurrentFillProcent;
			
			
			
			if(platformPlayerPhantom.IsPhantomModeActive)
		{
						
		PhantomOverlay.GetComponent<Image>().color = Color.green;
		
		Color Temp = PhantomOverlay.GetComponent<Image>().color;
			Temp.a = 0.5f;
			PhantomOverlay.GetComponent<Image>().color = Temp;
		
		}
		else
		{
			PhantomOverlay.GetComponent<Image>().color = Color.white;
			Color Temp = PhantomOverlay.GetComponent<Image>().color;
			Temp.a = 1f;
			PhantomOverlay.GetComponent<Image>().color = Temp;
			
		}
		//Debug.Log("sdkfjlsdfkj");
	    }
		
		
    }
}
