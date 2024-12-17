using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // Ссылка на игрока
    public float distance = 5f; // Расстояние до игрока
    public float height = 2f; // Высота камеры
    public float damping = 5f; // Задержка движения камеры

    private void LateUpdate()
    {
        Vector3 targetPosition = player.position + Vector3.up * height - player.forward * distance;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * damping);
        transform.LookAt(player.position + Vector3.up * height); // Смотрим на игрока
    }
}