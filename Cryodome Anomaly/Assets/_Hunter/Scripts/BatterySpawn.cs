using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BatterySpawn : MonoBehaviour
{
    public GameObject battery;
    private float spawnTime = 12.0f;
    private float timer;
    private float numOfBattery;
    private GameObject PlayerPos = null;

    private void Start()
    {
        if(PlayerPos == null)
        {
            PlayerPos = GameObject.Find("Player Variant");
        }
        timer = 1.0f;
    }

    Vector3 GetRandomLocation()
    {
        //Calculates and returns triangulation of navmesh containing vertices, triangle indices and navmesh layers
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        // Pick the first instance of a random triangle in the nav mesh
        int t = Random.Range(0, navMeshData.indices.Length - 3);

        // Select a random point on the first instance of a random trangle on the nav mesh
        //Set our point as a fraction inbetween indices[t] and indices[t+1] (Done using lerp)
        Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
        //lerp our point again with 3rd indice of triangle
        Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);

        return point;
    }
    //Function used to spawn 3 batterys total, each one randomly and at 25 units of time 
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >  spawnTime && numOfBattery !=3)
        {
            spawnBattery();
            timer = 0;
            numOfBattery++;
        }
    }
    //Actually spawn the battery
    public void spawnBattery()
    {

        Vector3 spawnLocation = GetRandomLocation();
        Vector3 finalSpawn = spawnLocation + new Vector3(0.0f, 0.41f, 0.0f);
        Instantiate(battery, finalSpawn, Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f));
    }

}
