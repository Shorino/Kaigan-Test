using UnityEngine;

public class Test1 : MonoBehaviour
{
    enum EngineState
    {
        Accelerate,
        Idle,
        Brake,
    }

    [Header("References")]
    public GameObject vehicle;
    public GameObject endPoint;

    [Header("Settings")]
    public float preparationTime;
    public float forwardAcceleration;
    public float brakeDeceleration;
    public float maxSpeed;

    float currentSpeed;
    float currentDistanceToTarget;
    float elapsedTime;

    private void Awake()
    {
        currentSpeed = 0;
        elapsedTime = 0;
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime < preparationTime) return;
        currentDistanceToTarget = endPoint.transform.position.x - vehicle.transform.position.x;
        EngineState engineState = DetermineEngineState(currentDistanceToTarget, currentSpeed, forwardAcceleration, brakeDeceleration, maxSpeed);
        currentSpeed = UpdateSpeed(engineState, currentSpeed, forwardAcceleration, brakeDeceleration);
        vehicle.transform.position = UpdatePosition(vehicle, currentSpeed);
    }
    EngineState DetermineEngineState(float distanceToTarget, float currentSpeed, float forwardAcceleration, float brakeDeceleration, float maxSpeed)
    {
        float brakeDistance = (0 - Mathf.Pow(currentSpeed, 2)) / (2 * -brakeDeceleration);
        if(distanceToTarget <= brakeDistance)
        {
            if(currentSpeed - (brakeDeceleration * Time.deltaTime) < 0)
            {
                return EngineState.Idle;
            }
            else
            {
                return EngineState.Brake;
            }
        }
        else
        {
            if(currentSpeed + (forwardAcceleration * Time.deltaTime) > maxSpeed)
            {
                return EngineState.Idle;
            }
            else
            {
                return EngineState.Accelerate;
            }
        }
    }
    float UpdateSpeed(EngineState engineState, float currentSpeed, float forwardAcceleration, float brakeDeceleration)
    {
        switch (engineState)
        {
            case EngineState.Accelerate:
                currentSpeed += forwardAcceleration * Time.deltaTime;
                break;
            case EngineState.Brake:
                currentSpeed -= brakeDeceleration * Time.deltaTime;
                break;
            case EngineState.Idle:
                currentSpeed = Mathf.Round(currentSpeed * 100) / 100; // round to 2 decimal places
                break;
        }
        return currentSpeed;
    }
    Vector3 UpdatePosition(GameObject vehicle, float currentSpeed)
    {
        if (currentSpeed == 0) return vehicle.transform.position;
        float x = vehicle.transform.position.x + (currentSpeed * Time.deltaTime);
        return new Vector3(x, vehicle.transform.position.y, vehicle.transform.position.z);
    }
}
