using UnityEngine;

public class Test2_Old : MonoBehaviour
{
    public bool enable;

    private void Awake()
    {
        if (!enable) return;
        var targetPosition = new Vector3(1, 4, 0);
        var targetVelocity = new Vector3(4, -2, 0);
        var bulletPosition = new Vector3(2, -1, 0);
        float bulletSpeed = 4.242640687119285f;
        var interceptPosition = Vector3.zero;

        var intercept = CalculateInterceptPosition(bulletPosition, Vector3.zero, targetPosition, targetVelocity, bulletSpeed, out interceptPosition);
        Debug.Log("intercept: " + intercept);
        Debug.Log("interceptPosition: " + interceptPosition);
        // need more time to verify...
    }
    Vector3 CalculateBulletVelocity(Vector3 targetPosition, Vector3 targetVelocity, Vector3 bulletPosition, float bulletSpeed, out bool status)
    {
        status = true;
        float a = targetPosition.x;
        float b = targetPosition.y;
        float c = targetPosition.z;
        float d = bulletPosition.x;
        float e = bulletPosition.y;
        float f = bulletPosition.z;
        float j = targetVelocity.x;
        float k = targetVelocity.y;
        float l = targetVelocity.z;
        float m = bulletSpeed;
        float a2 = Mathf.Pow(a, 2);
        float b2 = Mathf.Pow(b, 2);
        float c2 = Mathf.Pow(c, 2);
        float d2 = Mathf.Pow(d, 2);
        float e2 = Mathf.Pow(e, 2);
        float f2 = Mathf.Pow(f, 2);
        float j2 = Mathf.Pow(j, 2);
        float k2 = Mathf.Pow(k, 2);
        float l2 = Mathf.Pow(l, 2);
        float m2 = Mathf.Pow(m, 2);

        float gSqrt = (f2 - 2 * c * f + e2 - 2 * b * e + d2 - 2 * a * d + c2 + b2 + a2) * m2 + ((-e2) + 2 * b * e - d2 + 2 * a * d - b2 * a2) * l2 + (((2 * e - 2 * b) * f - 2 * c * e + 2 * b * c) * k + ((2 * d - 2 * a) * f - 2 * c * d + 2 * a * c) * j) * l + ((-f2) + 2 * c * f - d2 + 2 * a * d - c2 - a2) * k2 + ((2 * d - 2 * a) * e - 2 * b * d + 2 * a * b) * j * k + ((-f2) + 2 * c * f - e2 + 2 * b * e - c2 - b2) * j2;
        if(gSqrt < 0) status = false;
        float g = (d - a) * Mathf.Sqrt(Mathf.Abs(gSqrt)) + ((a - d) * f + c * d - a * c) * l + ((a - d) * e + b * d - a * b) * k + (f2 - 2 * c * f + e2 - 2 * b * e + c2 + b2) * j;
        float gDeno = f2 - 2 * c * f + e2 - 2 * b * e + d2 - 2 * a * d + c2 + b2 + a2;
        if (gDeno == 0)
        {
            gDeno = 1e-10f;
            status = false;
        }
        g /= gDeno;
        float g2 = Mathf.Pow(g, 2);

        float hSqrt = (f2 - 2 * c * f + e2 - 2 * b * e + c2 + b2) * m2 + ((-e2) + 2 * b * e - b2) * l2 + ((2 * e - 2 * b) * f - 2 * c * e + 2 * b * c) * k * l + ((-f2) + 2 * c * f - c2) * k2 + ((-f2) + 2 * c * f - e2 + 2 * b * e - c2 - b2) * g2;
        if (hSqrt < 0) status = false;
        float h = (e - b) * Mathf.Sqrt(Mathf.Abs(hSqrt)) + ((b - e) * f + c * e - b * c) * l + (f2 - 2 * c * f + c2) * k;
        float hDeno = f2 - 2 * c * f + e2 - 2 * b * e + c2 + b2;
        if (hDeno == 0)
        {
            hDeno = 1e-10f;
            status = false;
        }
        h /= hDeno;
        float h2 = Mathf.Pow(h, 2);

        float i = Mathf.Sqrt(Mathf.Abs(m2 * g2 * h2));

        return new Vector3(g, h, i);
    }
    bool CalculateInterceptPosition(Vector3 selfPosition, Vector3 selfVelocity, Vector3 targetPosition, 
        Vector3 targetVelocity, float bulletSpeed, out Vector3 interceptPosition)
    {
        bool intercept;
        Vector3 bulletPosition = selfPosition;
        Vector3 bulletVelocity = CalculateBulletVelocity(targetPosition, targetVelocity, bulletPosition, bulletSpeed, out intercept);
        float scalar = targetPosition.x - bulletPosition.x;
        float scalarDeno = bulletVelocity.x - targetVelocity.x;
        if (scalarDeno == 0)
        {
            scalarDeno = 1e-10f;
            intercept = false;
        }
        scalar /= scalarDeno;
        interceptPosition = targetPosition + (targetVelocity * scalar);
        return intercept;
    }
}
