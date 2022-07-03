using UnityEngine;

public class Test2 : MonoBehaviour
{
    public bool enable;

    private void Awake()
    {
        if (!enable) return;
        var targetPosition = new Vector3(1, 4, 0);
        var targetVelocity = new Vector3(2, -1, 0);
        var bulletPosition = new Vector3(2, -1, 0);
        float bulletSpeed = 3f;
        var interceptPosition = Vector3.zero;

        var intercept = CalculateInterceptPosition(bulletPosition, Vector3.zero, targetPosition, targetVelocity, bulletSpeed, out interceptPosition);
        Debug.Log("intercept: " + intercept);
        Debug.Log("interceptPosition: " + interceptPosition);
    }
    float Magnitude(Vector3 vector)
    {
        return Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2));
    }
    float SmallestWhichIsntNegativeOrNan(float a, float b)
    {
        if (isNanOrNegative(a))
        {
            if (isNanOrNegative(b))
            {
                return float.NaN;
            }
            else
            {
                return b;
            }
        }
        else
        {
            if (isNanOrNegative(b))
            {
                return a;
            }
            else
            {
                return Mathf.Min(a, b);
            }
        }
    }
    bool isNanOrNegative(float number)
    {
        if (number < 0 || float.IsNaN(number)) return true;
        return false;
    }
    bool CalculateInterceptPosition(Vector3 selfPosition, Vector3 selfVelocity, Vector3 targetPosition,
        Vector3 targetVelocity, float bulletSpeed, out Vector3 interceptPosition)
    {
        bool canHit = true;
        float targetSpeed = Magnitude(targetVelocity);
        Vector3 targetVector = targetVelocity / targetSpeed;
        Vector3 bulletPosition = selfPosition;

        float a = (targetVector.x * targetVector.x) + (targetVector.y * targetVector.y) + (targetVector.z * targetVector.z) - (bulletSpeed * bulletSpeed);
        float b = 2 * ((targetPosition.x * targetVector.x) + (targetPosition.y * targetVector.y) + (targetPosition.z * targetVector.z) - (bulletPosition.x * targetVector.x) - (bulletPosition.y * targetVector.y) - (bulletPosition.z * targetVector.z));
        float c = (targetPosition.x * targetPosition.x) + (targetPosition.y * targetPosition.y) + (targetPosition.z * targetPosition.z) + (bulletPosition.x * bulletPosition.x) + (bulletPosition.y * bulletPosition.y) + (bulletPosition.z * bulletPosition.z) - (2 * bulletPosition.x * bulletPosition.x) - (2 * bulletPosition.y * bulletPosition.y) - (2 * bulletPosition.z * bulletPosition.z);

        float t1 = (-b + Mathf.Sqrt((b * b) - (4 * a * c))) / (2 * a);
        float t2 = (-b - Mathf.Sqrt((b * b) - (4 * a * c))) / (2 * a);
        float t = SmallestWhichIsntNegativeOrNan(t1, t2);
        if (isNanOrNegative(t)) canHit = false;
        interceptPosition = (targetPosition - bulletPosition + (t * targetSpeed * targetVector)) / (t * bulletSpeed);
        return canHit;
    }
}
