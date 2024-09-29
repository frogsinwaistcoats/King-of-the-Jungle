using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerBumper : MonoBehaviour
{
    public GameObject Player;
    public GameObject Boundary;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("has entered");
        if (other.tag == "Player" )
        {
            Destroy(Player);
        }

    }

}