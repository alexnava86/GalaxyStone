using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour 
{
	private int cost;
	private string itemName;
	private MapNode node; //the location of this item on the map, if any
    private AbstractCharacter character; // character that has possession of this item, if any


	public int Cost
	{
		get
		{
			return cost;
		}
		set
		{
			cost = value;
		}
	}
	public string ItemName
	{
		get
		{
			return itemName;
		}
		set
		{
			itemName = value;
		}
	}
	public MapNode Node
	{
		get
		{
			return node;
		}
		set
		{
			node = value;
		}
	}
	public AbstractCharacter Character
	{
		get
		{
			return character;
		}
		set
		{
			character = value;
		}
	}

	protected void Start () 
	{
		this.ItemName = this.GetType().ToString();
		this.Character = null;
		this.Node = null;
	}

	public abstract void Use(AbstractCharacter target);
}
