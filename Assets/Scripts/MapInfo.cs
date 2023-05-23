using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MapInfo
{
	//The purpose of this class is to send information to the StageSelect screen about the enemies in the map, whether or not the level has been played, objectives(?), 
	public bool hasBeenPlayed{get; set;}
	//public bool unlocked{get; set;}
	public int enemies{get; set;}
	public int fertileSpaces{get; set;}
	public TextAsset json{get; set;}
}
