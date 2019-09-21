using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsManager : MonoBehaviour
{
    public Vector3 posOnScreen;

    public void CalculateBounds(Vector3 objectTransformPosition, float leftOffset, float rightOffset, float bottomOffset, float topOffset)
    {
        posOnScreen = Camera.main.WorldToViewportPoint(objectTransformPosition);
        posOnScreen.x = Mathf.Clamp(posOnScreen.x, leftOffset, rightOffset);
        posOnScreen.y = Mathf.Clamp(posOnScreen.y, bottomOffset, topOffset);
    }
}
