using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnItems;   
    public Vector3 spawnAreaMin;      
    public Vector3 spawnAreaMax;      
    public float frequency;           
    public float initialSpeed;       

    private float lastSpawnedTime;   
    private bool waiting;             
    public void Stop(float duration)
    {
        if (waiting) return;

        Time.timeScale = 0.0f;  // Freeze game time
        StartCoroutine(Wait(duration));  
    }

    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration); 
        Time.timeScale = 1.0f;  
        waiting = false;
    }

    void Update()
    {
        if (Time.time > lastSpawnedTime + frequency)
        {
            Spawn();
            lastSpawnedTime = Time.time;
        }
    }

   
    public void Spawn()
    {
      
        GameObject spawnItem = spawnItems[Random.Range(0, spawnItems.Length)];

        
        Vector3 randomSpawnPos = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y),
            Random.Range(spawnAreaMin.z, spawnAreaMax.z));

       
        GameObject newSpawnedObject = Instantiate(spawnItem, randomSpawnPos, Quaternion.identity);

        
        newSpawnedObject.GetComponent<Rigidbody>().velocity = transform.forward * initialSpeed;


        newSpawnedObject.transform.parent = transform;
    }
}
