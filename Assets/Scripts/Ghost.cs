using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    public NavMeshAgent agent;
    public float speed = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine(ResetNavMeshAgent());
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.enabled == false)return;
        Vector3 targetPosition = Camera.main.transform.position;
        targetPosition.y = 0;
        agent.SetDestination(targetPosition);
        agent.speed = speed;
        if(Vector3.Distance(transform.position, targetPosition) < .5)
        {
            Destroy(gameObject);
        }
    }
    public IEnumerator ResetNavMeshAgent()
    {
        agent.enabled= false;
        yield return new WaitForSeconds(1);
        agent.enabled = true;
    }
}
