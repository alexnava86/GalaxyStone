using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable] //?
public abstract class Player : MonoBehaviour 
{
	private List<AbstractCharacter> characterIndex = new List<AbstractCharacter>(); //use array[25]??
	private TimeSpan TimePlayed{get; set;}
	private int mana; //Life Force Player has available to use
	private int handicap;
	
	//UI Settings... Loaded when loading characters from a particular save slot/ continuing game
	public Sprite TextBox{get; set;}
	public Sprite TextBorder{get; set;}
	public Sprite TextBoxGradient{get; set;}
	public Font TextFont{get; set;}
	public Color TextColor{get; set;}
	public Color TextBoxColor{get; set;}
	public Color TextBorderColor{get; set;}
	public Color TextShadowColor{get; set;}
	public Color GradientColor{get; set;}
	public float GradientTransparency{get; set;}
	public float TextBoxTransparency{get; set;}
	public float TextShadowTransparency{get; set;}
	public bool GradientEnabled{get; set;}
	public float MusicVolume{get; set;}
	public float SfxVolume{get; set;}
	public int Difficulty{get; set;}
	public bool GridOn{get; set;}
	public Color GridColor{get; set;}
	public bool VsModeUnlocked{get; set;}

	private bool autoMode = false;
	private bool detailedInfo = false;
	private MapInfo[] mapData = new MapInfo[12];
	public delegate void PlayerAction();
	public static event PlayerAction OnPlayerAction;
    
	public List<AbstractCharacter> CharacterIndex //All characters in this player's party, not just active
	{
		get
		{
			return characterIndex;
		}
		set
		{
			characterIndex = value;
		}
	}
	private void Start()
	{

	}
	protected void OnEnable()
	{
		//OnPlayerAction += StartTurn;
		//OnPlayerAction();
		//OnPlayerAction -= StartTurn;
		//OnPlayerAction += 
	}
	private void OnDestroy()
	{
		if(TimeManager.Instance != null)
		{
			TimePlayed.Add(TimeManager.TimePlayed);
		}
	}

	public void AddCharacterToPlayerIndex(AbstractCharacter character)//Call this method after a stage has been selected, and the player chooses their units
	{
		if(this.CharacterIndex.Count < 25)
		{
			this.CharacterIndex.Add(character);
            character.Operator = this;
        }
    }
	public void RemoveCharacterFromPlayerIndex(AbstractCharacter character) //When a character dies, or is intentionally released/deleted
	{
		if(this.CharacterIndex.Contains(character))
		{
			this.CharacterIndex.Remove(character);
		}
    }
}
