using Meta.XR.MRUtilityKit;
using UnityEngine;

public class SpawnGhost : MonoBehaviour
{
    public GameObject ghostPrefab;
    public float spawnTimer = 1;
    private float timer = 0;
    public float minEdgeDistance = .3f;
    public float normalOffset;
    public MRUKAnchor.SceneLabels spawnLable;
    public MRUK.SurfaceType surface;
    public int spawnTry = 1000;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            SpawnGhostAtRandom();
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

                Instantiate(ghostPrefab, randomPositionNormalOffset, Quaternion.identity);
                return;
            }
            else
            {
                currentTry++;
            }
        }

    }
}
