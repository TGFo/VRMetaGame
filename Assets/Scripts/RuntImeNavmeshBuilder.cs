using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using Meta.XR.MRUtilityKit;
using System.Collections;

public class RuntImeNavmeshBuilder : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        MRUK.Instance.RegisterSceneLoadedCallback(BuildNav);
    }

    public void BuildNav()
    {
        StartCoroutine(BuildNavRoutine());
    }
    public IEnumerator BuildNavRoutine()
    {
        yield return new WaitForEndOfFrame();
        navMeshSurface.BuildNavMesh();
    }
    public void ClearNav()
    {
        navMeshSurface.RemoveData();
    }
}
