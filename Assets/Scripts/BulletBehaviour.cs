using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private float onScreenDelay = 3f;

    private void Start()
    {
        Destroy(this.gameObject, onScreenDelay);
    }
}
