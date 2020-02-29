using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhantomBar : MonoBehaviour
{
	
	public float phantomEnergy = 100;
	public float phantomEnergyMax = 100;
	
	public Image phantomEnergyImage;
    // Start is called before the first frame update
    void Start()
    {
        phantomEnergy = 100;
		phantomEnergyMax = 100;
    }

    // Update is called once per frame
    void Update()
    {
		//DEBUG
		phantomEnergyImage.fillAmount = phantomEnergy / phantomEnergyMax;
        if(Input.GetKeyDown(KeyCode.A))
		{
			phantomEnergy -= 10;
		}
    }
}
