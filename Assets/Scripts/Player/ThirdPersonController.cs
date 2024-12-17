using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonController : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения
    public float sprintSpeed = 10f; // Скорость ускорения
    public float jumpForce = 5f; // Сила прыжка
    public KeyCode jumpKey = KeyCode.Space; // Клавиша для прыжка
    public KeyCode sprintKey = KeyCode.LeftShift; // Клавиша для ускорения

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Запрещаем вращение под действием физики
    }

    private void Update()
    {
        // Проверка на земле
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f); // Проверяем, находится ли персонаж на земле

        // Получаем входные данные
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Получаем направление камеры
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Убираем вертикальную составляющую направления
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Определяем движение
        Vector3 move = (forward * moveVertical + right * moveHorizontal).normalized;
        float currentSpeed = Input.GetKey(sprintKey) ? sprintSpeed : moveSpeed;

        // Двигаем персонажа
        if (move.magnitude > 0)
        {
            rb.MovePosition(rb.position + move * currentSpeed * Time.deltaTime);
            // Поворачиваем персонажа в сторону движения
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Плавный поворот
        }

        // Прыжок
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Применяем силу прыжка
        }
    }
}