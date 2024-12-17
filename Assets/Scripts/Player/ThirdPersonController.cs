using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения
    public float sprintSpeed = 10f; // Скорость ускорения
    public float jumpForce = 5f; // Сила прыжка
    public KeyCode jumpKey = KeyCode.Space; // Клавиша для прыжка
    public KeyCode sprintKey = KeyCode.LeftShift; // Клавиша для ускорения

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0; // Сбрасываем вертикальную скорость, если на земле
        }

        // Получаем входные данные
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Определяем скорость движения
        Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;
        float currentSpeed = Input.GetKey(sprintKey) ? sprintSpeed : moveSpeed;

        controller.Move(move * currentSpeed * Time.deltaTime);

        // Прыжок
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
        }

        // Применяем гравитацию
        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}