using UnityEngine;
using System.Collections;

public class MapNode 
{
	public int X; //node x
	public int Y; //node y
	public int heuristic;
	public int distance;
	public MapTerrain terrain;
	public Item item;
	public GameObject tile;
	public AbstractCharacter character;
	//public 
	public MapNode parent;
	public MapNode child;
	public MapNode prevX;
	public MapNode prevY;
	public MapNode nextX;
	public MapNode nextY;
	public MapNode[] neighbor;
}
