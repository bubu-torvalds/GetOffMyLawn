using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour {

    public GameObject lifeUnitPrefab;
    public GameObject[] lifeBarPrefabs;


    void Start() {
        int index = 0;
        foreach (Transform child in transform) {
            Debug.Log(child.transform.position.x);
            GameObject lifeUnit = Instantiate(lifeUnitPrefab, child.position, Quaternion.identity) as GameObject;
            lifeUnit.transform.parent = child;
            lifeBarPrefabs[index] = lifeUnit;
            index++;
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(5, 1, 0));
    }

    public void LoseLife() {
        int size = 0;
        foreach(GameObject lifeUnit in lifeBarPrefabs) {
            if (lifeUnit != null) {
                size++;
            }
        }
        Destroy(lifeBarPrefabs[size - 1]);

    }

}
