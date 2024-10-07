using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinHandler : MonoBehaviour
{
    public float spinDuration = 1f;
    MazePlayerInput mazePlayerInput;

    private void Awake()
    {
        mazePlayerInput = GetComponent<MazePlayerInput>();
    }

    public void StartSpin(int spinNum, Vector3 spinPos)
    {
        StartCoroutine(SpinPlayer(spinNum, spinPos));
    }


    private IEnumerator SpinPlayer(int spinNum, Vector3 spinPos)
    {
        mazePlayerInput.OnDisable();

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        //Vector3 initialPosition = transform.position;

        float timeElapsed = 0f;
        float rotationSpeed = (1f * 360f) / spinDuration;

        while (timeElapsed < spinDuration)
        {
            transform.position = spinPos;

            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (spinNum == 1)
        {
            SpinControls1(mazePlayerInput.playerID);
        }
        else if (spinNum == 2)
        {
            SpinControls2(mazePlayerInput.playerID);
        }
        else if (spinNum == 3)
        {
            SpinControls3(mazePlayerInput.playerID);
        }
    }

    public void SpinControls1(int ID)
    {
        InputControls inputControls = mazePlayerInput.inputControls;
        if (mazePlayerInput.playerID == ID)
        {
            inputControls = mazePlayerInput.inputManager.players[mazePlayerInput.playerID].playerControls;
            inputControls.SpinControl1.Movement.performed += mazePlayerInput.OnMove;
            inputControls.SpinControl1.Movement.canceled += mazePlayerInput.OnMove;
        }
    }

    public void SpinControls2(int ID)
    {
        InputControls inputControls = mazePlayerInput.inputControls;
        if (mazePlayerInput.playerID == ID)
        {
            inputControls = mazePlayerInput.inputManager.players[mazePlayerInput.playerID].playerControls;
            inputControls.SpinControl2.Movement.performed += mazePlayerInput.OnMove;
            inputControls.SpinControl2.Movement.canceled += mazePlayerInput.OnMove;
        }
    }

    public void SpinControls3(int ID)
    {
        InputControls inputControls = mazePlayerInput.inputControls;
        if (mazePlayerInput.playerID == ID)
        {
            inputControls = mazePlayerInput.inputManager.players[mazePlayerInput.playerID].playerControls;
            inputControls.SpinControl3.Movement.performed += mazePlayerInput.OnMove;
            inputControls.SpinControl3.Movement.canceled += mazePlayerInput.OnMove;
        }
    }

    public void MasterControls(int ID)
    {
        InputControls inputControls = mazePlayerInput.inputControls;
        if (mazePlayerInput.playerID == ID)
        {
            inputControls = mazePlayerInput.inputManager.players[mazePlayerInput.playerID].playerControls;
            inputControls.MasterControls.Movement.performed += mazePlayerInput.OnMove;
            inputControls.MasterControls.Movement.canceled += mazePlayerInput.OnMove;
        }
    }
}
