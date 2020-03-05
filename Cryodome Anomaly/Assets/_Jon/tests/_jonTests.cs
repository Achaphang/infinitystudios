using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

// TODO: figure out what the heck tests are in Unity. I am confused as heckle
// Also integrate the following code.
public class _jonTests {
    
    // Goal: generate tons of pathfinding markers and see if the monster glitches out.
    // Pass: continues pathfinding. Check if monster is moving.
    // Fail: anything else? idk. Monster not moving?
    void SpawnMassMarkers() {
        MonsterController monster = GameObject.Find("Monster").GetComponent<MonsterController>();
        for(int i = 0; i < 999; i++) {
            monster.AddTarget(new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
        }
    }

    // Goal: play tons of noises and see if the game breaks.
    // Pass: the game does not break. Basically check sound system and framerate is ok.
    // Fail: the game breaks. Wow.
    void PlayNoises() {
        MonsterNoiseController monster = GameObject.Find("Monster").GetComponent<MonsterNoiseController>();
        for(int i = 0; i < 999; i++) {
            monster.commitDie();
            monster.locatedPlayer();
        }
    }

    // Goal: Spawn tons of markers directly on top of the monster. They should all immediately be deleted.
    // Pass: No markers remain after a set amount of time.
    // Fail: At least one marker is still in the scene.
    void MassMarkerPlaceAtMe() {
        MonsterController monster = GameObject.Find("Monster").GetComponent<MonsterController>();
        int temp = monster.GetTargetCount();
        for (int i = 0; i < 999; i++) {
            monster.AddTarget(monster.gameObject.transform.position);
        }

        // Todo: incorporate Assert.Fail()
        if (temp != monster.GetTargetCount())
            return;
            // Assert.Fail();
    }
}
