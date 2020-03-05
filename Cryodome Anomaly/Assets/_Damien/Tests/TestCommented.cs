/** Damien's Tests
  **
 [UnityTest]
 public IEnumerator PlayerBoundsCheck()
 {
     UnloadPrevScene();
     yield return new WaitForSeconds(5);
     UnloadPrevScene();
     yield return new WaitForSecondsRealtime(5);
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