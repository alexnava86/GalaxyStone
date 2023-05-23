using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections;
//using System.Collections.Generic;

public class NameEntryMenu : MonoBehaviour
{
	#region Variables
	public AudioClip selectSound;
	public AudioClip cancelSound;
	public AudioClip invalidSound;
	public AudioClip cursorToggleSound;
	public AudioClip song;
	public Image screen;
	public Image cursor;

	public Text nameText;
	public Text defaultText;
	public Text confirmText;
	public Text cancelText;
	public Text [] letter;
	private int row = 0;  //Max = 10 /11 is default, confirm, cancel
	private int column = 0; //Max = 9
	private int letterSelection = 0;
	private AbstractCharacter character;
	private bool executing = false;
	private AudioSource audioSource;
	private AudioSource songSource;
	private StringBuilder s;
	#endregion

	#region MonoBehaviour
	private void Awake ()
	{
		letter [letterSelection].color = GameManager.P1.TextColor;
	}
	private void Start ()
	{
		audioSource = this.gameObject.AddComponent<AudioSource> ();
		switch (Application.loadedLevelName)
		{
		case "HornedLizardCreation":
			StartCoroutine (FadeIn ());
			character = Instantiate (Resources.Load ("Prefabs/Characters/Player/HornedLizard", typeof (HornedLizard)) as HornedLizard);
			character.transform.position = new Vector2 (-250f, 50f);
			nameText.text = character.defaultNames [0];
			songSource = this.gameObject.AddComponent<AudioSource> ();
			songSource.clip = song;
			songSource.loop = true;
			songSource.Play ();
			break;
		case "CrayslugCreation":
			break;
		default:
			//character = CharacterCreationMenu.Instance.
			break;
		}
		for (int i = 0; i < letter.Length; i++)
		{
			if (i != letterSelection)
			{
				letter [i].color = new Color (GameManager.P1.TextColor.r / 2, GameManager.P1.TextColor.g / 2, GameManager.P1.TextColor.b / 2, 1f);
			}
		}
	}
	private void Update ()
	{
		if (Input.GetButtonDown ("Horizontal") && Input.GetAxisRaw ("Horizontal") != 0 && executing == false)
		{
			StartCoroutine (MoveCursorNew ("Horizontal"));
		}
		if (Input.GetButtonDown ("Vertical") && Input.GetAxisRaw ("Vertical") != 0 && executing == false)
		{
			StartCoroutine (MoveCursorNew ("Vertical"));
		}
		if (Input.GetButtonDown ("Confirm"))
		{
			ConfirmLetter (letterSelection);
		}
		if (Input.GetButtonDown ("Cancel"))
		{
			DeleteLetter (letterSelection);
		}
	}
	#endregion

	#region Methods
	public void HighlightLetter (int selection)
	{
		letter [selection].color = new Color (GameManager.P1.TextColor.r, GameManager.P1.TextColor.g, GameManager.P1.TextColor.b, 1f);
		if (letterSelection < 72)
		{
			cursor.rectTransform.position = new Vector2 (letter [letterSelection].rectTransform.position.x - 125f, letter [letterSelection].rectTransform.position.y - 5f);
		}
		else
		{
			cursor.rectTransform.position = new Vector2 (letter [letterSelection].rectTransform.position.x - 95f, letter [letterSelection].rectTransform.position.y - 5f);
		}
	}
	public void ConfirmLetter (int selection)
	{
		audioSource.clip = selectSound;
		if (nameText.text.Length <= 12 && letterSelection < 72)
		{
			nameText.text = nameText.text + letter [selection].text;
			audioSource.Play ();
		}
		else if (letterSelection > 71)
		{
			switch (letterSelection)
			{
			case 72:
				break;
			case 73:
				ConfirmName ();
				break;
			case 74:
				break;
			default:
				audioSource.Play ();
				break;
			}
		}
	}
	private void DeleteLetter (int selection) // Recreate this method using String Builder?
	{
		char [] nameArray = nameText.text.ToCharArray ();
		char [] temp = new char [0];
		string tempString = "";

		if (nameArray.Length > 0)
		{
			temp = new char [nameArray.Length - 1];
			for (int i = 0; i < temp.Length; i++)
			{
				temp [i] = nameText.text [i];
				tempString = tempString + temp [i];
			}
			nameText.text = tempString;
			audioSource.clip = cancelSound;
			audioSource.Play ();
		}
		else
		{
			audioSource.clip = invalidSound;
			audioSource.Play ();
		}
	}
	private void ConfirmName ()
	{
		if (nameText.text.Length > 0)
		{
			if (Application.loadedLevelName == "HornedLizardCreation")
			{
				StartCoroutine (FadeOut ());
				character.CharName = nameText.text;
				GameManager.P1.AddCharacterToPlayerIndex (character);
				//Debug.Log (GameManager.P1.CharacterIndex.Count);
			}
			else if (Application.loadedLevelName == "CrayslugCreation")
			{

			}
			/*else if(Application.loadedLevelName == "CrayslugCreation")
			{

			}
			else if(Application.loadedLevelName == "CrayslugCreation")
			{
			
			}
			*/
			else // if not a main character
			{
				Instantiate (Resources.Load ("Prefabs/Interface/HSBToggle", typeof (GameObject)) as GameObject);
				Destroy (this.gameObject);
			}
		}
		else
		{
			audioSource.clip = invalidSound;
			audioSource.Play ();
		}
	}
	private void DefaultName ()
	{

	}
	private void CancelName ()
	{
		//if(AbstractCharacter == mainCharacter)
		{

		}
	}
	#endregion

	#region Coroutines
	private IEnumerator MoveCursorNew (string direction)
	{
		int increment = 0;
		executing = true;

		audioSource.clip = cursorToggleSound;
		letter [letterSelection].color = new Color (GameManager.P1.TextColor.r / 2, GameManager.P1.TextColor.g / 2, GameManager.P1.TextColor.b / 2, 1f);

		switch (direction)
		{
		case "Horizontal":
			increment = (int)Input.GetAxisRaw ("Horizontal");
			column += increment;
			if (column > 8)
			{
				column = column - 9;
			}
			else if (column < 0)
			{
				column = column + 9;
			}
			column = Mathf.Clamp (column, 0, 8);
			switch (row)
			{
			case 4:
				if (column == 2)
				{
					column = 6;
				}
				else if (column == 5)
				{
					column = 1;
				}
				if (column > 5)
				{
					letterSelection = 32 + column;
				}
				else
				{
					letterSelection = 36 + column;
				}
				break;
			case 5:
				letterSelection = 41 + column;
				break;
			case 6:
				if (column == 8)
				{
					column = 0;
				}
				letterSelection = 50 + column;
				break;
			case 7:
				if (column > 5)
				{
					column = 0;
				}
				letterSelection = 58 + column;
				break;
			case 8:
				if (column > 5)
				{
					column = 0;
				}
				letterSelection = 64 + column;
				break;
			case 9:
				if (column > 1)
				{
					column = 0;
				}
				letterSelection = 70 + column;
				break;
			case 10:
				column -= increment;
				letterSelection += increment;
				letterSelection = Mathf.Clamp (letterSelection, 72, 74);
				break;
			default:
				letterSelection = row * 9 + column;
				break;
			}
			letterSelection = Mathf.Clamp (letterSelection, 0, 74);
			HighlightLetter (letterSelection);
			audioSource.Play ();
			yield return new WaitForSeconds (0.2f);
			while (Input.GetAxisRaw ("Horizontal") < 0 || Input.GetAxisRaw ("Horizontal") > 0)
			{
				if (column == 8 && Input.GetAxis ("Horizontal") > 0 || column == 0 && Input.GetAxis ("Horizontal") < 0)
				{
					increment = 0;
					yield return new WaitForSeconds (0.075f);
				}
				else
				{
					letter [letterSelection].color = new Color (GameManager.P1.TextColor.r / 2, GameManager.P1.TextColor.g / 2, GameManager.P1.TextColor.b / 2, 1f);
					increment = (int)Input.GetAxisRaw ("Horizontal");
					column += increment;
					column = Mathf.Clamp (column, 0, 8);
					switch (row)
					{
					case 4:
						if (column == 2)
						{
							column = 6;
						}
						else if (column == 5)
						{
							column = 1;
						}
						if (column > 5)
						{
							letterSelection = 32 + column;
						}
						else
						{
							letterSelection = 36 + column;
						}
						break;
					case 5:
						letterSelection = 41 + column;
						break;
					case 6:
						if (column == 8)
						{
							column = 0;
						}
						letterSelection = 50 + column;
						break;
					case 7:
						if (column > 5)
						{
							column = 0;
						}
						letterSelection = 58 + column;
						break;
					case 8:
						if (column > 5)
						{
							column = 0;
						}
						letterSelection = 64 + column;
						break;
					case 9:
						if (column > 1)
						{
							column = 0;
						}
						letterSelection = 70 + column;
						break;
					case 10:
						column -= increment;
						letterSelection += increment;
						letterSelection = Mathf.Clamp (letterSelection, 72, 74);
						break;
					default:
						letterSelection = row * 9 + column;
						break;
					}
					audioSource.Play ();
				}
				HighlightLetter (letterSelection);
				yield return new WaitForSeconds (0.075f);
			}
			break;

		case "Vertical":
			increment = (int)Input.GetAxisRaw ("Vertical");
			row -= increment;
			if (row > 10)
			{
				row -= 11;
			}
			else if (row < 0)
			{
				row += 11;
			}
			row = Mathf.Clamp (row, 0, 10);
			switch (row)
			{
			case 4:
				if (column > 1 && column < 6)
				{
					if (Input.GetAxisRaw ("Vertical") > 0)
					{
						row = 3;
						letterSelection = row * 9 + column;
					}
					else if (Input.GetAxisRaw ("Vertical") < 0)
					{
						row = 5;
						letterSelection = 41 + column;
					}
				}
				else if (column > 5)
				{
					letterSelection = 32 + column;
				}
				else
				{
					letterSelection = row * 9 + column;
				}
				break;
			case 5:
				letterSelection = 41 + column;
				break;
			case 6:
				if (column == 8)
				{
					row = 10;
					letterSelection = 74;
				}
				else
				{
					letterSelection = 50 + column;
				}
				break;
			case 7:
				if (column == 6)
				{
					row = 10;
					letterSelection = 73;
				}
				else if (column == 7)
				{
					row = 10;
					letterSelection = 74;
				}
				else
				{
					letterSelection = 58 + column;
				}
				break;
			case 8:
				letterSelection = 64 + column;
				break;
			case 9:
				if (Input.GetAxisRaw ("Vertical") > 0)
				{
					if (column > 1 && column < 6)
					{
						row = 8;
						letterSelection = 64 + column;
					}
					else if (column == 6 || column == 7)
					{
						row = 6;
						letterSelection = 50 + column;
					}
					else if (column == 8)
					{
						row = 5;
						letterSelection = 49;
					}
					else
					{
						letterSelection = 70 + column;
					}
				}
				else if (Input.GetAxisRaw ("Vertical") < 0)
				{
					if (column > 1 && column < 5)
					{
						row = 10;
						letterSelection = 72;
					}
					else if (column == 5)
					{
						row = 10;
						letterSelection = 73;
					}
					else
					{
						letterSelection = 70 + column;
					}
				}
				break;
			case 10:
				if (column < 2)
				{
					row = 0;
					letterSelection = column;
				}
				else if (column > 1 && column < 5)
				{
					letterSelection = 72;
				}
				else if (column == 5 || column == 6)
				{
					letterSelection = 73;
				}
				else if (column == 7 || column == 8)
				{
					letterSelection = 74;
				}
				break;
			default:
				letterSelection = row * 9 + column;
				break;
			}
			letterSelection = Mathf.Clamp (letterSelection, 0, 74);
			HighlightLetter (letterSelection);
			audioSource.Play ();
			yield return new WaitForSeconds (0.2f);
			while (Input.GetAxisRaw ("Vertical") < 0 || Input.GetAxisRaw ("Vertical") > 0)
			{

				if (row == 10 && Input.GetAxis ("Vertical") < 0 || row == 0 && Input.GetAxis ("Vertical") > 0)
				{
					increment = 0;
					yield return new WaitForSeconds (0.075f);
				}
				else
				{
					letter [letterSelection].color = new Color (GameManager.P1.TextColor.r / 2, GameManager.P1.TextColor.g / 2, GameManager.P1.TextColor.b / 2, 1f);
					increment = (int)Input.GetAxisRaw ("Vertical");
					row -= increment;
					row = Mathf.Clamp (row, 0, 10);
					switch (row)
					{
					case 4:
						if (column > 1 && column < 6)
						{
							if (Input.GetAxisRaw ("Vertical") > 0)
							{
								row = 3;
								letterSelection = row * 9 + column;
							}
							else if (Input.GetAxisRaw ("Vertical") < 0)
							{
								row = 5;
								letterSelection = 41 + column;
							}
						}
						else if (column > 5)
						{
							letterSelection = 32 + column;
						}
						else
						{
							letterSelection = row * 9 + column;
						}
						break;
					case 5:
						letterSelection = 41 + column;
						break;
					case 6:
						if (column == 8)
						{
							row = 10;
							letterSelection = 74;
						}
						else
						{
							letterSelection = 50 + column;
						}
						break;
					case 7:
						if (column == 6)
						{
							row = 10;
							letterSelection = 73;
						}
						else if (column == 7)
						{
							row = 10;
							letterSelection = 74;
						}
						else
						{
							letterSelection = 58 + column;
						}
						break;
					case 8:
						letterSelection = 64 + column;
						break;
					case 9:
						if (Input.GetAxis ("Vertical") > 0)
						{
							if (column > 1 && column < 6)
							{
								row = 8;
								letterSelection = 64 + column;
							}
							else if (column == 6 || column == 7)
							{
								row = 6;
								letterSelection = 50 + column;
							}
							else if (column == 8)
							{
								row = 5;
								letterSelection = 49;
							}
							else
							{
								letterSelection = 70 + column;
							}
						}
						else if (Input.GetAxis ("Vertical") < 0)
						{
							if (column > 1 && column < 5)
							{
								row = 10;
								letterSelection = 72;
							}
							else if (column == 5)
							{
								row = 10;
								letterSelection = 73;
							}
							else
							{
								letterSelection = 70 + column;
							}
						}
						break;
					case 10:
						if (column < 2)
						{
							row = 0;
							letterSelection = column;
						}
						else if (column > 1 && column < 6)
						{
							letterSelection = 72;
						}
						else if (column == 5 || column == 6)
						{
							letterSelection = 73;
						}
						else if (column == 7 || column == 8)
						{
							letterSelection = 74;
						}
						break;
					default:
						letterSelection = row * 9 + column;
						break;
					}
					HighlightLetter (letterSelection);
					audioSource.Play ();
					yield return new WaitForSeconds (0.075f);
				}
			}
			break;
		}
		executing = false;
		yield return null;
	}
	private IEnumerator FadeIn ()
	{
		executing = true;
		while (screen.color.a > 0f)
		{
			screen.color = new Color (0f, 0f, 0f, screen.color.a - 0.025f);
			yield return null;
		}
		executing = false;
		yield return null;
	}
	private IEnumerator FadeOut ()
	{
		while (screen.color.a < 1f)
		{
			screen.color = new Color (0f, 0f, 0f, screen.color.a + 0.025f);
			yield return null;
		}
		if (Application.loadedLevelName == "HornedLizardCreation")
		{
			Application.LoadLevel ("NewGameIntro");
		}
		else if (Application.loadedLevelName == "CrayslugCreation")
		{

		}
		/*else if(Application.loadedLevelName == "CrayslugCreation")
		{

		}
		else if(Application.loadedLevelName == "CrayslugCreation")
		{
		
		}
		*/
		yield return null;
	}
	#endregion
}
