using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private Vector2 mouse;
    private float angle_left;
    private float angle_right;

    public void Look(Transform hand_right, Transform hand_left)
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle_right = Mathf.Atan2(mouse.y - hand_right.position.y, mouse.x - hand_right.position.x) * Mathf.Rad2Deg;
        angle_left = Mathf.Atan2(mouse.y - hand_left.position.y, mouse.x - hand_left.position.x) * Mathf.Rad2Deg;

        if (transform.position.x < mouse.x)
        {
            hand_right.localRotation = Quaternion.Euler(0, 0, angle_right);
            hand_left.localRotation = Quaternion.Euler(0, 0, angle_left);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (transform.position.x > mouse.x)
        {
            hand_right.localRotation = Quaternion.Euler(0, 0, -(angle_right + 180f));
            hand_left.localRotation = Quaternion.Euler(0, 0, -(angle_left + 180f));
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
