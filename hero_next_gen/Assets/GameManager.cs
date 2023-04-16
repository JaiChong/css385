using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    public Text textElement;
    public string textValue;
    public int enemyTargetRandom;
    public bool enemyTargetHero;
    public int enemyTargetSequential;

    // prints
    public string movementControl;      // updated by Hero.cs
    public int eggsInWorld;             // updated here
    public int enemiesInWorld;          // updated here; should always be 10
    public int enemiesDestroyed;        // updated by Enemy.cs
    public int waypointsDestroyed;      // updated by Waypoints.cs
    public int collisionsWithEnemies;   // updated by Hero.cs; number of times hero has touched an enemy
    public int heroDeaths;              // updated by Hero.cs

    public struct WaypointData
    {
        public string tag;
        public bool destroyed;
    }
    public WaypointData[] wds = new WaypointData[6];

    void Start()
    {
        // Initialize text
        textValue = "    Movement Control Scheme: Mouse\n" +
                    "    Eggs in World:                        0\n" +
                    "    Enemies in World:                 0\n" +
                    "    Enemies Destroyed:              0\n" +
                    "    Waypoints Destroyed:           0\n" +
                    "    Collisions with Enemies:      0\n" +
                    "    Hero Deaths:                          0\n";
        textElement.text = textValue;

        // Initializes enemy target values
        enemyTargetRandom = -1;
        enemyTargetHero = false;
        enemyTargetSequential = 0;

        // Initializes struct values
       eggsInWorld = 0;
       enemiesInWorld = 0;
       enemiesDestroyed = 0;
       collisionsWithEnemies = 0;

        // Populates WaypointData array
        for (int i = 0; i < 6; i++)
        {
            wds[i].tag = "Waypoint" + (char)(65+i);
            wds[i].destroyed = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            Application.Quit();
        }
        
        // Check for target change input
        if (Input.GetKeyDown("j"))
        {
            if (enemyTargetRandom == -1)
            {
                enemyTargetRandom = 6;
                NextWaypoint(-1);
            }
            else
            {
                enemyTargetRandom = -1;
            }
            enemyTargetHero = false;
        }
        else if (Input.GetKeyDown("k"))
        {
            enemyTargetHero = !enemyTargetHero;
            enemyTargetRandom = -1;
        }
        
        textValue = "    Movement Control Scheme: " +movementControl + "\n" +
                    "    Eggs in World:                        " + eggsInWorld + "\n" +
                    "    Enemies in World:                 " + enemiesInWorld + "\n" +
                    "    Enemies Destroyed:              " + enemiesDestroyed + "\n" +
                    "    Waypoints Destroyed:           " + waypointsDestroyed + "\n" +
                    "    Collisions with Enemies:      " + collisionsWithEnemies + "\n" +
                    "    Hero Deaths:                          " + heroDeaths + "\n";
        textElement.text = textValue;

       eggsInWorld = GameObject.FindGameObjectsWithTag("Egg").Length;
       enemiesInWorld = GameObject.FindGameObjectsWithTag("Enemy").Length;

        while (enemiesInWorld < 10)
        {
            Instantiate(enemy);
            enemiesInWorld++;
        }

        for (int i = 0; i < 6; i++)
        {
            if (wds[i].destroyed && ((i == enemyTargetRandom && enemyTargetRandom >= 0)
                                  || (i == enemyTargetSequential && enemyTargetRandom == -1 && !enemyTargetHero)))
            {
                wds[i].destroyed = false;
                NextWaypoint(i);
            }
        }
    }

    void NextWaypoint(int i)
    {
        // if targetting randomly, updates to next unique random
        if (enemyTargetRandom >= 0)
        {
            int res;
            do
            {
                res = Random.Range(0,5);
            } while (res == enemyTargetRandom || res == enemyTargetSequential);
            enemyTargetRandom = res;
        }

        // if targetting sequentially, updates to next in sequence
        else if (!enemyTargetHero)
        {
            enemyTargetSequential = (i+1)%6;
            Debug.Log("hello");
        }
    }
}