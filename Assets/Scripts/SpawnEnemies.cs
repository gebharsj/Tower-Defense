using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemies : MonoBehaviour {

    public List<GameObject> enemyPrefabs;
    public float xSpawn;
    public float zSpawn;
    public float spawnDelay = 1f;

    Vector3 spawnPoint;

    enum SpawnOrigin
    {
        North,
        East,
        South,
        West
    }

    SpawnOrigin _spawnOrigin = SpawnOrigin.North;

	// Use this for initialization
	void Start () {
        StartCoroutine(Spawn());
	}

    IEnumerator Spawn()
    {
        ChooseSpawnOrigin();

        switch (_spawnOrigin)
        {
            case SpawnOrigin.North:
                spawnPoint = new Vector3(Random.Range(-xSpawn, xSpawn), 0, zSpawn);
                break;
            case SpawnOrigin.East:
                spawnPoint = new Vector3(xSpawn, 0, Random.Range(-zSpawn, zSpawn));
                break;
            case SpawnOrigin.South:
                spawnPoint = new Vector3(Random.Range(-xSpawn, xSpawn), 0, -zSpawn);
                break;
            case SpawnOrigin.West:
                spawnPoint = new Vector3(-xSpawn, 0, Random.Range(-zSpawn, zSpawn));
                break;
        }

        GameObject clone =  Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], spawnPoint, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(Spawn());
    }

    void ChooseSpawnOrigin()
    {
        int randomInt = Random.Range(0, 4);
        switch(randomInt)
        {
            case 0:
                _spawnOrigin = SpawnOrigin.North;
                break;
            case 1:
                _spawnOrigin = SpawnOrigin.East;
                break;
            case 2:
                _spawnOrigin = SpawnOrigin.South;
                break;
            case 3:
                _spawnOrigin = SpawnOrigin.West;
                break;
            default:
                _spawnOrigin = SpawnOrigin.North;
                break;
        }
    }
}