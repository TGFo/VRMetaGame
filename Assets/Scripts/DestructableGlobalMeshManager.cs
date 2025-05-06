using UnityEngine;
using Meta.XR.MRUtilityKit;
using System.Collections.Generic;

public class DestructableGlobalMeshManager : MonoBehaviour
{
    public DestructibleGlobalMeshSpawner meshSpawner;
    private DestructibleMeshComponent currentComponent;
    List<GameObject> segments = new List<GameObject>();
    public FireLaser fireLaser;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshSpawner.OnDestructibleMeshCreated.AddListener(SetUpDestructableComponents);
        fireLaser.onHit.AddListener(DestroyMeshSegment);
    }

    public void SetUpDestructableComponents(DestructibleMeshComponent component)
    {
        currentComponent = component;

        component.GetDestructibleMeshSegments(segments);
        foreach(GameObject item in segments)
        {
            item.AddComponent<MeshCollider>();
            item.layer = 6;
        }
    }
    public void DestroyMeshSegment(GameObject segment)
    {
        if(!segments.Contains(segment)) return;
        currentComponent.DestroySegment(segment);
    }
}
