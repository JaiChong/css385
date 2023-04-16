using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetOnKeypress : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.R) && Input.GetKey("left") && !Input.GetKey("right"))
        {
            SceneManager.LoadScene("Level1_left");
        }
        else if (Input.GetKey(KeyCode.R) && Input.GetKey("right") && !Input.GetKey("left"))
        {
            SceneManager.LoadScene("Level1_right");
        }
        else if (Input.GetKey(KeyCode.R)) // || (Input.GetKey(KeyCode.R) && Input.GetKey("right") && Input.GetKey("left"))
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
