using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class Map : MonoBehaviour
{
	#region Variables
	private TextAsset mapFile;
	private Sprite [] tileset;
	private Sprite [] objectset;
	private GameObject [] tiles;
	private GameObject [] objects;

	public static MapNode [,] Node { get; set; }
	public static int Width { get; set; }
	public static int Height { get; set; }

	#endregion
	//Singleton
	public Map Instance { get; private set; }
	#region Properties
	#endregion

	#region MonoBehaviour
	private void Awake ()
	{
		if (!Instance)    //if no GameManager object exists...
		{
			Instance = this; //set this gameObject as GameManager
		} else {
			Destroy (this.gameObject); //Otherwise, Destroy this Instance of a GameManager
		}
	}
	private void Start ()
	{
		//SetMapData(); //Eliminate or rework
		//ReadMap();
		//DrawGrid();
	}

	private void OnDestroy ()
	{

	}
	#endregion

	#region Methods
	private void SetMapData ()
	{
		switch (Application.loadedLevelName) {
		case "1TheGreatTree":
			tileset = Resources.LoadAll ("Assets/Art/Tilesets/TheGreatTreeTileset").OfType<Sprite> ().ToArray ();
			objectset = Resources.LoadAll ("Assets/Art/Tilesets/TheGreatTreeObjectset").OfType<Sprite> ().ToArray ();
			mapFile = Resources.Load ("Maps/1TheGreatTree", typeof (TextAsset)) as TextAsset;
			break;

		case "2PurpleSwamp":
			tileset = Resources.LoadAll ("Assets/Art/Tilesets/PurpleSwampTileset").OfType<Sprite> ().ToArray ();
			objectset = Resources.LoadAll ("Assets/Art/Tilesets/PurpleSwampObjectset").OfType<Sprite> ().ToArray ();
			mapFile = Resources.Load ("Maps/2PurpleSwamp", typeof (TextAsset)) as TextAsset;
			break;

		case "3CrystalCave":
			tileset = Resources.LoadAll ("Assets/Art/Tilesets/CrystalCaveTileset").OfType<Sprite> ().ToArray ();
			objectset = Resources.LoadAll ("Assets/Art/Tilesets/CrystalCaveObjectset").OfType<Sprite> ().ToArray ();
			mapFile = Resources.Load ("Maps/3CrystalCave", typeof (TextAsset)) as TextAsset;
			break;

		case "4ElectricWormOrchard":
			tileset = Resources.LoadAll ("Assets/Art/Tilesets/ElectricWormOrchardTileset").OfType<Sprite> ().ToArray ();
			objectset = Resources.LoadAll ("Assets/Art/Tilesets/ElectricWormOrchardObjectset").OfType<Sprite> ().ToArray ();
			mapFile = Resources.Load ("Maps/4ElectricWormOrchard", typeof (TextAsset)) as TextAsset;
			break;

		case "5SaltInSpringDesert":
			tileset = Resources.LoadAll ("Assets/Art/Tilesets/SaltInSpringDesertTileset").OfType<Sprite> ().ToArray ();
			objectset = Resources.LoadAll ("Assets/Art/Tilesets/SaltInSpringDesertObjectset").OfType<Sprite> ().ToArray ();
			mapFile = Resources.Load ("Maps/5SaltInSpringDesert", typeof (TextAsset)) as TextAsset;
			break;

		case "6MeltingGlacier":
			tileset = Resources.LoadAll ("Assets/Art/Tilesets/MeltingGlacierTileset").OfType<Sprite> ().ToArray ();
			objectset = Resources.LoadAll ("Assets/Art/Tilesets/MeltingGlacierObjectset").OfType<Sprite> ().ToArray ();
			mapFile = Resources.Load ("Maps/6MeltingGlacier", typeof (TextAsset)) as TextAsset;
			break;

		case "7HolyMountain":
			tileset = Resources.LoadAll ("Assets/Art/Tilesets/HolyMountainTileset").OfType<Sprite> ().ToArray ();
			objectset = Resources.LoadAll ("Assets/Art/Tilesets/HolyMountainObjectset").OfType<Sprite> ().ToArray ();
			mapFile = Resources.Load ("Maps/7HolyMountain", typeof (TextAsset)) as TextAsset;
			break;

		case "8FloatingPalace":
			tileset = Resources.LoadAll ("Assets/Art/Tilesets/FloatingPalaceTileset").OfType<Sprite> ().ToArray ();
			objectset = Resources.LoadAll ("Assets/Art/Tilesets/FloatingPalaceObjectset").OfType<Sprite> ().ToArray ();
			mapFile = Resources.Load ("Maps/8FloatingPalace", typeof (TextAsset)) as TextAsset;
			break;

		case "9MoltenWell":
			tileset = Resources.LoadAll ("Assets/Art/Tilesets/MoltenWellTileset").OfType<Sprite> ().ToArray ();
			objectset = Resources.LoadAll ("Assets/Art/Tilesets/MoltenWellObjectset.png").OfType<Sprite> ().ToArray ();
			mapFile = Resources.Load ("Maps/9MoltenWell", typeof (TextAsset)) as TextAsset;
			break;

		case "10MagneticPlains":
			tileset = Resources.LoadAll ("Assets/Art/Tilesets/MagneticPlainsTileset").OfType<Sprite> ().ToArray ();
			objectset = Resources.LoadAll ("Assets/Art/Tilesets/MagneticPlainsObjectset").OfType<Sprite> ().ToArray ();
			mapFile = Resources.Load ("Maps/10MagneticPlains", typeof (TextAsset)) as TextAsset;
			break;

		case "11AsteroidRide":
			tileset = Resources.LoadAll ("Assets/Art/Tilesets/AsteroidRideTileset").OfType<Sprite> ().ToArray ();
			objectset = Resources.LoadAll ("Assets/Art/Tilesets/AsteroidRideObjectset").OfType<Sprite> ().ToArray ();
			mapFile = Resources.Load ("Maps/11AsteroidRide", typeof (TextAsset)) as TextAsset;
			break;

		case "12CosmicWormhole":
			tileset = Resources.LoadAll ("Assets/Art/Tilesets/CosmicWormholeTileset").OfType<Sprite> ().ToArray ();
			objectset = Resources.LoadAll ("Assets/Art/Tilesets/CosmicWormholeObjectset").OfType<Sprite> ().ToArray ();
			mapFile = Resources.Load ("Maps/12CosmicWormhole", typeof (TextAsset)) as TextAsset;
			break;
		}
	}

	private void ReadMap ()
	{
		//mapFile = Resources.Load("Maps/" + Application.loadedLevelName); //this line, line 134, line 135 eliminate the need for the SetMapData method?
		JSONNode json = JSON.Parse (mapFile.text);
		//tileset = Resources.LoadAll(json["tileset"][0]["image"].ToString()).OfType<Sprite>().ToArray();
		//objectset = Resources.LoadAll(json["tileset"][0]["image"].ToString()).OfType<Sprite>().ToArray();
		tiles = new GameObject [tileset.Length];
		//uint flippedHorizontally = 0x80000000;
		//uint flippedVertically = 0x40000000;
		//uint flippedDiagonally = 0x20000000;
		int tileIDOffset;
		uint tileID;
		uint objectID;
		bool blocked;
		string terrainType;
		int terrain;
		int x;
		int y;

		for (int tile = 0; tile < tiles.Length; tile++) {
			//tiles[tile] = new GameObject();
			//tiles[tile].AddComponent<SpriteRenderer>().sprite = tileset[tile];
		}
		for (int layer = json ["layers"].AsArray.Count; layer >= 0; layer--) {
			Width = json ["layers"] [layer] ["width"].AsInt;
			Height = json ["layers"] [layer] ["height"].AsInt;
			Node = new MapNode [Width, Height];

			for (int i = 0; i < (Width * Height); i++) {
				tileID = json ["layers"] [layer] ["data"] [i].AsUInt;
				//xscale = ((tileID & flippedHorizontally) != 0) ? -1.0f : 1.0f;
				//yscale = ((tileID & flippedVertically) != 0) ? -1.0f : 1.0f;
				//tileID = (tileID & ~(flippedHorizontally | flippedVertically | flippedDiagonally));

				//Generate Tile Layers...
				if (tileID != 0) {
					tileIDOffset = json ["tilesets"] [0] ["firstgid"].AsInt;

					blocked = json ["tilesets"] [0] ["tileproperties"] [(int)tileID - 1] ["Blocked"].AsBool;
					//terrainType = json["tilesets"][0]["tileproperties"][(int)tileID - tileIDOffset]["TerrainType"].ToString();

					y = (int)(i / Width);
					x = (int)(i % Width);
					Node [x, y].X = x;
					Node [x, y].Y = y;
					//Node[x, y].tile = new GameObject(tileset[tileID - 1], new Vector2((float)x * 128f, (float)y * - 128f), Quaternion.identity) as GameObject; //If this works, can eliminate tiles[] gameobj array
					//node[x, y].blocked = blocked;
					//node[x, y].terrain = terrainType;
					//Node[x, y].tile.name = "Tile" + i;
					//Node[x, y].tile.GetComponent<SpriteRenderer>().sortingLayerName = json["layers"][layer]["properties"]["sortingLayerName"].ToString();
				}

				//Generate Object Layers...
				objectID = json ["layers"] [layer] ["objects"] [i] ["gid"].AsUInt;
				//objectID = (objectID & ~(flippedHorizontally | flippedVertically | flippedDiagonally));

				if (objectID != 0) {

				}
			}
			for (int i = 0; i < tiles.Length; i++) {
				Destroy (tiles [i]);
			}
		}
		LinkNodes ();
	}

	private void LinkNodes ()
	{
		for (int i = 2; i < Width; i++) {
			for (int j = 2; j < Height; j++) {
				Node [i, j].neighbor = new MapNode [4];
				if (j == 2) {
					Node [i, j].prevY = null;
				} else {
					Node [i, j].prevY = Node [i, j - 1];
					Node [i, j].neighbor [0] = Node [i, j].prevY;
				}
				if (i >= Width - 3) {
					Node [i, j].nextX = null;
				} else {
					Node [i, j].nextX = Node [i + 1, j];
					Node [i, j].neighbor [1] = Node [i, j].nextX;
				}

				if (j >= Height - 3) {
					Node [i, j].nextY = null;
				} else {
					Node [i, j].nextY = Node [i, j + 1];
					Node [i, j].neighbor [2] = Node [i, j].nextY;
				}
				if (i == 2) // so that only nodes within grid are assigned linked nodes
				{
					Node [i, j].prevX = null;
				} else {
					Node [i, j].prevX = Node [i - 1, j];
					Node [i, j].neighbor [3] = Node [i, j].prevX;
				}
			}
		}
	}

	private void DrawGrid ()
	{
		if (GameManager.P1.GridOn != false) {
			for (int i = 0; i < Width - 2; i++) {
				for (int j = 0; j < Height - 3; j++) {
					LineRenderer line = Node [i, j].tile.AddComponent<LineRenderer> ();
					line.SetWidth (2, 2);
					line.SetVertexCount (2);
					line.useWorldSpace = true;
					line.useLightProbes = false;
					line.receiveShadows = false;
					line.reflectionProbeUsage = 0;
					line.sortingLayerName = "Grid";
					line.sortingOrder = 1;
					line.SetColors (GameManager.P1.GridColor, GameManager.P1.GridColor);
					line.material = new Material (Shader.Find ("Sprites/Default"));
					line.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

					if (j == 0) {
						//line.SetPosition(0, new Vector2((float)256, (float)i*-128+(height-2)*128));
						//line.SetPosition(1, new Vector2((float)(height - 2) * 128, (float)i*-128+(height - 2) * 128));
					}
					if (i == 0) {
						//line.SetPosition(0, new Vector2((float)j*128+256, (float)(height - 3)*128));
						//line.SetPosition(1, new Vector2((float)j*128+256, (float)128));
					}
					//^^^ REDO THIS, Lines are in upper right quadrant, should be in lower right quadrant
				}
			}
		}
	}

	private void GeneratePlayers ()
	{
		//for()
		{

		}
	}

	private void GenerateEnemies ()
	{

	}

	private void GeneratePlayScene () // Cutscene that happens on map?
	{
		switch (Application.loadedLevelName) {
		case "1TheGreatTree":
			break;
		case "2PurpleSwamp":
			break;
		case "3CrystalCave":
			break;
		case "4ElectricWormOrchard":
			break;
		case "5SaltInSpringDesert":
			break;
		case "6MeltingGlacier":
			break;
		case "7HolyMountain":
			break;
		case "8FloatingPalace":
			break;
		case "9MoltenWell":
			break;
		case "10MagneticPlains":
			break;
		case "11AsteroidRide":
			break;
		case "12CosmicWormhole":
			break;
		}
	}
	#endregion
}
