using System.Collections;
using UnityEngine;

public class MosquitoAI : MonoBehaviour
{
    public Transform player; // Referência ao XR Origin (Player)
    public float minDistance = 1.0f;
    public float maxDistance = 3.0f;
    public float minHeight = 0.5f;
    public float maxHeight = 2.0f;
    public float speed = 1.5f;
    public float heightChangeSpeed = 0.5f;
    public float rotationSpeed = 30f;
    private float currentDistance;
    private float targetDistance;
    private float currentHeight;
    private float targetHeight;
    private float angle;
    private int direction = 1; // 1 para horário, -1 para anti-horário

    void Start()
    {
        currentDistance = Random.Range(minDistance, maxDistance);
        targetDistance = currentDistance;
        currentHeight = Random.Range(minHeight, maxHeight);
        targetHeight = currentHeight;
        angle = Random.Range(0f, 360f);
        StartCoroutine(ChangeDirectionAndDistance());
    }

    void Update()
    {
        if (player == null) return;

        // Atualiza distância e altura suavemente
        currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * speed);
        currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * heightChangeSpeed);

        // Suaviza a rotação ao redor do jogador
        angle += direction * rotationSpeed * Time.deltaTime;
        if (angle > 360f) angle -= 360f;
        if (angle < 0f) angle += 360f;

        float radians = angle * Mathf.Deg2Rad;
        Vector3 newPosition = player.position + new Vector3(Mathf.Cos(radians) * currentDistance, currentHeight, Mathf.Sin(radians) * currentDistance);
        transform.position = newPosition;
    }

    IEnumerator ChangeDirectionAndDistance()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 7f));

            // Alterna suavemente a direção
            direction = (Random.value > 0.5f) ? 1 : -1;

            // Define novos alvos para distância e altura
            targetDistance = Random.Range(minDistance, maxDistance);
            targetHeight = Random.Range(minHeight, maxHeight);
        }
    }
}
