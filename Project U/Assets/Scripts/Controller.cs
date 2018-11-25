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
	public static AStar pathAlgo = new AStar();
	public static Tile SelectedTile;
	
	public GameObject TilePrefab;
	void Awake () {
		if (Game == null)
		{
			DontDestroyOnLoad(gameObject);
			Game = this;
			Initiate();
		}
		else if (Game != this)
		{
			Destroy(gameObject);
		}
	}

    private void Initiate()
    {
		GenerateMapTiles();
    }

    private void GenerateMapTiles()
    {
        Vector3 pos = new Vector3();
		for (var row = 0; row < 10; row++)
		{
			for (var column = 0; column < 20; column++)
			{
				pos.x = column;
				pos.z = row;
				GameObject newTile = (GameObject)Instantiate(
					TilePrefab, pos, Quaternion.identity, this.transform
				);
			}
		}
    }

    private void Update()
	{
		SelectedTile = SelectTileOnClick();
		FindPathToHoveringTile();
	}

    private Tile SelectTileOnClick()
    {
        if( Input.GetMouseButtonDown(0) )
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;
			
			if( Physics.Raycast( ray, out hit, 100 ) )
			{
				
				return World[hit.transform.gameObject.transform.position];
			}
		}
		return SelectedTile;
    }
}
