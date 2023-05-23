using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class DefaultSettingsPrompt : ConfirmationPrompt 
{ 	
	new private void Start()
	{
		base.Start();
    }
    new private void Update()
	{
		base.Update();
	}
	new private void OnDestroy()
	{
		base.OnDestroy();
	}

	public override void Toggle(bool confirm)
	{
		base.Toggle(confirm);
		switch(confirm)
		{
		case true:
			yesText.color = new Color (1f, 1f, 1f);
			noText.color = new Color (0.5f, 0.5f, 0.5f);
			break;
		case false:
			yesText.color = new Color (0.5f, 0.5f, 0.5f);
			noText.color = new Color (1f, 1f, 1f);
			break;
		}
	}
	public override void Select (bool confirm)
	{
		OptionsManager.Instance.SetAllDefaultSettings();
		base.Select (confirm);
	}
}