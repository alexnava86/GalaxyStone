using UnityEngine;
//using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.IO;

public class GameManager : MonoBehaviour
{
	#region Variables
	public AudioClip song;
	public static Enemy CPU { get; set; }
	public static Player1 P1 { get; set; }
	public static Player2 P2 { get; set; }
	public static Player3 P3 { get; set; }
	public static Player4 P4 { get; set; }
	public static AbstractCharacter Current { get; set; } //character active in turn order...
	private static List<AbstractCharacter> Active { get; set; } //= new List<AbstractCharacter>(); //All characters active in current battle
																//public List<AbstractCharacter> characterLibrary = new List<AbstractCharacter> ();
	public AudioSource songSource { get; set; }
	[SerializeField]
	public bool devMode { get; set; }
	public static bool VsMode { get; set; }
	public static bool TutorialMode { get; set; }

	private static GameObject dialogue;
	private static bool dialogueSkip;
	private static bool dialogueOverflow;
	private static bool dialogueComplete;
	//public delegate void GameManagerEvent();
	//public static event GameManagerEvent OnGameStart;

	//Singleton
	public static GameManager Instance { get; private set; }
	#endregion

	#region MonoBehaviour
	private void Awake ()
	{
		if (!Instance)
		{
			Instance = this;
			DontDestroyOnLoad (this.gameObject);
		}
		else
		{
			Destroy (this.gameObject);
		}
	}
	private void Start ()
	{
		this.songSource = this.gameObject.AddComponent<AudioSource> ();
		P1 = this.gameObject.AddComponent<Player1> ();
		RestoreAllSettingsToDefault ();
		//Test2(DisplayCharacterInfo
		//Application.targetFrameRate = 60;
		//Force Resolution?
		this.songSource.clip = song;
		this.songSource.Play ();
	}
	private void OnDestroy ()
	{
		QuitGame ();
	}
	private void OnGUI ()
	{

	}
	#endregion

	#region Methods
	public static void PauseGame ()
	{

	}
	public static void ResumeGame ()
	{

	}

	public static void StartBattle ()
	{
		//SortCharactersBySpeed();
		//Current = Active.(x <= x.Speed);

	}

	public static void SortCharactersBySpeed ()
	{
		//if two characters have the same speed, create a digital 50% scenario to determine who goes first?
	}

	public static void StartTurn () //void ActivatePlayer(Player player) //?
	{
		/*
		StartCoroutine(CameraManager.LerpCameraToMapNode(Current.Node));
		Current.ExecuteActiveStatusEffects();
		
		if(Current.autonomous != false)
		{
			//Current.Operator.enabled = false;
			if(Current.hostile != false)
			{
				//Character is an enemy, or hostile ally...
				//player = CPU;
				//Add a bunch of methods to a multi-cast Action/ AI?
			}
			else
			{
				//Character is an ally in auto-mode, or uncontrolled ally such as NPC...
				
			}
		}
		else
		{
			//Character is Player controlled, either a player's character or an enemy unit they can control
			if(vsMode != false)
			{
				CPU.enabled = false;
				player = p1;
			}
			else
			{
			
			}
		}
		*/
	}

	public static void ResetTurnOrder () //may not be necessary, maybe able to just refer to active by adding/removing to list
	{
		//Sort Active characters by speed

	}

	public static void EndBattle () //Player was victorious!
	{
		if (VsMode != true)
		{

		}
		Active.Clear ();
	}

	public static void GameOver () //Player lost...
	{
		//Application.LoadLevel("GameOver"); //Continue? 
	}

	public static void QuitGame ()
	{
		TimeManager.LastPlayed = DateTime.Now;
	}

	public static void SaveGameFile (int saveslot)
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;

		switch (saveslot)
		{
		case 1: //Save the current game data into save slot1
			file = File.Open (Application.persistentDataPath + "/PlayerData1.dat", FileMode.Open);
			//file = new File ();
			bf.Serialize (file, P1);
			file.Close ();
			break;
		case 2: //Save the current game data into save slot2
			file = File.Open (Application.persistentDataPath + "/PlayerData2.dat", FileMode.Open);
			bf.Serialize (file, P1);
			file.Close ();
			break;
		case 3: //Save the current game data into save slot3
			file = File.Open (Application.persistentDataPath + "/PlayerData3.dat", FileMode.Open);
			bf.Serialize (file, P1);
			file.Close ();
			break;
		case 4: //Save the current game data into save slot4
			file = File.Open (Application.persistentDataPath + "/PlayerData4.dat", FileMode.Open);
			//PlayerData data4 = new PlayerData();
			//data4.PlayerInfo = P1;
			bf.Serialize (file, P1);
			file.Close ();
			break;
		}
	}

	public static void LoadGameFile (Player player, int saveslot) //Player loading the save file, save slot index
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;

		switch (saveslot)
		{
		case 1:
			if (File.Exists (Application.persistentDataPath + "/PlayerData1.dat")) //Check to see if there is a file in save slot #1...
			{
				file = File.Open (Application.persistentDataPath + "/PlayerData1.dat", FileMode.Open);
				Player data1 = (Player)bf.Deserialize (file);
				bf.Serialize (file, data1);
				file.Close ();
				player = data1;
			}
			break;
		case 2:
			if (File.Exists (Application.persistentDataPath + "/PlayerData2.dat"))
			{
				file = File.Open (Application.persistentDataPath + "/PlayerData2.dat", FileMode.Open);
				Player data2 = (Player)bf.Deserialize (file);
				bf.Serialize (file, data2);
				file.Close ();
				player = data2;
			}
			break;
		case 3:
			if (File.Exists (Application.persistentDataPath + "/PlayerData3.dat"))
			{
				file = File.Open (Application.persistentDataPath + "/PlayerData3.dat", FileMode.Open);
				Player data3 = (Player)bf.Deserialize (file);
				bf.Serialize (file, data3);
				file.Close ();
				player = data3;
			}
			break;
		case 4:
			if (File.Exists (Application.persistentDataPath + "/PlayerData4.dat"))
			{
				file = File.Open (Application.persistentDataPath + "/PlayerData4.dat", FileMode.Open);
				Player data4 = (Player)bf.Deserialize (file);
				bf.Serialize (file, data4);
				file.Close ();
				player = data4;
			}
			break;
		}
	}
	public static void DeleteSaveData (int saveSlot)
	{
		switch (saveSlot)
		{
		case 1:
			if (File.Exists (Application.persistentDataPath + "/PlayerData1.dat"))
			{
				File.Delete (Application.persistentDataPath + "/PlayerData1.dat");
			}
			break;
		case 2:
			if (File.Exists (Application.persistentDataPath + "/PlayerData2.dat"))
			{
				File.Delete (Application.persistentDataPath + "/PlayerData2.dat");
			}
			break;
		case 3:
			if (File.Exists (Application.persistentDataPath + "/PlayerData3.dat"))
			{
				File.Delete (Application.persistentDataPath + "/PlayerData3.dat");
			}
			break;
		case 4:
			if (File.Exists (Application.persistentDataPath + "/PlayerData4.dat"))
			{
				File.Delete (Application.persistentDataPath + "/PlayerData4.dat");
			}
			break;
		}
	}

	public static bool CheckForSavedData ()
	{
		FileStream [] saves = new FileStream [4];

		//Search for saved game data...
		if (File.Exists (Application.persistentDataPath + "/PlayerData1.dat"))
		{
			saves [0] = File.Open (Application.persistentDataPath + "/PlayerData1.dat", FileMode.Open);
			return true;
		}
		else if (File.Exists (Application.persistentDataPath + "/PlayerData2.dat"))
		{
			saves [1] = File.Open (Application.persistentDataPath + "/PlayerData2.dat", FileMode.Open);
			return true;
		}
		else if (File.Exists (Application.persistentDataPath + "/PlayerData3.dat"))
		{
			saves [2] = File.Open (Application.persistentDataPath + "/PlayerData3.dat", FileMode.Open);
			return true;
		}
		else if (File.Exists (Application.persistentDataPath + "/PlayerData4.dat"))
		{
			saves [3] = File.Open (Application.persistentDataPath + "/PlayerData4.dat", FileMode.Open);
			return true;
		}
		else
		{
			return false;
		}
	}

	public static void RestoreAllSettingsToDefault ()
	{
		Sprite [] sprites = Resources.LoadAll ("Art/Interface/TextBorders").OfType<Sprite> ().ToArray ();
		P1.TextBorder = sprites [0];
		sprites = Resources.LoadAll ("Art/Interface/TextBoxes").OfType<Sprite> ().ToArray ();
		P1.TextBox = sprites [0];
		sprites = Resources.LoadAll ("Art/Interface/Gradients").OfType<Sprite> ().ToArray ();
		P1.TextBoxGradient = sprites [0];

		P1.TextBorderColor = new Color (1f, 1f, 1f, 1f); //white
		P1.TextBoxColor = new Color (0f, 0.5f, 0.5f, 1f); //blue/green
		P1.TextBoxTransparency = 1f;
		P1.GradientTransparency = 0f;
		P1.GradientColor = new Color (0f, 0f, 0f, 0f); //black, transparent
		P1.TextFont = Resources.Load ("Fonts/Masaaki-Regular", typeof (Font)) as Font;
		P1.TextColor = new Color (1f, 1f, 1f, 1f); //white
		P1.TextShadowColor = new Color (0f, 0f, 0f, 1f); //black
		P1.TextShadowTransparency = 1f; //same^
		P1.GradientEnabled = false;
		P1.GridOn = true;
		P1.GridColor = new Color (0f, 0f, 0f, 0.5f); //black, half-transparent
		P1.Difficulty = 3;
		P1.MusicVolume = 1f;
		P1.SfxVolume = 1f;
	}

	public static void SetNewGameMapData ()
	{
		//foreach(MapInfo stage in unlocked)
		{
			//stage.HasBeenPlayed = false;
		}
	}

	private void Test (Action myMethod)// coroutine)
	{
		Debug.Log (myMethod.Method.Name); //returns whatever method is passed into this function...
										  //myMethod.Invoke();
	}

	private void Test2 (Func<string, IEnumerator> coroutine)
	{
		//Debug.Log(coroutine.Method.GetParameters()[0].Name); //returns name of actual parameter of the coroutine passed into this method, not value passed in
		//Debug.Log(coroutine.Method.Name); //returns whatever coroutine is passed into this function...
	}
	#endregion

	#region Coroutines
	private IEnumerator DisplayCharacterInfo (string bob)
	{
		yield return null;
	}

	private IEnumerator DisplayTargetInfo ()
	{
		yield return null;
	}

	private IEnumerator DisplayCharacterStats ()
	{
		yield return null;
	}

	private IEnumerator DisplayTargetStats ()
	{
		yield return null;
	}

	private IEnumerator DisplayTerrainInfo ()
	{
		yield return null;
	}
	public IEnumerator GeneratePlanetDialogue ()
	{
		yield return null;
	}

	public IEnumerator GenerateDialogue (AbstractCharacter character, string text)
	{
		if (dialogue.activeInHierarchy != true)
		{
			//if there is no active dialogue box, instantiate a new one
			Instantiate (dialogue);
			RectTransform boxPos = dialogue.GetComponent<RectTransform> ();
			while (boxPos.position.y != 0)
			{
				boxPos.position = new Vector2 (boxPos.position.x, boxPos.position.y + 1f);
				yield return null;
			}
		}
		else if (dialogueOverflow != false)
		{
			if (Input.GetButton ("Confirm"))
			{
				//
			}
		}
		else if (dialogueComplete != false)
		{
			//if there is an active dialogue box...
			if (Input.GetButton ("Confirm"))
			{
				Destroy (dialogue);
			}
		}
		yield return null;
	}
	#endregion
}