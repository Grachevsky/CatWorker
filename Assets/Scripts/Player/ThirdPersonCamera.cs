using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player; // Ссылка на игрока
    public float distance = 5f; // Расстояние до игрока
    public float height = 2f; // Высота камеры
    public float damping = 5f; // Задержка движения камеры
    public float rotationSpeed = 5f; // Скорость вращения камеры
    public float zoomSpeed = 2f; // Скорость приближения/отдаления
    public float minDistance = 2f; // Минимальное расстояние до игрока
    public float maxDistance = 10f; // Максимальное расстояние до игрока

    private float currentAngle = 0f; // Текущий угол вращения камеры

    private void LateUpdate()
    {
        // Проверяем, удерживается ли правая кнопка мыши
        if (Input.GetMouseButton(1)) // 1 - правая кнопка мыши
        {
            // Получаем ввод мыши
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            currentAngle += mouseX; // Обновляем текущий угол на основании движения мыши
        }

        // Обработка приближения и отдаления
        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        distance = Mathf.Clamp(distance - scroll, minDistance, maxDistance); // Ограничиваем расстояние

        // Вычисляем позицию камеры
        Vector3 targetPosition = player.position + Vector3.up * height - Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;

        // Перемещаем камеру к новому положению с задержкой
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * damping);
        transform.LookAt(player.position + Vector3.up * height); // Смотрим на игрока
    }
}