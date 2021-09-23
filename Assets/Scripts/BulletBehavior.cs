using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public bool exists = true;
    void Update()
    {
        if (!exists)
        {
            Destroy(gameObject);
        }
    }
}
