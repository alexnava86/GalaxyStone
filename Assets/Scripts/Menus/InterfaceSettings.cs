using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InterfaceSettings : MonoBehaviour 
{
	#region Variables
	public Image textBorder;
	public Image textBox;
	public Image gradient;
	public Text[] text;
	private int fontSize;
	private int textOffset = 0;
	#endregion

	#region MonoBehaviour
	private void Start()
	{
		Initialize(GameManager.P1); //Change if UI is player specific
	}

	#endregion
	private void Initialize(Player player)
	{
		switch(player.TextFont.name)
		{
		case "Masaaki-Regular":
			fontSize = 20;
			textOffset = 0;
			break;
		case "PressStart2P":
			fontSize = 11;
			textOffset = 12;
			break;
		case "BMarmy":
			fontSize = 14;
			textOffset = 10;
			break;
		case "celtic-bit":
			fontSize = 10;
			textOffset = 18;
			break;
		case "alagard":
			fontSize = 16;
			textOffset = 10;
			break;
		}
		switch(player.TextBorder.name)
		{
		case "TextBorders_0":
			textBox.rectTransform.anchoredPosition = new Vector2(1f, textBox.rectTransform.anchoredPosition.y);
			gradient.rectTransform.anchoredPosition = new Vector2(-1f, -1f);
			break;
		case "TextBorders_1":
			textBox.rectTransform.anchoredPosition = new Vector2(0f, textBox.rectTransform.anchoredPosition.y);
			gradient.rectTransform.anchoredPosition = new Vector2(0f, 0f);
			break;
		case "TextBorders_2":
			textBox.rectTransform.anchoredPosition = new Vector2(1f, textBox.rectTransform.anchoredPosition.y);
			gradient.rectTransform.anchoredPosition = new Vector2(-1f, 0f);
			break;
		case "TextBorders_3":
			textBox.rectTransform.anchoredPosition = new Vector2(1f, textBox.rectTransform.anchoredPosition.y);
			gradient.rectTransform.anchoredPosition = new Vector2(-1f, 0f);
			break;
		case "TextBorders_4":
			textBox.rectTransform.anchoredPosition = new Vector2(0f, textBox.rectTransform.anchoredPosition.y);
			gradient.rectTransform.anchoredPosition = new Vector2(0f, 0f);
			break;
		}
		
		foreach(Text texts in text)
		{
			texts.fontSize = fontSize;
			texts.font = player.TextFont;
			texts.color = player.TextColor;
			texts.GetComponent<Shadow>().effectColor = new Color(player.TextShadowColor.r, player.TextShadowColor.g, player.TextShadowColor.b, player.TextShadowTransparency);
			texts.rectTransform.anchoredPosition = new Vector2(texts.rectTransform.anchoredPosition.x, texts.rectTransform.anchoredPosition.y - textOffset);
		}
		
		textBorder.sprite = player.TextBorder;
		textBox.sprite = player.TextBox;
		gradient.sprite = player.TextBoxGradient;
		textBorder.color = player.TextBorderColor;
		textBox.color = new Color (player.TextBoxColor.r, player.TextBoxColor.g, player.TextBoxColor.b, player.TextBoxTransparency);
		gradient.color = new Color (player.GradientColor.r, player.GradientColor.g, player.GradientColor.b, player.GradientTransparency);
		//other stuff... only if UI settings can alter based on current player/ enemy 
	}
}
