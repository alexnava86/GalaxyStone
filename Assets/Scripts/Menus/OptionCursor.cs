using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class OptionCursor : MonoBehaviour 
{
	public static OptionCursor Instance{get; private set;}

	private void Awake()
	{
		if(!Instance)
		{
			Instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
	private void OnTriggerStay2D(Collider2D collider)
	{
		switch(OptionsManager.optionSelector)
		{
		case 0: // Text Border Style
			if(Input.GetMouseButtonDown(0) && OptionsManager.style != 0)
			{
				OptionsManager.Instance.SelectOption(0);
			}
			break;
		case 6: // Font Style
			if(Input.GetMouseButtonDown(0) && OptionsManager.style != 0)
			{
				OptionsManager.Instance.SelectOption(6);
            }
            break;
        case 12: // Difficulty
			if(Input.GetMouseButtonDown(0) && OptionsManager.style != 0)
			{
				OptionsManager.Instance.SelectOption(12);
            }
            break;
        case 13:
			if(Input.GetMouseButtonDown(0))
			{
				OptionsManager.Instance.SelectOption(13);
			}
            break;
        case 14:
			if(Input.GetMouseButtonDown(0))
			{
				OptionsManager.Instance.SelectOption(14);
			}
            break;
		case 15:
			if(Input.GetMouseButtonDown(0))
			{
				OptionsManager.Instance.SelectOption(15);
			}
			break;
        }
	}
    private void OnTriggerEnter2D(Collider2D collider)
	{
		//OptionSelector text switch
		switch(collider.gameObject.name)
		{
		case "TextBorderStyleText":
			OptionsManager.Instance.OptionToggle(0);
			break;
		case "TextBorderColorText":
			OptionsManager.Instance.OptionToggle(1);
			break;
		case "TextBoxColorText":
			OptionsManager.Instance.OptionToggle(2);
			break;
		case "TextBoxTransparencyText":
			OptionsManager.Instance.OptionToggle(3);
			break;
		case "GradientTransparencyText":
			OptionsManager.Instance.OptionToggle(4);
			break;
		case "GradientColorText":
			OptionsManager.Instance.OptionToggle(5);
			break;
		case "FontStyleText":
			OptionsManager.Instance.OptionToggle(6);
			break;
		case "TextColorText":
			OptionsManager.Instance.OptionToggle(7);
			break;
		case "TextShadowColorText":
			OptionsManager.Instance.OptionToggle(8);
			break;
		case "TextShadowTransparencyText":
			OptionsManager.Instance.OptionToggle(9);
			break;
		case "MusicVolumeText":
			OptionsManager.Instance.OptionToggle(10);
			break;
		case "SfxVolumeText":
			OptionsManager.Instance.OptionToggle(11);
			break;
		case "DifficultyText":
			OptionsManager.Instance.OptionToggle(12);
			break;
		case "DefaultText":
			OptionsManager.Instance.OptionToggle(13);
			break;
		case "ConfirmText":
			OptionsManager.Instance.OptionToggle(14);
			break;
		case "CancelText":
			OptionsManager.Instance.OptionToggle(15);
			break;
		}

		// RGB / Style Switch
		switch(OptionsManager.optionSelector)
		{
		case 0:
			switch(collider.gameObject.name)
			{
			case "1Text":
				OptionsManager.Instance.StyleToggle(1);
				break;
			case "2Text":
				OptionsManager.Instance.StyleToggle(2);
				break;
			case "3Text":
				OptionsManager.Instance.StyleToggle(3);
				break;
			case "4Text":
				OptionsManager.Instance.StyleToggle(4);
				break;
			case "5Text":
				OptionsManager.Instance.StyleToggle(5);
				break;
			}
			break;
		case 1:
			switch(collider.gameObject.name)
			{
			case "TextBorderColorSliderR":
				OptionsManager.Instance.RGBToggle('r');
				break;
			case "TextBorderColorSliderG":
				OptionsManager.Instance.RGBToggle('g');
				break;
			case "TextBorderColorSliderB":
				OptionsManager.Instance.RGBToggle('b');
				break;
			}
			break;
		case 2:
			switch(collider.gameObject.name)
			{
			case "TextBoxColorSliderR":
				OptionsManager.Instance.RGBToggle('r');
				break;
			case "TextBoxColorSliderG":
				OptionsManager.Instance.RGBToggle('g');
                break;
            case "TextBoxColorSliderB":
                OptionsManager.Instance.RGBToggle('b');
                break;
            }
			break;
		case 3:
			if(collider.gameObject.name == "TextBoxTransparencySlider")
			{
				OptionsManager.Instance.SelectOption(3);
			}
			break;
		case 4:
			if(collider.gameObject.name == "GradientTransparencySlider")
			{
				OptionsManager.Instance.SelectOption(4);
			}
			break;
		case 5:
			switch(collider.gameObject.name)
			{
			case "GradientColorSliderR":
				OptionsManager.Instance.RGBToggle('r');
				break;
			case "GradientColorSliderG":
				OptionsManager.Instance.RGBToggle('g');
				break;
			case "GradientColorSliderB":
				OptionsManager.Instance.RGBToggle('b');
				break;
			}
			break;
		case 6:
			switch(collider.gameObject.name)
			{
			case "1Text":
				OptionsManager.Instance.StyleToggle(1);
				break;
			case "2Text":
				OptionsManager.Instance.StyleToggle(2);
				break;
			case "3Text":
				OptionsManager.Instance.StyleToggle(3);
				break;
			case "4Text":
				OptionsManager.Instance.StyleToggle(4);
				break;
			case "5Text":
				OptionsManager.Instance.StyleToggle(5);
				break;
			}
			break;
		case 7:
			switch(collider.gameObject.name)
			{
			case "TextColorSliderR":
				OptionsManager.Instance.RGBToggle('r');
				break;
			case "TextColorSliderG":
				OptionsManager.Instance.RGBToggle('g');
				break;
			case "TextColorSliderB":
				OptionsManager.Instance.RGBToggle('b');
				break;
			}
			break;
		case 8:
			switch(collider.gameObject.name)
			{
			case "TextShadowColorSliderR":
				OptionsManager.Instance.RGBToggle('r');
				break;
			case "TextShadowColorSliderG":
				OptionsManager.Instance.RGBToggle('g');
				break;
			case "TextShadowColorSliderB":
				OptionsManager.Instance.RGBToggle('b');
				break;
			}
			break;
		case 9:
			if(collider.gameObject.name == "TextShadowTransparencySlider")
			{
				OptionsManager.Instance.SelectOption(9);
			}
			break;
		case 10:
			if(collider.gameObject.name == "MusicVolumeSlider")
			{
				OptionsManager.Instance.SelectOption(10);
			}
			break;
		case 11:
			if(collider.gameObject.name == "SfxVolumeSlider")
			{
				OptionsManager.Instance.SelectOption(11);
			}
			break;
		case 12: // Difficulty
			switch(collider.gameObject.name)
			{
			case "1Text":
				OptionsManager.Instance.StyleToggle(1);
				break;
			case "2Text":
				OptionsManager.Instance.StyleToggle(2);
				break;
			case "3Text":
				OptionsManager.Instance.StyleToggle(3);
				break;
			case "4Text":
				OptionsManager.Instance.StyleToggle(4);
				break;
			case "5Text":
				OptionsManager.Instance.StyleToggle(5);
				break;
			}
			break;
		case 13: //Default
			break;
		case 14: //Save
			break;
		case 15: //Cancel
			break;
		}
    }
    private void Update()
	{
		this.transform.position = Input.mousePosition;
	}
}
