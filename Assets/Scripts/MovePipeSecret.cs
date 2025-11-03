using UnityEngine;

public class MovePipeSecret : MonoBehaviour
{
     private float _speed = 0.75f;     // Velocidade horizontal
     private float _amplitude = 0.3f;  // Altura máxima que o cano sobe/desce
     private float _frequency = 1.5f;    // Velocidade da oscilação vertical

    private Vector3 _startPos;
    private float _offset; // deslocamento individual para cada cano

    private void Start()
    {
        _startPos = transform.position;
        _offset = Random.Range(0f, 2f * Mathf.PI); // sorteia um ponto inicial diferente para cada cano
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
        transform.position += Vector3.left * (_speed * Time.deltaTime);
    }

    private void MoveVertical()
    {
        float newY = _startPos.y + Mathf.Sin(Time.time * _frequency + _offset) * _amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}