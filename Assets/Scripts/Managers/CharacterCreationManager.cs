using UnityEngine;
using System.Collections;

public class CharacterCreationManager : MonoBehaviour 
{
	//Variables
	private static int characterKingdom; // 0 = plants/ 1 = Fungi/ 2 = Mineral / 3 = Micro 
	private static int characterNum; //each kingdom has 4 nums/ 4 types of creatures
	private static AbstractCharacter character; 

	public static CharacterCreationManager Instance{get; private set;}
	//MonoBehavior
	private void Start () 
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
	
	private void Update () 
	{
		if(Input.GetAxisRaw("Horizontal") != 0)
		{

		}
		else if(Input.GetAxisRaw("Vertical") != 0)
		{

		}
	}
}
