using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class NewGameIntro : MonoBehaviour
{
	#region Variables
	public Image screen;
	public AudioClip textBlip;
	public AudioClip song;
	private HornedLizard hornedLizard;
	private AudioSource audioSource;
	private GameObject dialogue;
	private bool dialogueComplete = false;
	private int textSpeed;

	public static NewGameIntro Instance { get; private set; }
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
			Destroy (this.gameObject);
		}
		Debug.Log (GameManager.P1.CharacterIndex [0].CharName);
		//hornedLizard = Instantiate (GameManager.P1.CharacterIndex [0], new Vector2 (0f, 0f), Quaternion.identity) as HornedLizard;
		//hornedLizard = Instantiate (GameManager.P1.CharacterIndex [0] as HornedLizard);
		//hornedLizard = (HornedLizard)Instantiate (GameManager.P1.CharacterIndex [0]);
		audioSource = this.gameObject.AddComponent<AudioSource> ();
		StartCoroutine (FadeIn ());
	}
	private void Update ()
	{
		if (Input.GetButtonDown ("Confirm"))
		{
			if (dialogue != null && dialogueComplete != false)
			{
				hornedLizard.GetComponent<Mobility> ().enabled = true;
				Destroy (dialogue);
			}
		}
	}
	private void OnGUI ()
	{

	}
	#endregion

	#region Coroutines
	private IEnumerator FadeIn ()
	{
		while (screen.color.a > 0f)
		{
			screen.color = new Color (screen.color.r, screen.color.g, screen.color.b, screen.color.a - 0.025f);
			yield return null;
		}
		//hornedLizard.GetComponent<Mobility>().enabled = true;
		yield return null;
		//StartCoroutine(GenerateDialogue("Hello? Who is that...?"));
	}

	public IEnumerator FadeOut ()
	{
		while (screen.color.a < 1f)
		{
			screen.color = new Color (screen.color.r, screen.color.g, screen.color.b, screen.color.a + 0.025f);
			yield return null;
		}
		if (GameManager.TutorialMode != true)
			Application.LoadLevel ("1TheGreatTree");

		if (GameManager.TutorialMode != false)
			Application.LoadLevel ("1TheGreatTreeTutorial");
		//Instantiate(Resources.Load("Prefabs/Interface/TutorialPrompt", typeof(GameObject)) as GameObject);
		yield return null;
	}
	private IEnumerator GenerateDialogue (string text)
	{
		dialogueComplete = false;
		hornedLizard.GetComponent<Mobility> ().enabled = false;
		dialogue = Instantiate (Resources.Load ("Prefabs/Interface/DialogueBox", typeof (GameObject)) as GameObject);
		Text nameText = dialogue.transform.GetChild (0).transform.GetChild (0).transform.GetChild (2).GetComponent<Text> ();
		Text dialogueText = dialogue.transform.GetChild (0).transform.GetChild (0).transform.GetChild (2).transform.GetChild (0).GetComponent<Text> ();
		nameText.text = Player1.Instance.CharacterIndex [0].CharName + ": ";
		dialogueText.rectTransform.anchoredPosition = new Vector2 (Player1.Instance.CharacterIndex [0].CharName.Length * 25, dialogueText.rectTransform.anchoredPosition.y);


		char [] letter = text.ToCharArray ();
		int count = 0;

		audioSource.clip = textBlip;
		yield return new WaitForSeconds (0.5f);

		while (count < letter.Length)
		{
			if (Input.GetButton ("Confirm"))
			{
				textSpeed = 1;
			}
			else
			{
				textSpeed = 3;
			}
			if (letter [count] != ' ')
			{
				audioSource.Play ();
			}
			dialogueText.text = dialogueText.text.Insert (count, letter [count].ToString ());
			count++;
			yield return new WaitForSeconds ((float)textSpeed * 0.05f);
		}
		dialogueComplete = true; // TEST ONLY
		yield return null;
	}

	private IEnumerator DisplayDialogueBox ()
	{
		dialogue = Instantiate (Resources.Load ("Prefabs/Interface/DialogueBox", typeof (GameObject)) as GameObject);
		RectTransform trans = dialogue.transform.GetChild (0).GetComponent<RectTransform> ();
		Debug.Log (dialogue.transform.GetChild (0).gameObject);
		Debug.Log (trans.position.y);
		trans.position = new Vector2 (trans.position.x, trans.position.y);
		//while(trans.position.y != 0)
		{
			//trans.position = new Vector2(trans.position.x, trans.position.y + 1f);
			yield return null;
		}
		yield return null;
	}
	private IEnumerator HideDialogueBox ()
	{
		RectTransform trans = dialogue.GetComponent<RectTransform> ();
		while (trans.position.y != -400)
		{
			trans.position = new Vector2 (trans.position.x, trans.position.y);
		}
		Destroy (dialogue);
		yield return null;
	}
	#endregion
}
