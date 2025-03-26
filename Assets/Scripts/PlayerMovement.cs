using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float MOVE_SPEED = 3.0f;
    private const float TURN_SPEED = 300.0f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();    
    }

    private void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalMovement, 0.0f, verticalMovement).normalized;
        Vector3 distanceToMove = MOVE_SPEED * direction - rb.linearVelocity;

        rb.AddForce(distanceToMove);

        if (direction == Vector3.zero) return;
        Turning(direction);
    }

    private void Turning(Vector3 direction)
    {
        float currentAngle = gameObject.transform.eulerAngles.y;
        float desiredAngle = Vector3.SignedAngle(gameObject.transform.forward, direction, Vector3.up); // Return relative angle
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, currentAngle + desiredAngle, TURN_SPEED * Time.deltaTime); // 2 absolute angles
        gameObject.transform.rotation = Quaternion.Euler(0.0f, newAngle, 0.0f);
    }
}
