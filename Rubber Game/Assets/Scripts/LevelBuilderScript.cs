﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RandomTile
{
	public float weight;
	public GameObject tilePrefab;
}

public class LevelBuilderScript : MonoBehaviour {

	public GameObject backgroundPrefab;
	private float currentBkgPosition;
	public float lengthOfBackground;
	private List<GameObject> backgroundPool;
	public float distanceBeforeDestroyingBkg;
	public float distanceFromBkgCreation;

	public List<RandomTile> tiles;
	public float lengthOfTile;
	private float totalWeightOfTiles;
	private float currentTilePosition;
	private List<GameObject> tilesPool;
	public float distanceBeforeDestroyingTile;
	public float distanceFromTileCreation;

	public List<RandomTile> obstacles;
	public float lengthOfObstacles;
	private float totalWeightOfObstacles;
	private float currentObstaclePosition;
	private List<GameObject> obstaclesPool;
	public float distanceBeforeDestroyingObstacle;
	public float distanceFromObstacleCreation;

	public GameObject levelParent;

	public GameObject player;



	// Use this for initialization
	void Start () 
	{
		tilesPool = new List<GameObject> ();
		obstaclesPool = new List<GameObject> ();
		backgroundPool = new List<GameObject> ();

		// background
		totalWeightOfTiles = 0;
		foreach (RandomTile tile in tiles)
		{
			totalWeightOfTiles += tile.weight;
		}
		currentTilePosition = -lengthOfTile;

		// obstacles and collectibles
		totalWeightOfObstacles = 0;
		foreach (RandomTile obstacle in obstacles)
		{
			totalWeightOfObstacles += obstacle.weight;
		}
		currentObstaclePosition = lengthOfObstacles*5;

		CreateNewTileIfNeeded ();
		CreateNewTileIfNeeded ();
		CreateNewTileIfNeeded ();
		CreateNewBkgIfNeeded ();
		CreateNewObstacleIfNeeded ();
		CreateNewObstacleIfNeeded ();
		CreateNewObstacleIfNeeded ();
		StartCoroutine (WaitAndGarbageCollect (1.1f));
		StartCoroutine (WaitAndCreateTiles (0.2f));
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	IEnumerator WaitAndGarbageCollect(float timer)
	{
		yield return new WaitForSeconds (timer);
		DestroyLastTileIfNeeded ();
		DestroyLastBkgIfNeeded ();
		DestroyLastObstacleIfNeeded ();
		StartCoroutine (WaitAndGarbageCollect (timer));
	}
	IEnumerator WaitAndCreateTiles(float timer)
	{
		yield return new WaitForSeconds (timer);
		CreateNewTileIfNeeded ();
		CreateNewBkgIfNeeded ();
		CreateNewObstacleIfNeeded ();
		StartCoroutine (WaitAndCreateTiles (timer));
	}

	
	public void CreateNewBkgIfNeeded()
	{
		bool needToCreateBkg = backgroundPool.Count.Equals (0);
		if (!needToCreateBkg)
		{
			GameObject rightTile = backgroundPool[backgroundPool.Count-1];
			float distanceWithPlayer = rightTile.transform.position.x - player.transform.position.x;
			needToCreateBkg = (distanceWithPlayer < distanceFromBkgCreation);
		}
		if (needToCreateBkg)
		{
			GameObject newBkg = (GameObject) Instantiate(backgroundPrefab, new Vector3(currentBkgPosition, 0, 0), Quaternion.identity);
			newBkg.transform.SetParent( levelParent.transform );
			backgroundPool.Add(newBkg);
			currentBkgPosition += lengthOfBackground;
		}
	}
	public void DestroyLastBkgIfNeeded()
	{
		GameObject leftTile = backgroundPool [0];
		float distanceWithPlayer = player.transform.position.x - leftTile.transform.position.x;
		if (distanceWithPlayer > distanceBeforeDestroyingBkg)
		{
			backgroundPool.RemoveAt(0);
			Destroy (leftTile);
		}
	}

	public void CreateNewTileIfNeeded()
	{
		bool needToCreateTile = tilesPool.Count.Equals (0);
		if (!needToCreateTile)
		{
			GameObject rightTile = tilesPool[tilesPool.Count-1];
			float distanceWithPlayer = rightTile.transform.position.x - player.transform.position.x;
			needToCreateTile = (distanceWithPlayer < distanceFromTileCreation);
		}
		if (needToCreateTile)
		{
			float randomNumber = Random.Range(0, totalWeightOfTiles);
			float weight = 0;
			int index = 0;
			foreach (RandomTile tile in tiles)
			{
				if (randomNumber >= weight && randomNumber <= (weight + tile.weight))
				{
					break;
				}
				weight += tile.weight;
				index++;
			}
			GameObject newTile = (GameObject) Instantiate(tiles[index].tilePrefab, new Vector3(currentTilePosition, 0, 0), Quaternion.identity);
			newTile.transform.SetParent( levelParent.transform );
			tilesPool.Add(newTile);
			currentTilePosition += lengthOfTile;
		}
	}
	public void DestroyLastTileIfNeeded()
	{
		GameObject leftTile = tilesPool [0];
		float distanceWithPlayer = player.transform.position.x - leftTile.transform.position.x;
		if (distanceWithPlayer > distanceBeforeDestroyingTile)
		{
			tilesPool.RemoveAt(0);
			Destroy (leftTile);
		}
	}
	
	public void CreateNewObstacleIfNeeded()
	{
		bool needToCreateObstacle = obstaclesPool.Count.Equals (0);
		if (!needToCreateObstacle)
		{
			GameObject rightObstacle = obstaclesPool[obstaclesPool.Count-1];
			float distanceWithPlayer = rightObstacle.transform.position.x - player.transform.position.x;
			needToCreateObstacle = (distanceWithPlayer < distanceFromObstacleCreation);
		}
		if (needToCreateObstacle)
		{
			float randomNumber = Random.Range(0, totalWeightOfObstacles);
			float weight = 0;
			int index = 0;
			foreach (RandomTile tile in obstacles)
			{
				if (randomNumber >= weight && randomNumber <= (weight + tile.weight))
				{
					break;
				}
				weight += tile.weight;
				index++;
			}
			GameObject newObstacle = (GameObject) Instantiate(obstacles[index].tilePrefab, new Vector3(currentObstaclePosition, 0, 0), Quaternion.identity);
			newObstacle.transform.SetParent( levelParent.transform );
			obstaclesPool.Add(newObstacle);
			currentObstaclePosition += lengthOfObstacles;
		}
	}
	public void DestroyLastObstacleIfNeeded()
	{
		GameObject leftObstacle = obstaclesPool [0];
		float distanceWithPlayer = player.transform.position.x - leftObstacle.transform.position.x;
		if (distanceWithPlayer > distanceBeforeDestroyingObstacle)
		{
			obstaclesPool.RemoveAt(0);
			Destroy (leftObstacle);
		}
	}
}
