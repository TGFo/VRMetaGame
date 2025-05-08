using Meta.XR.MRUtilityKit;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGhost : MonoBehaviour
{
    public GameObject ghostPrefab;
    public GameObject destroyerPrefab;
    public float spawnTimer = 1;
    private float timer = 0;
    public float minEdgeDistance = .3f;
    public float normalOffset;
    public MRUKAnchor.SceneLabels spawnLable;
    public MRUK.SurfaceType surface;
    public int spawnTry = 1000;
    public int maxSpawnedGhostPosts = 5;
    public int spawnCount = 0;
    public RuntImeNavmeshBuilder navBuilder;
    public List<Vector3> spawnPositions = new List<Vector3>();
    public IInteractorView interactorView;
    [SerializeField]int failSafeCount = 0;
    [SerializeField]int failsafeCountMax = 1000;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(WaitForSpawn());
    }
    // Update is called once per frame
    void Update()
    {
        if(!MRUK.Instance && !MRUK.Instance.IsInitialized)
        {
            Debug.Log("No MRUK instance");
        }

        timer += Time.deltaTime;
        if(timer > spawnTimer)
        {
            if(spawnPositions.Count >= maxSpawnedGhostPosts)
            {
                SpawnGhostAtRandom();
            }
            timer = 0;
        }
    }
    public void SpawnGhostAtRandom()
    {
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();
        if(room == null) return;
        int currentTry = 0;
        while(currentTry < spawnTry)
        {
            bool hasFoundPosition = room.GenerateRandomPositionOnSurface(surface, minEdgeDistance, new LabelFilter(spawnLable), out Vector3 pos, out Vector3 norm);
            if (hasFoundPosition)
            {
                Vector3 randomPositionNormalOffset = pos + norm * normalOffset;
                randomPositionNormalOffset.y = 0;
                Mathf.FloorToInt(randomPositionNormalOffset.x);
                Mathf.FloorToInt(randomPositionNormalOffset.z);
                if(!spawnPositions.Contains(randomPositionNormalOffset))
                {
   
                    Instantiate(ghostPrefab, randomPositionNormalOffset, Quaternion.identity);
                    return;
                }
                else
                {
                    currentTry++;
                }
                
            }
            else
            {
                currentTry++;
            }
        }

    }
    public IEnumerator WaitForSpawn()
    {
        yield return new WaitForSeconds(1);
        SpawnGhostPosts();
    }
    public void SpawnGhostPosts()
    {
        Debug.Log("posts");
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();
        if (room == null) { Debug.Log("null"); return; }
        Debug.Log("room");
        while(spawnCount <= maxSpawnedGhostPosts)
        {
            bool hasFoundPosition = room.GenerateRandomPositionOnSurface(surface, minEdgeDistance, new LabelFilter(spawnLable), out Vector3 pos, out Vector3 norm);
            if(failSafeCount >= failsafeCountMax) return;
            if (hasFoundPosition)
            {
                Debug.Log("spawned");
                Vector3 randomPositionNormalOffset = pos + norm * normalOffset;
                randomPositionNormalOffset.y = 0;
                Mathf.RoundToInt(randomPositionNormalOffset.x);
                Mathf.RoundToInt(randomPositionNormalOffset.z);
                if (!spawnPositions.Contains(randomPositionNormalOffset))
                {
                    spawnPositions.Add(randomPositionNormalOffset);
                    Instantiate(destroyerPrefab, randomPositionNormalOffset, Quaternion.identity);
                    spawnCount++;
                }
                else
                {
                    failSafeCount++;
                }
                
            }
            else
            {
                failSafeCount++;
            }
        }
        navBuilder.ClearNav();
        navBuilder.BuildNav();
    }
}
