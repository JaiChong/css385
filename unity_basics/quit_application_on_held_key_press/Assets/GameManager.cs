using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Stores the holdTime across frames
    private struct BufferData
    {
        public float holdTime;
    }
    BufferData bd = new BufferData();
    
    void Start()
    {
        // Initializes or resets the holdTime
        bd.holdTime = 3f;   // in seconds
    }

    void Update()
    {
        if (bd.holdTime > 0)
        {//
            Debug.Log("bd.holdTime > 0");
            if (!Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.Escape))
            {
                // if bd.holdTime hasn't hit 0 and neither Q nor Escape are pressed, reset bd.holdTime
                Start();
            }

            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.Escape))
            {
                // if bd.holdTime hasn't hit 0 and either Q or Escape is pressed, decremeent bd.holdTime
                bd.holdTime -= Time.deltaTime;
                Debug.Log(bd.holdTime);
            }
        }

        else // (bd.q <= 0 || bd.esc <= 0)
        {
            // if bd.holdTime hits 0, quit the application
            Application.Quit();
        }
    }
}
