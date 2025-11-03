using UnityEngine;

public class MovePipeSecret : MonoBehaviour
{
     private float _speed = 0.75f;     // Velocidade horizontal
     private float _amplitude = 0.30f;  // Altura máxima que o cano sobe/desce
     private float _frequency = 1.5f;    // Velocidade da oscilação vertical

    private Vector3 _startPos;
    private float _offset; // deslocamento individual para cada cano

    private void Start()
    {
        _startPos = transform.position;
        _offset = Random.Range(0f, 1f);
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
        // A função seno, Mathf.Sin(x), recebe um ângulo em radianos como entrada.
        transform.position = new Vector3(
            transform.position.x,
            _startPos.y + _amplitude * Mathf.Sin(Time.time * _frequency + _offset),
            transform.position.z
        );
        
    }
}