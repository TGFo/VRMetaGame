using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int playerHealth;
    public int playerMaxHealth;
    public int score;
    public bool gameOver = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //Destroy(gameObject);
        }
    }
    public void AttackPlayer(int damage)
    {
        playerHealth -= damage;
    }
    public void IncreaseScore()
    {
        score++;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth <= 0)
        {
            gameOver = true;
        }
    }
}
