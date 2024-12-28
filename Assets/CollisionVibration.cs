using UnityEngine;
using Bhaptics.SDK2;
public class CollisionVibration : MonoBehaviour
{
    enum Direction
    {
        Front, FrontRight, FrontLeft, Back, BackRight, BackLeft
    }
    public float detectionDistance = 5f; 

    void Update()
    {
        DetectWalls();
    }

    void DetectWalls()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, detectionDistance);

        foreach (var obj in objectsInRange)
        {
            if (obj.CompareTag("Wall")) 
            {
                Vector3 directionToWall = (obj.transform.position - transform.position).normalized;
                float angle = Vector3.SignedAngle(transform.forward, directionToWall, Vector3.up);
                float distance = Vector3.Distance(transform.position, obj.transform.position);

                Direction direction = DetermineDirection(angle);
                Debug.Log($"Wall detected at {direction}");
                Vibrate(direction, distance);
            }
        }
    }

    Direction DetermineDirection(float angle)
    {
        if (angle > -30 && angle <= 30)
            return Direction.Front;
        else if (angle > 30 && angle <= 90)
            return Direction.FrontRight;
        else if (angle > 90 && angle <= 150)
            return Direction.BackRight;
        else if (angle > -90 && angle <= -30)
            return Direction.FrontLeft;
        else if (angle > -150 && angle <= -90)
            return Direction.BackLeft;
        else
            return Direction.Back;
    }

    void Vibrate(Direction direction, float distance)
    {
        // moze jakos inacvzej okreslac
        float intensity = distance / detectionDistance;
        switch (direction)
        {
            case Direction.Front:
                // (event, intensity)
                BhapticsLibrary.PlayParam(BhapticsEvent.COLLISION1_1, intensity);
                break;
            case Direction.FrontLeft:
                break;
            case Direction.FrontRight:
                break;
            case Direction.Back:
                break;
            case Direction.BackRight:
                break;
            case Direction.BackLeft:
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
    }
}
