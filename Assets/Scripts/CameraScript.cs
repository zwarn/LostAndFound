using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject target;
    public float deadZone = 2;
    public float cameraSpeed = 10;

    void Update()
    {
        var delta = getDelta();
        if (delta.magnitude > deadZone)
        {
            setXY(getXY(gameObject) +
                  delta * (Mathf.Clamp(delta.magnitude * 0.1f, 1, 10) * cameraSpeed * Time.deltaTime));
        }
    }

    private Vector2 getXY(GameObject gameObject)
    {
        return new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    }

    private void setXY(Vector2 position)
    {
        Vector3 worldPosition = transform.position;
        worldPosition.x = position.x;
        worldPosition.y = position.y;
        transform.position = worldPosition;
    }

    private Vector2 getDelta()
    {
        Vector2 targetPosition = getXY(target);
        Vector2 currentPosition = getXY(gameObject);

        return targetPosition - currentPosition;
    }
}