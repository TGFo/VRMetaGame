using UnityEngine;

public class FireLaser : MonoBehaviour
{
    public LayerMask layermask;
    public LineRenderer linePrefab;
    public GameObject rayPrefab;
    public Transform shootingPoint;
    public float maxLineDistance = 5;
    public float lineShowTimer = 0.3f;
    public float impactTimer = 1;
    public OVRInput.RawButton shootButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(shootButton))
        {
            TestGesture();
        }
    }
    public void TestGesture()
    {
        Debug.Log("pew pew");
        Vector3 endpoint = Vector3.zero;
        Ray ray = new Ray(shootingPoint.position, shootingPoint.forward);
        bool hitSurface = Physics.Raycast(ray, out RaycastHit hit, maxLineDistance, layermask);
        LineRenderer line = Instantiate(linePrefab);
        line.positionCount = 2;
        line.SetPosition(0, shootingPoint.position);
        if(hitSurface)
        {
            endpoint = hit.point;
            Quaternion rayImpactRotation = Quaternion.LookRotation(-hit.normal);
            GameObject rayImpact = Instantiate(rayPrefab, hit.point, rayImpactRotation);
            Destroy(rayImpact, impactTimer);
        }
        else
        {
            endpoint = shootingPoint.position + shootingPoint.forward * maxLineDistance;
        }
        line.SetPosition(1, endpoint);
        Vector3.Lerp(line.gameObject.transform.localScale, Vector3.zero, lineShowTimer);
        Destroy(line.gameObject, lineShowTimer);
    }
}
