using UnityEngine;

public class Test2 : MonoBehaviour
{

    bool CalculateInterceptPosition(Vector3 selfPosition, Vector3 selfVelocity, Vector3 targetPosition, 
        Vector3 targetVelocity, float bulletSpeed, out Vector3 interceptPosition)
    {
        interceptPosition = new Vector3(0, 0, 0);
        return true;
    }
}
