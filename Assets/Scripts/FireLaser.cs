using Oculus.Interaction.Input;
using UnityEngine;
using UnityEngine.Events;

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
    public Vector3 rayDirection;
    bool usingHands = true;
    public UnityEvent onShoot = new UnityEvent();
    public UnityEvent<GameObject> onHit = new UnityEvent<GameObject>();
    [SerializeField]
    private Hand hand;
    private Pose pointerTipPose;
    private Pose pointerProximalPose;
    private HandJointId pointTipID = HandJointId.HandIndexTip;
    private HandJointId pointPrxomalID = HandJointId.HandIndex1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hand.GetJointPose(pointTipID, out pointerTipPose);
        hand.GetJointPose(pointPrxomalID, out pointerProximalPose);
        if(OVRInput.GetDown(shootButton))
        {
            usingHands = false;
            rayDirection = shootingPoint.forward;
            ShootRay();
            usingHands = true;
        }
    }
    public void ShootRay()
    {
        onShoot.Invoke();
        Vector3 laserFirePoint = shootingPoint.position;
        if(usingHands)
        {
            laserFirePoint = pointerTipPose.position;
            var heading = pointerTipPose.position - pointerProximalPose.position;
            var distance = heading.magnitude;
            rayDirection = heading/distance;
        }
        Debug.Log("pew pew");
        Vector3 endpoint = Vector3.zero;
        Ray ray = new Ray(laserFirePoint, rayDirection);
        bool hitSurface = Physics.Raycast(ray, out RaycastHit hit, maxLineDistance, layermask);
        LineRenderer line = Instantiate(linePrefab);
        line.positionCount = 2;
        line.SetPosition(0, laserFirePoint);
        if(hitSurface)
        {
            Debug.Log("Hit");
            endpoint = hit.point;
            Quaternion rayImpactRotation = Quaternion.LookRotation(-hit.normal);
            GameObject rayImpact = Instantiate(rayPrefab, hit.point, rayImpactRotation);
            onHit.Invoke(hit.transform.gameObject);
            Destroy(rayImpact, impactTimer);
        }
        else
        {
            endpoint = laserFirePoint + rayDirection * maxLineDistance;
        }
        line.SetPosition(1, endpoint);
        Vector3.Lerp(line.gameObject.transform.localScale, Vector3.zero, lineShowTimer);
        Destroy(line.gameObject, lineShowTimer);
    }
}
