using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnInterval = 3f;
    void Start()
    {
        InvokeRepeating(nameof(SpawnPipes),2f,spawnInterval);
    }
    
    void SpawnPipes()
    {
       Vector3 newPos = new  Vector3(transform.position.x,Random.Range(-2,3),transform.position.z);
       
       Instantiate(pipePrefab, newPos, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
