using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private float maxTime = 1.5f;
    [SerializeField] private float heightRange = 0.50f;
    [SerializeField] private GameObject pipe;
    private float _timer;
    private void Start()
    {
        SpawnPipes();
    }
    
    private void SpawnPipes()
    {
        var spawnPos = transform.position + new Vector3(0, Random.Range(-heightRange, heightRange));
        var pipeClone = Instantiate(pipe, spawnPos, Quaternion.identity);

        Destroy(pipeClone, 10f);
    }

    private void Update()
    {
        if (_timer > maxTime)
        {
            SpawnPipes();
            _timer = 0;
        }

        _timer += Time.deltaTime;
    }
}
