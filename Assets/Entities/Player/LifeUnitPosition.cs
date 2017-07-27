using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUnitPosition : MonoBehaviour {

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 0.3f);
    }
}
