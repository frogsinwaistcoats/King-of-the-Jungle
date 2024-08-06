using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinHandler : MonoBehaviour
{
    public Collider spinCollider;
    public float spinDuration = 1f;

    MazePlayerInput mazePlayerInput;

    private void Awake()
    {
        mazePlayerInput = GetComponent<MazePlayerInput>();
    }

    public void StartSpin()
    {
        StartCoroutine(SpinPlayer());
    }


    private IEnumerator SpinPlayer()
    {
        mazePlayerInput.OnDisable();
        spinCollider.enabled = false;

        float timeElapsed = 0f;
        float rotationSpeed = (1f * 360f) / spinDuration;

        while (timeElapsed < spinDuration)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        SpinControls(mazePlayerInput.playerID);
    }

    private void SpinControls(int ID)
    {
        InputControls inputControls = mazePlayerInput.inputControls;
        if (mazePlayerInput.playerID == ID)
        {
            inputControls = mazePlayerInput.inputManager.players[mazePlayerInput.playerID].playerControls;

            //randomly generate number
            int randomControl = Random.Range(1, 4);

            switch (randomControl)
            {
                case 1:
                    inputControls.SpinControl1.Movement.performed += mazePlayerInput.OnMove;
                    inputControls.SpinControl1.Movement.canceled += mazePlayerInput.OnMove;
                    break;
                case 2:
                    inputControls.SpinControl2.Movement.performed += mazePlayerInput.OnMove;
                    inputControls.SpinControl2.Movement.canceled += mazePlayerInput.OnMove;
                    break;
                case 3:
                    inputControls.SpinControl3.Movement.performed += mazePlayerInput.OnMove;
                    inputControls.SpinControl3.Movement.canceled += mazePlayerInput.OnMove;
                    break;
            }
        }
    }
}
