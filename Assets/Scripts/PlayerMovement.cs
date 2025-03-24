using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float MOVE_SPEED = 3.0f;
    private const float TURN_SPEED = 300.0f;

    private void Update()
    {
        // Calculating direction
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalMovement, 0.0f, verticalMovement).normalized;
        if (direction == Vector3.zero) return;

        Vector3 distanceToMove = MOVE_SPEED * Time.deltaTime * direction;
        Vector3 newPosition = gameObject.transform.position + distanceToMove;

        // Clamping position within map's boundary
        float x = newPosition.x;
        float z = newPosition.z;

        if (gameObject.transform.position.x > PlaySceneGameMode.BOUNDARY) x = PlaySceneGameMode.BOUNDARY;
        else if (gameObject.transform.position.x < -PlaySceneGameMode.BOUNDARY) x = -PlaySceneGameMode.BOUNDARY;
        if (gameObject.transform.position.z > PlaySceneGameMode.BOUNDARY) z = PlaySceneGameMode.BOUNDARY;
        else if (gameObject.transform.position.z < -PlaySceneGameMode.BOUNDARY) z = -PlaySceneGameMode.BOUNDARY;

        newPosition.x = x;
        newPosition.z = z;

        gameObject.transform.position = newPosition;
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
