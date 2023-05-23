using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapCursor : MonoBehaviour 
{
	#region Variables
	public static MapNode Node{get; set;} 
	private static bool executing;
	private static AbstractCharacter highlighted; 
	private static AbstractCharacter selected;
	private static List<AbstractCharacter> targeted = new List<AbstractCharacter>();
	private static List<GameObject> movementNodes = new List<GameObject>();
	private static List<GameObject> attackNodes = new List<GameObject>();
	private static List<GameObject> path = new List<GameObject>();
	private static PathManager test = new PathManager();
	private static CharacterStats characterStats;
	private static CharacterStats targetStats;
	private static CharacterInfo characterInfo;
	private static CharacterInfo targetInfo;
	//Singleton
	public static MapCursor Instance{get; private set;}
	#endregion

	
	#region MonoBehaviour
	private void Awake()
	{
		
    }
	private void Start () 
	{
        
    }
    private void OnEnable()
	{
		this.GetComponent<SpriteRenderer>().enabled = true;
        if(GameManager.P1 != null && Player1.Instance.enabled != false)
		{
			//Player1.OnConfirm += Confirm;
			//...
		}
		//else if(GameManager.P2 != null && Player2.Instance.enabled != false)
		//{}
		//else if(GameManager.P3 != null && Player3.Instance.enabled != false)
		//{}
		//else if(GameManager.P4 != null && Player4.Instance.enabled != false)
		//{}
	}
	private void Update () 
	{

	}
	private void OnDisable()
	{
		this.GetComponent<SpriteRenderer>().enabled = false;
		//Player1.OnConfirm -= Confirm;
		//...
	}
	private void OnGUI()
	{
		
	}
	#endregion
	
	#region Methods
	private static void DisplayCharacterInfo(AbstractCharacter character)
	{
		//CharacterInfo info = Instantiate(Resources.Load("Prefabs/Interface/CharacterStats", typeof(CharacterInfo)) as CharacterInfo);
		//info.character = character;
	}
	private static void DisplayCharacterStats(AbstractCharacter character)
	{
		//CharacterStats stats = Instantiate(Resources.Load("Prefabs/Interface/CharacterStats", typeof(CharacterStats)) as CharacterStats);
		//stats.character = character;
	}
	private static void DisplayTargetInfo(AbstractCharacter target)
	{
		//CharacterInfo info = Instantiate(Resources.Load("Prefabs/Interface/CharacterStats", typeof(CharacterInfo)) as CharacterInfo);
		//info.character = target;
	}
	private static void DisplayTargetStats(AbstractCharacter target)
	{
		//CharacterStats stats = Instantiate(Resources.Load("Prefabs/Interface/CharacterStats", typeof(CharacterStats)) as CharacterStats);
		//stats.character = target;
	}
	private static void DisplayCharacterRange(AbstractCharacter character)
	{
		//foreach(MapNode node in character.MovementRange)
		{
			//movementNodes.Add((GameObject)Instantiate(Resources.Load("Prefabs/Interface/MovementNode", typeof(GameObject))as GameObject));
			//movementNodes[movementNodes.Count - 1].GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0.75f);
		}
	}
	private static void HighlightCharacter(AbstractCharacter character)
	{
		selected = null;
		highlighted = character;
		DisplayCharacterRange(character);
		DisplayCharacterStats(character);
		//if(GameManager.P1.detailedInfo != false)
		{
			DisplayCharacterInfo(character);
		}
	}
	private static void SelectCharacter(AbstractCharacter character)
	{
		highlighted = null;
		selected = character;
		//foreach(GameObject node in movementNodes)
		{
			//node.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0.75f);
		}
	}
	private static void DeselectCharacter(AbstractCharacter character)
	{
		selected = null;
		highlighted = null;
		//attackNodes.Clear();//?
		//movementNodes.Clear();
		if(Node.character != null)
		{
			HighlightCharacter(Node.character);
		}
	}
	private static void Confirm()
	{
		if(highlighted != null && selected == null)
		{
			SelectCharacter(highlighted);
		}
	}
	private static void Cancel()
	{
		if(selected != null)
		{
			DeselectCharacter(selected);
		}
	}
	private void MoveCursor(string direction)
	{
		if(executing != true)
		{
			StartCoroutine(LerpCursor(direction));
		}
	}
	#endregion
	
	#region Coroutines
	private static IEnumerator LerpCursor(string direction)
	{	
		executing = true;

		switch(direction)
		{
		case "Horizontal":
			int increment = (int)Input.GetAxisRaw("Horizontal");
			if(selected != null)
			{
				if(selected.MovementRange.Contains(Map.Node[Node.X + increment, Node.Y]) != false)
				{
					Node.X += increment;
				}
			}
			else
			{
				Mathf.Clamp(Node.X, 2, Map.Width - 2);
			}
			MapCursor.Instance.transform.position = new Vector2((float)Node.X * 128f, (float)Node.Y * -128f);
			yield return new WaitForSeconds(0.2f);
			while(Input.GetAxisRaw("Horizontal") != 0)
			{
				if(selected != null)
				{
					if(selected.MovementRange.Contains(Map.Node[Node.X + increment, Node.Y]) != false)
					{
						Node.X += increment;
					}
				}
				else
				{
					Node.X += increment;
					Mathf.Clamp(Node.X, 2, Map.Width - 2);
				}
				MapCursor.Instance.transform.position = new Vector2((float)Node.X * 128f, (float)Node.Y * -128f);
				yield return new WaitForSeconds(0.075f);
			}
			break;

		case "Vertical":
			increment = (int)Input.GetAxisRaw("Vertical");
			if(selected != null)
			{
				if(selected.MovementRange.Contains(Map.Node[Node.X, Node.Y + increment]) != false)
				{
					Node.Y += increment;
				}
			}
			else
			{
				Node = Map.Node[Node.X, Node.Y];
				Mathf.Clamp(Node.Y, 2, Map.Height - 2);
			}
			MapCursor.Instance.transform.position = new Vector2((float)Node.X * 128f, (float)Node.Y * -128f);
			yield return new WaitForSeconds(0.2f);
			while(Input.GetAxisRaw("Vertical") != 0)
			{
				if(selected != null)
				{
					if(selected.MovementRange.Contains(Map.Node[Node.X, Node.Y + increment]) != false)
					{
						Node.Y += increment;
					}
				}
				else
				{
					Node.Y += increment;
					Mathf.Clamp(Node.Y, 2, Map.Height - 2);
				}
				MapCursor.Instance.transform.position = new Vector2((float)Node.X * 128f, (float)Node.Y * -128f);
				yield return new WaitForSeconds(0.075f);
			}
			break;
		}
		Node = Map.Node[Node.X, Node.Y];
		//if(selected == null)
		{
			if(Node.character != null)
			{
				//HighlightCharacter(node.character);
			}
			else
			{
				if(characterInfo != null)
				{
					Destroy(characterInfo);
				}
				if(characterStats != null)
				{
					Destroy(characterStats);
				}
			}
		}
		//else
		{

		}
		//node.terrain.
		executing = false;
		yield return null;
	}
	//
	#endregion
}
