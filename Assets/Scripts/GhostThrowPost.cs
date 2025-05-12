using UnityEngine;

public class GhostThrowPost : MonoBehaviour
{
    public string enemyTag;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(enemyTag))
        {
            Debug.Log("Destroy");
            GameManager.instance.IncreaseScore();
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(enemyTag))
        {
            Debug.Log("Destroy");
            GameManager.instance.IncreaseScore();
            Destroy(other.gameObject);
        }
    }
}
