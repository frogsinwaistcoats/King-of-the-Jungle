using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBoulder : MonoBehaviour
{
    public Transform[] pathPoints; //points to define boulders path
    public float speed = 3f;
    public float rotateSpeed = 150f;

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
        Rotate();
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

    void Rotate()
    {
        Vector3 direction = pathPoints[currentPointIndex].position - transform.position;

        //if moving to left
        if (direction.x < 0)
        {
            transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f, Space.Self);
        }
        //if moving to right
        else if (direction.x > 0)
        {
            transform.Rotate(0f, -rotateSpeed * Time.deltaTime, 0f, Space.Self);
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
