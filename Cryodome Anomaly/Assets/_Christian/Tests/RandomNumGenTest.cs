/** Christian's Tests
        [UnityTest]
        public IEnumerator RandomNumGeneratorTest()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            int[] passcode = new int[4];
            for (int i = 0; i < 4; i ++) {
                passcode[i] = -1;
            }
            for (int i = 0; i < 4; i ++) {
                passcode[i] = UnityEngine.Random.Range(0, 9);
            }
            for (int i = 0; i < 4; i ++) {
                if (passcode[i] < 0 || passcode[i] > 9) {
                    Assert.Fail();
                    yield break;
                }
            }
            yield return null;
        }**/