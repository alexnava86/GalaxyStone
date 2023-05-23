using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveSettingsPrompt : ConfirmationPrompt 
{
	new private void Start()
	{
		base.Start();
		//Set the appearance of the UI prompt to the settings in the OptionManager
		textBorder.sprite = OptionsManager.Instance.textBorders[0].sprite;
		textBox.sprite = OptionsManager.Instance.textBoxes[0].sprite;
		textBoxGradient.sprite = OptionsManager.Instance.gradients[0].sprite;
		textBorder.color = OptionsManager.TextBorderColor;
		textBox.color = OptionsManager.TextBoxColor;
		textBoxGradient.color = new Color(OptionsManager.GradientColor.r, OptionsManager.GradientColor.g, OptionsManager.GradientColor.b, OptionsManager.Instance.gradientTransparencySlider.value);
		
		promptText.font = OptionsManager.TextFont;
		promptText.color = OptionsManager.TextColor;
		promptText.GetComponent<Shadow>().effectColor = new Color(OptionsManager.TextShadowColor.r, OptionsManager.TextShadowColor.g, OptionsManager.TextShadowColor.b, OptionsManager.Instance.textShadowTransparencySlider.value);
		yesText.font = OptionsManager.TextFont;
		yesText.color = OptionsManager.TextColor;
		yesText.GetComponent<Shadow>().effectColor = new Color(OptionsManager.TextShadowColor.r, OptionsManager.TextShadowColor.g, OptionsManager.TextShadowColor.b, OptionsManager.Instance.textShadowTransparencySlider.value);
		noText.font = OptionsManager.TextFont;
		noText.color = new Color(OptionsManager.TextColor.r / 2, OptionsManager.TextColor.g / 2, OptionsManager.TextColor.b / 2, 1f);
		noText.GetComponent<Shadow>().effectColor = new Color(OptionsManager.TextShadowColor.r, OptionsManager.TextShadowColor.g, OptionsManager.TextShadowColor.b, OptionsManager.Instance.textShadowTransparencySlider.value);
		switch(OptionsManager.TextFont.name)
		{
		case "Masaaki-Regular":
			promptText.fontSize = 20;
			yesText.fontSize = 20;
			noText.fontSize = 20;
			TextOffset = 0;
			break;
		case "PressStart2P":
			promptText.fontSize = 11;
			yesText.fontSize = 11;
            noText.fontSize = 11;
			TextOffset = -12;
			break;
		case "BMarmy":
			promptText.fontSize = 14;
			yesText.fontSize = 14;
            noText.fontSize = 14;
			TextOffset = -10;
			break;
		case "celtic-bit":
			promptText.fontSize = 10;
			yesText.fontSize = 10;
            noText.fontSize = 10;
			TextOffset = -18;
			break;
		case "alagard":
			promptText.fontSize = 16;
			yesText.fontSize = 16;
			noText.fontSize = 16;
			TextOffset = -10;
			break;
        }
		promptText.rectTransform.anchoredPosition = new Vector2(promptText.rectTransform.anchoredPosition.x, promptText.rectTransform.anchoredPosition.y + TextOffset);
		yesText.rectTransform.anchoredPosition = new Vector2(yesText.rectTransform.anchoredPosition.x, yesText.rectTransform.anchoredPosition.y + TextOffset);
		noText.rectTransform.anchoredPosition = new Vector2(noText.rectTransform.anchoredPosition.x, noText.rectTransform.anchoredPosition.y + TextOffset);
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
			yesText.color = OptionsManager.TextColor;
			noText.color = new Color(OptionsManager.TextColor.r / 2, OptionsManager.TextColor.g / 2, OptionsManager.TextColor.b / 2, 1f);
			break;
		case false:
			yesText.color = new Color(OptionsManager.TextColor.r / 2, OptionsManager.TextColor.g / 2, OptionsManager.TextColor.b / 2, 1f);
			noText.color = OptionsManager.TextColor;
			break;
		}
	}
	public override void Select (bool confirm)
	{
		base.Select (confirm);
		OptionsManager.Instance.SaveChangesToSettings();
	}
}