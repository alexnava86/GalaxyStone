using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StageSelectManager : MonoBehaviour 
{
	private GameObject screen; //current UI prefab associated with game state
	private GameObject stageSelect; //Stage Select UI Prefab
	private GameObject merchant; //Merchant UI Prefab
	private GameObject quartermaster; // Quartermaster UI Prefab
	private GameObject characterCreation; //Character Creation UI Prefab
	private GameObject nameEntryMenu;
	private int levelSelection = 1;
	private MapInfo[] stages = new MapInfo[12];
    
	#region MonoBehaviour
	private void Awake()
	{

	}
	private void Start () 
	{
		//screen = stageSelectScreen;
	}
	private void Update () 
	{
		//if(Input.GetButtonDown("RightTriggerB"))
		{
			if(screen != null) 
			{
				//StartCoroutine(LerpCurrenScreenRight());
			}
		}
		//if(Input.GetButtonDown("LeftTriggerB"))
		{
			if(screen != null)
			{
				//StartCoroutine(LerpCurrenScreenLeft());
			}
		}
		//if(Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") != 0)
		{
			//levelSelection = levelSelection + (int)Input.GetAxisRaw("Horizontal");
			//Level.Highlight(GameManager.Instance.Unlocked[levelSelection]);
		}
	}
	#endregion

	#region Methods
	private void DisplayMapData()
	{

	}
	#endregion

	#region Coroutines
	private IEnumerator LerpCurrentScreenRight()
	{
		//RectTransform screenPos = screen.transform;
		GameObject next = null; // = UI Asset that refers to Merchant UI if in stage select UI, and stage select UI if in quarter master

		if(screen == stageSelect)
		{
			next = merchant;
		}
		else if(screen == quartermaster)
		{
			next = stageSelect;
		}
		screen = null;
		//while(screenPos.position != new Vector2( , ));
		{
			//screen 
		}
		screen = next;
		yield return null;
	}

	private IEnumerator LerpCurrentScreenLeft()
	{
		yield return null;
	}

	private IEnumerator FadeIn()
	{
		yield return null;
	}

	private IEnumerator FadeOut()
	{
		yield return null;
	}
	#endregion

	#region Strategy Pattern Design 
	public interface IStage
	{
		void Highlight(MapInfo stage);
		void Select(MapInfo stage);
	}
	private class Stage
	{
		private static Dictionary<int, IStage> stages = new Dictionary<int, IStage>();

		static Stage()
		{
			stages.Add(1, new TheGreatTree());
			//stages.Add(2, new PurpleSwamp());
			//stages.Add(3, new Crystal Cave());
			//stages.Add(4, new ElectricWormOrchard());
			//stages.Add(5, new);
			//stages.Add(6, new);
			//stages.Add(7, new);
			//stages.Add(8, new);
			//stages.Add(9, new);
			//stages.Add(10, new);
			//stages.Add(11, new);
			//stages.Add(12, new);

		}
		public static void Highlight(int state, MapInfo stage) // (int state, MapData stage)
		{
			stages[state].Highlight(stage);
		}
		public static void Select(int state, MapInfo stage)
		{
			stages[state].Select(stage);
		}
	}
	private class TheGreatTree : IStage
	{	
		public void Highlight(MapInfo stage)
		{

		}
		public void Select(MapInfo stage)
		{
			//Load Great Tree... Assign startnodes
			//stage.HasBeenPlayed = true; tell the game manager that this stage has been played once
		}
	}
	#endregion
}

