using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CancelSettingsPrompt : ConfirmationPrompt 
{
	new private void Start()
	{
		OptionsManager.Instance.mouseTrigger.GetComponent<OptionCursor>().enabled = false;

		//Set the appearance of UI prompt to settings that were previously set in the GameManager
		textBorder.sprite = GameManager.P1.TextBorder;
		textBox.sprite = GameManager.P1.TextBox;
		textBoxGradient.sprite = GameManager.P1.TextBoxGradient;
		textBorder.color = GameManager.P1.TextBorderColor;
		textBox.color = GameManager.P1.TextBoxColor;
		textBoxGradient.color = new Color(GameManager.P1.GradientColor.r, GameManager.P1.GradientColor.g, GameManager.P1.GradientColor.b, GameManager.P1.GradientTransparency);

		promptText.font = GameManager.P1.TextFont;
		promptText.color = GameManager.P1.TextColor;
		promptText.GetComponent<Shadow>().effectColor = new Color(GameManager.P1.TextShadowColor.r, GameManager.P1.TextShadowColor.g, GameManager.P1.TextShadowColor.b, GameManager.P1.TextShadowTransparency);
		yesText.font = GameManager.P1.TextFont;
		yesText.color = GameManager.P1.TextColor;
		yesText.GetComponent<Shadow>().effectColor = new Color(GameManager.P1.TextShadowColor.r, GameManager.P1.TextShadowColor.g, GameManager.P1.TextShadowColor.b, GameManager.P1.TextShadowTransparency);
		noText.font = GameManager.P1.TextFont;
		noText.color = new Color(GameManager.P1.TextColor.r / 2, GameManager.P1.TextColor.g / 2, GameManager.P1.TextColor.b / 2, 1f);
		noText.GetComponent<Shadow>().effectColor = new Color(GameManager.P1.TextShadowColor.r, GameManager.P1.TextShadowColor.g, GameManager.P1.TextShadowColor.b, GameManager.P1.TextShadowTransparency);
		switch(GameManager.P1.TextFont.name)
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
		switch(GameManager.P1.TextBorder.name)
		{
		case "TextBorders_0":
			textBox.rectTransform.anchoredPosition = new Vector2(1f, textBox.rectTransform.anchoredPosition.y);
			textBoxGradient.rectTransform.anchoredPosition = new Vector2(-1f, -1f);
			break;
		case "TextBorders_1":
			textBox.rectTransform.anchoredPosition = new Vector2(1f, textBox.rectTransform.anchoredPosition.y);
			textBoxGradient.rectTransform.anchoredPosition = new Vector2(0f, 0f);
			break;
		case "TextBorders_2":
			textBox.rectTransform.anchoredPosition = new Vector2(2f, textBox.rectTransform.anchoredPosition.y);
			textBoxGradient.rectTransform.anchoredPosition = new Vector2(-1f, 0f);
			break;
		case "TextBorders_3":
			textBox.rectTransform.anchoredPosition = new Vector2(2f, textBox.rectTransform.anchoredPosition.y);
			textBoxGradient.rectTransform.anchoredPosition = new Vector2(-1f, 0f);
			break;
		case "TextBorders_4":
			textBox.rectTransform.anchoredPosition = new Vector2(1f, textBox.rectTransform.anchoredPosition.y);
			textBoxGradient.rectTransform.anchoredPosition = new Vector2(0f, 0f);
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
			yesText.color = GameManager.P1.TextColor;
			noText.color = new Color(GameManager.P1.TextColor.r / 2, GameManager.P1.TextColor.g / 2, GameManager.P1.TextColor.b / 2, 1f);
			break;
		case false:
			yesText.color = new Color(GameManager.P1.TextColor.r / 2, GameManager.P1.TextColor.g / 2, GameManager.P1.TextColor.b / 2, 1f); 
			noText.color = GameManager.P1.TextColor;
			break;
		}
	}
	public override void Select (bool confirm)
	{
		base.Select (confirm);
		OptionsManager.Instance.CancelChangesToSettings();

	}
	private void Initialize(Player player)
	{

	}
}