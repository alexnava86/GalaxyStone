using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartScreenManager : MonoBehaviour
{
	#region Variables
	public Text statusText;
	public AudioClip song;
	public AudioClip selectSound;
	public AudioClip toggleSound;
	public Image screen;
	public Image gameLogo;
	public Image startCursor;
	public Image cursorShadow;

	public Text continueText;
	public Text startText;
	public Text loadGameText;
	public Text vsModeText;
	public Text optionsText;

	private Text [] text;
	private int selection;
	private RectTransform logoPos;
	private bool selected = false;
	private AudioSource songSource;
	private AudioSource soundSource;
	public delegate void StartScreenAction ();
	public static event StartScreenAction OnStartScreenAction;

	public static StartScreenManager Instance { get; private set; }
	#endregion

	#region MonoBehaviour
	private void Start ()
	{
		if (!Instance)
		{
			Instance = this;
		}
		else
		{
			Destroy (this);
		}
		songSource = this.gameObject.AddComponent<AudioSource> ();
		soundSource = this.gameObject.AddComponent<AudioSource> ();
		songSource.clip = song;
		songSource.Play ();
		Initialize ();
	}

	private void Update ()
	{
		if (Input.GetButtonDown ("Vertical") && Input.GetAxisRaw ("Vertical") != 0)// && selected != true)
		{
			if (text != null && gameLogo.rectTransform.position.y == 500f)
			{
				text [selection].color = new Color (0.5f, 0.5f, 0.5f);
				selection = selection - (int)Input.GetAxisRaw ("Vertical");
				selection = Mathf.Clamp (selection, 0, text.Length - 1);
				text [selection].color = new Color (1f, 1f, 1f);
				startCursor.rectTransform.position = new Vector2 (text [selection].rectTransform.position.x - 75f, text [selection].rectTransform.position.y + 15f);
				soundSource.clip = toggleSound;
				soundSource.Play ();
			}
		}
		if (Input.GetButtonDown ("Confirm"))
		{
			if (selected != true)
			{
				if (gameLogo.rectTransform.position.y > 500f)
				{
					logoPos.position = new Vector2 (logoPos.position.x, 500f);
					for (int i = 0; i < text.Length; i++)
					{
						text [i].color = new Color (0.5f, 0.5f, 0.5f, 1f);
					}
					text [selection].color = new Color (1f, 1f, 1f, 1f);
					startCursor.color = new Color (0f, 0f, 0f, 1f);
					cursorShadow.color = new Color (1f, 1f, 1f, 1f);
				}
				else
				{
					StartCoroutine (FadeOut ());
					soundSource.clip = selectSound;
					soundSource.Play ();
				}
			}
		}
	}
	#endregion

	#region Methods
	private void Initialize ()
	{
		StartCoroutine (FadeIn ());

		Color empty = new Color (0f, 0f, 0f, 0f);
		startCursor.color = empty;
		cursorShadow.color = empty;
		startText.color = empty;
		optionsText.color = empty;
		continueText.color = empty;
		loadGameText.color = empty;
		vsModeText.color = empty;

		if (GameManager.Instance.devMode != false) //if Player has unlocked vs mode || if dev mode is enabled? 
		{
			statusText.text = "Dev. Mode: ON";

			text = new Text [5];
			text [0] = continueText;
			text [1] = startText;
			text [2] = loadGameText;
			text [3] = vsModeText;
			text [4] = optionsText;

			for (int i = 0; i < text.Length; i++)
			{
				text [i].rectTransform.anchoredPosition = new Vector2 (0f, text [0].rectTransform.anchoredPosition.y - (i * 25));
			}
		}
		else if (GameManager.CheckForSavedData () != true)
		{
			statusText.text = "Dev. Mode: OFF"; // ""; //DisplayNothing
			text = new Text [2];
			text [0] = startText;
			text [1] = optionsText;

			for (int i = 0; i < text.Length; i++)
			{
				text [i].rectTransform.anchoredPosition = new Vector2 (0f, text [0].rectTransform.anchoredPosition.y - (i * 25));
			}
		}
		else //A game has been saved... Dev mode not enabled...
		{
			statusText.text = "Dev. Mode: OFF";

			text = new Text [4];
			text [0] = continueText;
			text [1] = startText;
			text [2] = loadGameText;
			text [3] = optionsText;

			for (int i = 0; i < text.Length; i++)
			{
				text [i].rectTransform.anchoredPosition = new Vector2 (0, text [0].rectTransform.anchoredPosition.y - (i * 25));
			}

		}
		startCursor.rectTransform.position = new Vector2 (text [0].rectTransform.position.x - 75f, text [0].rectTransform.position.y + 15f);
		logoPos = gameLogo.rectTransform;
	}

	private static void ContinueGame ()
	{
		//Load most recently played game file...
		if (GameManager.CheckForSavedData () != false)
		{

		}
		//Application.LoadLevel("StageSelect");
		if (GameManager.CheckForSavedData () != true && GameManager.Instance.devMode != false)
		{
			StartScreenManager.Instance.statusText.text = "NO SAVE DATA EXISTS YET!";
		}
	}

	private static void StartNewGame ()
	{
		GameManager.CPU = GameManager.Instance.gameObject.AddComponent<Enemy> ();
		//TimeManager.Instance.enabled = true;
		Application.LoadLevel ("HornedLizardCreation");
	}

	private static void LoadGame ()
	{
		Application.LoadLevel ("LoadGameScene");
	}

	private static void VsMode ()
	{
		if (GameManager.Instance.devMode == true) // || if game has been beaten at least once
		{
			Application.LoadLevel ("VsModeSetupScene");
		}
	}

	private static void OptionsMenu ()
	{
		Application.LoadLevel ("OptionsScene");
	}


	#endregion

	#region Coroutines
	private IEnumerator DropScreen ()
	{
		while (Camera.current.transform.position.y > 0f)
		{
			Camera.current.transform.position = new Vector2 (Camera.current.transform.position.x, Camera.current.transform.position.y - 1f);
		}
		yield return null;
	}

	private IEnumerator DropGameLogo ()
	{
		while (logoPos.position.y > 500f)
		{
			logoPos.position = new Vector2 (logoPos.position.x, logoPos.position.y - 0.75f);
			yield return null;
		}
		for (int i = 0; i < text.Length; i++)
		{
			text [i].color = new Color (0.5f, 0.5f, 0.5f);
		}
		text [selection].color = Color.white;
		startCursor.color = Color.black;
		cursorShadow.color = Color.white;
		yield return null;
	}

	private IEnumerator FadeIn ()
	{
		while (screen.color.a > 0f)
		{
			screen.color = new Color (0f, 0f, 0f, screen.color.a - 0.025f);
			yield return null;
		}
		StartCoroutine (DropGameLogo ());
		yield return null;
	}

	private IEnumerator FadeOut ()
	{
		selected = true;
		Destroy (gameLogo.gameObject);
		Destroy (startCursor.gameObject);
		for (int i = 0; i < text.Length; i++)
		{
			Destroy (text [i].gameObject);
		}

		while (screen.color.a < 1f)
		{
			screen.color = new Color (0f, 0f, 0f, screen.color.a + 0.025f);
			yield return null;
		}

		switch (selection)
		{
		case 0: // Continue
			if (text [0] == continueText)
			{
				ContinueGame ();
			}
			else
			{
				StartNewGame ();
			}
			break;

		case 1: // Start
			if (text [1] == startText)
			{
				StartNewGame ();
			}
			if (text [1] == optionsText)
			{
				OptionsMenu ();
			}
			break;

		case 2: //Load Game
			LoadGame ();
			break;

		case 3: //VS Mode
			if (text [3] == optionsText)
			{
				OptionsMenu ();
			}
			if (text [3] == vsModeText)
			{
				VsMode ();
			}
			break;

		case 4: //Options
			OptionsMenu ();
			break;
		}
		yield return null;
	}
	#endregion
}