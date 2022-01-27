using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DF_DamageIndicatorController : MonoBehaviour
{
    float moveStart = 0f, moveFinish = 1f;
    private void Update()
    {
        if (moveStart < moveFinish)
        {
            moveStart += (moveStart + Time.deltaTime) * 0.1f;
            transform.position = transform.position + new Vector3(0, 0.02f, 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
