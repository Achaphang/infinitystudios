using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerController : MonoBehaviour
{
    GameObject monsterParent;

    public GameObject GetMonster() {
        return monsterParent;
    }

    public void SetMonster(GameObject m) {
        monsterParent = m;
    }
}
