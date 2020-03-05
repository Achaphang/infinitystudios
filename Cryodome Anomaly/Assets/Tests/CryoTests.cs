using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class CryoTests
    {
        /** Damien's Tests
         **/
        [UnityTest]
        public IEnumerator PlayerBoundsCheck()
        {
            //SetupScene();
            yield return new WaitForSeconds(5);
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator PlayerPickupBoundsCheck()
        {
            //SetupScene();
            yield return new WaitForSeconds(5);
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayerStressSpawn()
        {
            //SetupScene();
            yield return new WaitForSeconds(5);
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        // **/

        /** Jon's Tests
          **/
        [UnityTest]
        public IEnumerator MonsterBoundsCheck()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
        /*
        [UnityTest]
        public IEnumerator SpawnMassMarkers() {
            MonsterController monster = GameObject.Find("Monster").GetComponent<MonsterController>();
            for (int i = 0; i < 999; i++) {
                monster.AddTarget(new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
            }
        }
        [UnityTest]
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
        [UnityTest]
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
        */
        

        // **/

        /** Hunter's Tests
         **/
        [UnityTest]
        public IEnumerator StressSpawn()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        // **/

        /** Tobias's Tests
         **/
        [UnityTest]
        public IEnumerator MenuInputTest()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        // **/

        /** Christian's Tests
         **/
        [UnityTest]
        public IEnumerator RandomNumGeneratorTest()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        // **/

        // External Functions(Comment your portions if you try to use this like the above Tests
        public void SetupScene()
        {
            SceneManager.LoadScene("main");
        }


    }
}
