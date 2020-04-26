using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeperThrow3D : MonoBehaviour
{
    int beepers;
    public float throwForce = 12f;
    public GameObject beeperPrefab;
    Vector3 tossBeeper;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
            beepers = GameObject.Find("3dPlayerObjs").GetComponent<PlayerController3D>().beepers;
            if (beepers > 0)
            {
                GameObject.Find("3dPlayerObjs").GetComponent<PlayerController3D>().beepers -= 1;
                ThrowBeeper();
            }

        }
    }

    public void ThrowBeeper()
    {
        GameObject beeper = Instantiate(beeperPrefab, transform.position, transform.rotation);
        Rigidbody beeperRB = beeper.GetComponent<Rigidbody>();
        beeperRB.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        StartCoroutine(BeeperBrake(beeperRB));
    }

    IEnumerator BeeperBrake(Rigidbody beeperRB)
    {
        yield return new WaitForSeconds(3);
        beeperRB.isKinematic = true;
        beeperRB.collisionDetectionMode = CollisionDetectionMode.Discrete;
    }
}
