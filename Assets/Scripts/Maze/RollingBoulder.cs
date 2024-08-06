using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBoulder : MonoBehaviour
{
    public Transform[] pathPoints; //points to define boulders path
    public float speed = 3f;

    private int currentPointIndex;
    private List<MazePlayerInput> players;

    void Start()
    {
        transform.position = pathPoints[currentPointIndex].position;
        players = new List<MazePlayerInput>(FindObjectsOfType<MazePlayerInput>());
    }

    void Update()
    {
        MoveAlongPath();
    }

    void MoveAlongPath()
    {
        //if there are no path points, return
        if (pathPoints.Length == 0) return;

        //move towards current path point
        transform.position = Vector3.MoveTowards(transform.position, pathPoints[currentPointIndex].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, pathPoints[currentPointIndex].position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % pathPoints.Length;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MazePlayerInput playerInput = collision.gameObject.GetComponent<MazePlayerInput>();
            if (playerInput != null)
            {
                playerInput.ReturnToStart();
            }
        }
    }
}
