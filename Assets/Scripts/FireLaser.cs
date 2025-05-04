using UnityEngine;

public class FireLaser : MonoBehaviour
{
    public LineRenderer linePrefab;
    public Transform shootingPoint;
    public float maxLineDistance = 5;
    public float lineShowTimer = 0.3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TestGesture()
    {
        Debug.Log("pew pew");
        LineRenderer line = Instantiate(linePrefab);
        line.positionCount = 2;
        line.SetPosition(0, shootingPoint.position);

        Vector3 endpoint = shootingPoint.position +shootingPoint.forward *maxLineDistance;

        line.SetPosition(1, endpoint);

        Destroy(line.gameObject, lineShowTimer);
    }
}
