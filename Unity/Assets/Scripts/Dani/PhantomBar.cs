using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhantomBar : MonoBehaviour
{
	public Image phantomEnergyImage;

    void Update()
    {
	    phantomEnergyImage.fillAmount = GameManager.Instance.PlatformPlayer.PlatformPlayerPhantom.CurrentFillProcent;
    }
}
