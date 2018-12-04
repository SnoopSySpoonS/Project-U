using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project_U.Assets.Scripts.Pathfinding.Algorithms;
using Project_U.Assets.Scripts.Pathfinding;
using Project_U.Assets.Scripts;

public class Controller : MonoBehaviour {

		// Singelton
	public static Controller Game;
	public static AStar pathAlgo;
	public static Tile SelectedTile;
	public static Tile HoveredTile;
	private GameObject _selectedGameObject;
	private GameObject _hoverededGameObject;
	public Material Grass;
	public Material Hover;
	public Material Origin;
	
	public GameObject TilePrefab;
	void Awake () {
		if (Game == null)
		{
			DontDestroyOnLoad(gameObject);
			Initiate();
		}
		else if (Game != this)
		{
			Destroy(gameObject);
		}
	}

    private void Initiate()
    {
		Game = this;
		World world = new World(); 
		pathAlgo = new AStar(world);
		GenerateMapTiles();
    }

    private void GenerateMapTiles()
    {
        Vector3 pos = new Vector3();
		for (var row = 0; row < 3; row++)
		{
			for (var column = 0; column < 4; column++)
			{
				pos.x = column;
				pos.z = row;
				GameObject newTile = (GameObject)Instantiate(
					TilePrefab, pos, Quaternion.identity, this.transform
				);
				newTile.GetComponentInChildren<TextMesh>().text = "(" + pos.x + ", " + pos.z + ")";
				Tile newWoldTile = new Tile(column, row, 0);
				pathAlgo.World.AddTile(newWoldTile);
				for (int i = -1; i < 2; i++)
				{
					for (int j = -1; j < 2; j++)
					{
						Position posOff = new Position(newWoldTile.Position.Column + i, newWoldTile.Position.Row + j, 0);
						pathAlgo.World.AddNabourship(newWoldTile.Position, posOff);
					}
				}
			}
		}
    }

    private void Update()
	{
		SelectedTile = SelectTileOnClick();
		HoveredTile = SelectTargetOnHover();
		if (Input.GetKey(KeyCode.P))
			FindPathToHoveringTile();
	}

    private void FindPathToHoveringTile()
    {
        if (SelectedTile == null || HoveredTile == null)
			{ return; }
		var path = pathAlgo.CalculatePath(SelectedTile, HoveredTile);
		Debug.Log("Lenght of path: " + path.Count);
    }

    private Tile SelectTargetOnHover()
    {
		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		RaycastHit hit;
        if( Physics.Raycast( ray, out hit, 100 ) )
		{
			var hitPosition = hit.transform.gameObject.transform.position;
			var gO = hit.transform.gameObject;
			// if (gO == null)
			// 	{ return null; }
			if (_selectedGameObject == null)
			{ 
				if (_hoverededGameObject != null)
					{ _hoverededGameObject.GetComponentInChildren<MeshRenderer>().material = Grass; }
				_hoverededGameObject = null;
				return null; 
			}
			if (_selectedGameObject == gO)
			{ 
				if (_hoverededGameObject != null)
					{ _hoverededGameObject.GetComponentInChildren<MeshRenderer>().material = Grass; }
				_hoverededGameObject = null;
				return null; 
			}
			if (_hoverededGameObject == gO) 
				{ return HoveredTile; }
			if (_hoverededGameObject != null)
				{ _hoverededGameObject.GetComponentInChildren<MeshRenderer>().material = Grass; }
			_hoverededGameObject = gO;
			gO.GetComponentInChildren<MeshRenderer>().material = Hover;
			return (Tile)pathAlgo.World.GetTile(new Position(hitPosition.x, hitPosition.z, hitPosition.y));
		}
		return HoveredTile;
    }

    private Tile SelectTileOnClick()
    {
        if( Input.GetMouseButtonDown(0) )
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;
			
			if( Physics.Raycast( ray, out hit, 100 ) )
			{
				var hitPosition = hit.transform.gameObject.transform.position;
				var gO = hit.transform.gameObject;
				if (_selectedGameObject == gO)
				{					
					{ _selectedGameObject.GetComponentInChildren<MeshRenderer>().material = Grass; }
					_selectedGameObject = null;
					return null;
				}
				if (_selectedGameObject != null)
					{ _selectedGameObject.GetComponentInChildren<MeshRenderer>().material = Grass; }
				if (_hoverededGameObject != null)
					{ _hoverededGameObject.GetComponentInChildren<MeshRenderer>().material = Grass; }
				gO.GetComponentInChildren<MeshRenderer>().material = Origin;
				_selectedGameObject = gO;
				_hoverededGameObject = null;
				return (Tile)pathAlgo.World.GetTile(new Position(hitPosition.x, hitPosition.z, hitPosition.y));
			}
		}
		return SelectedTile;
    }
}
