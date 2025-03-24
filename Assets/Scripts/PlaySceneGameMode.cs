using UnityEngine;

public class PlaySceneGameMode : MonoBehaviour
{
    public const float BOUNDARY = 7.0f;
    private const float MOVE_SPEED = 3.0f;
    private const float TURN_SPEED = 100.0f;
    private const float RADIUS = 1.0f;
    private const float ENEMY_RADIUS = 2.0f;
    private const float TIME_TO_TARGET = 2.0f;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private GameObject enemyPrefab;
    private GameObject player;
    private GameObject target;
    private GameObject enemy;

    private void Start()
    {

    }

    private void Update()
    {

    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-BOUNDARY, BOUNDARY), 0.0f, Random.Range(-BOUNDARY, BOUNDARY));
    }

    private void LinearSeek()
    {
        Vector3 direction = (target.transform.position - player.transform.position).normalized;
        Vector3 distanceToMove = direction * MOVE_SPEED * Time.deltaTime;
        player.transform.position += distanceToMove;
        Turning(direction);
    }

    private void LinearFlee()
    {
        Vector3 direction = (player.transform.position - enemy.transform.position).normalized;
        Vector3 distanceToMove = direction * MOVE_SPEED * Time.deltaTime;
        Vector3 newPosition = player.transform.position + distanceToMove;

        float x = newPosition.x;
        float z = newPosition.z;

        if (player.transform.position.x >= BOUNDARY) x = BOUNDARY;
        else if (player.transform.position.x <= -BOUNDARY) x = -BOUNDARY;
        if (player.transform.position.z >= BOUNDARY) z = BOUNDARY;
        else if (player.transform.position.z <= -BOUNDARY) z = -BOUNDARY;

        newPosition.x = x;
        newPosition.z = z;
        player.transform.position = newPosition;

        Turning(direction);
    }

    private void LinearArrive()
    {
        Vector3 distance = target.transform.position - player.transform.position;
        if (distance.magnitude > RADIUS)
        {
            distance /= TIME_TO_TARGET;
            if (distance.magnitude > MOVE_SPEED)
            {
                distance = distance.normalized * MOVE_SPEED;
            }
            Vector3 distanceToMove = distance * Time.deltaTime;
            player.transform.position += distanceToMove;
            Turning(distance);
        }
    }

    private void LinearAvoid()
    {
        float weight = 1.0f;
        float distanceToEnemy = Vector3.Distance(player.transform.position, enemy.transform.position);
        Vector3 seekVelocity = target.transform.position - player.transform.position;
        Vector3 fleeVelocity = player.transform.position - enemy.transform.position;

        if (seekVelocity.magnitude > RADIUS)
        {
            if (distanceToEnemy < ENEMY_RADIUS)
            {
                weight = Mathf.Clamp01(distanceToEnemy - RADIUS) / ENEMY_RADIUS;
            }
            float magnitute = seekVelocity.magnitude;
            seekVelocity = seekVelocity.normalized * weight;
            fleeVelocity = fleeVelocity.normalized * (1.0f - weight);

            Vector3 desiredVelocity = (seekVelocity + fleeVelocity).normalized;
            desiredVelocity *= magnitute;
            desiredVelocity /= TIME_TO_TARGET;
            
            if (desiredVelocity.magnitude > MOVE_SPEED)
            {
                desiredVelocity = desiredVelocity.normalized * MOVE_SPEED;
            }

            Vector3 distanceToMove = desiredVelocity * Time.deltaTime;
            player.transform.position += distanceToMove;
            Turning(desiredVelocity);
        }
        else
        {
            Turning(seekVelocity);
        }
    }

    private void Turning(Vector3 direction)
    {
        float currentAngle = player.transform.eulerAngles.y;
        float desiredAngle = Vector3.SignedAngle(player.transform.forward, direction, Vector3.up); // Return relative angle
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, currentAngle + desiredAngle, TURN_SPEED * Time.deltaTime); // 2 absolute angles
        player.transform.rotation = Quaternion.Euler(0.0f, newAngle, 0.0f);
    }
}
