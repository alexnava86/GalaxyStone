using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialPrompt : MonoBehaviour 
{
	public Text[] yesNoText;
	private bool tutorial = true; 
	private int selection = 0;

	private void Update()
	{
		if(Input.GetAxisRaw("Horizontal") != 0 && Input.GetButtonDown("Horizontal"))
		{
			selection += (int)Input.GetAxisRaw("Horizontal");
			selection = Mathf.Clamp(selection, 0, 1);
		}
	}
	private void OnDestroy()
	{
		if(tutorial != false)
		{
			GameManager.TutorialMode = true;
		}
		else
		{
			GameManager.TutorialMode = false;
		}
	}
}
