using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetOnFall : MonoBehaviour
{
    void OnTriggerEnter2D()
    {
        if (Input.GetKey("left") && !Input.GetKey("right"))
        {
            SceneManager.LoadScene("Level1_left");
        }
        else if (Input.GetKey("right") && !Input.GetKey("left"))
        {
            SceneManager.LoadScene("Level1_right");
        }
        else // ( (!Input.GetKey("left") && !Input.GetKey("right")) || ((Input.GetKey("left") && Input.GetKey("right")) )
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
