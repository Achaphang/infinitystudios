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
