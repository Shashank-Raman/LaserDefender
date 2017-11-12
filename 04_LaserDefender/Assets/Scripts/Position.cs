using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(new Vector3(0, 0, 0), 1);
    }
}
