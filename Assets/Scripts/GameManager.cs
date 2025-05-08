using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int playerHealth;
    public int playerMaxHealth;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
