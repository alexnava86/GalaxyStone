using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MerchantMenu : MonoBehaviour {

	public Text buyText;
	public Text sellText;
	public Text itemText;
	public Image cursor;
	public List<Item> itemInventory = new List<Item>();
	private List<Text> itemList = new List<Text>();
	private bool confirm = false;
	private bool buy = true;
	private bool sell = false;

	private void Start () 
	{
		if(buy == true)
		{
			buyText.color = new Color(1f, 1f, 1f,1f); //test change to gamemanager.instance.textcolor
			sellText.color = new Color(0.5f, 0.5f, 0.5f, 1f);
		}
		else if(sell == true)
		{
			sellText.color = new Color(1f, 1f, 1f,1f);
			buyText.color = new Color(0.5f, 0.5f, 0.5f, 1f);
		}
		itemInventory.Add(Resources.Load("Prefabs/Items/GinkaHerb", typeof(GinkaHerb)) as GinkaHerb);
		foreach(Item item in itemInventory)
		{

			Instantiate(itemText);
		}
	}

	private void Update () 
	{
	
	}
}
