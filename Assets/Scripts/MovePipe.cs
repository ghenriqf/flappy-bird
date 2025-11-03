using UnityEngine;

public class MovePipe : MonoBehaviour
{
    [SerializeField] private float speed = 0.75f;     // Velocidade horizontal
    [SerializeField] private float amplitude = 0.3f;  // Altura máxima que o cano sobe/desce
    [SerializeField] private float frequency = 0.75f;    // Velocidade da oscilação vertical

    private Vector3 startPos;
    private float offset; // deslocamento individual para cada cano

    private void Start()
    {
        startPos = transform.position;
        offset = Random.Range(0f, 2f * Mathf.PI); // sorteia um ponto inicial diferente para cada cano
    }

    private void Update()
    {
        MoveHorizontal();
        MoveVertical();

        if (transform.position.x < -4f)
            Destroy(gameObject);
    }

    private void MoveHorizontal()
    {
        transform.position += Vector3.left * (speed * Time.deltaTime);
    }

    private void MoveVertical()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * frequency + offset) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}