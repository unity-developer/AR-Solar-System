using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using Vuforia;

public class Flash : MonoBehaviour {

    //	public ScriptableObject script;

    public GameObject FlashBtn;
    public Sprite[] FlashSprites; 
	public bool flashOn = false;

	
	void Start()
	{}

	public void flashLight()
	{
		flashOn = !flashOn;
		if (flashOn) 
		{
			CameraDevice.Instance.SetFlashTorchMode(true);
            FlashBtn.GetComponent<UnityEngine.UI.Image>().sprite = FlashSprites[0];
            //flashlight.GetComponent<Light>().enabled = true;
        }
		else if (!flashOn) 
		{
			CameraDevice.Instance.SetFlashTorchMode(false);
            FlashBtn.GetComponent<UnityEngine.UI.Image>().sprite = FlashSprites[1];

            //flashlight.GetComponent<Light>().enabled = false;
        }
    }


	void Update()
	{
		

	}
}
