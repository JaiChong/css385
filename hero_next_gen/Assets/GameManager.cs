using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    public Text textElement;
    public string textValue;
    public struct GameManagerData
    {
        // Used by Enemy.cs
        //public bool enemiesFollowing;

        // Prints
        public int eggsInWorld;             // "Graded based on proper application status echo of number of eggs currently spawned", updated in Hero.cs
        public int enemiesInWorld;          // should always be 10; updated here
        public int enemiesDestroyed;        // updated in Enemy.cs
        public int collisionsWithEnemies;   // number of times hero has touched an enemy, updated in Hero.cs
        public int heroDeaths;
    }
    public GameManagerData gmd = new GameManagerData();

    void Start()
    {
        // Initialize text
        textValue = " Eggs in World:                0\n" +
                    " Enemies in World:          0\n" +
                    " Enemies Destroyed:       0\n" +
                    " Collisions with Enemies: 0\n" +
                    " Hero Deaths:                  0";
        textElement.text = textValue;

        // Initializes struct values
        //gmd.enemiesFollowing = false;
        gmd.eggsInWorld = 0;
        gmd.enemiesInWorld = 0;
        gmd.enemiesDestroyed = 0;
        gmd.collisionsWithEnemies = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            Application.Quit();
        }
        
        textValue = " Eggs in World:                " + gmd.eggsInWorld + "\n" +
                    " Enemies in World:          " + gmd.enemiesInWorld + "\n" +
                    " Enemies Destroyed:       " + gmd.enemiesDestroyed + "\n" +
                    " Collisions with Enemies: " + gmd.collisionsWithEnemies + "\n" +
                    " Hero Deaths:                  " + gmd.heroDeaths;
        textElement.text = textValue;

        gmd.eggsInWorld = GameObject.FindGameObjectsWithTag("Egg").Length;
        gmd.enemiesInWorld = GameObject.FindGameObjectsWithTag("Enemy").Length;

        while (gmd.enemiesInWorld < 10)
        {
            Instantiate(enemy);
            gmd.enemiesInWorld++;
        }
    }
}