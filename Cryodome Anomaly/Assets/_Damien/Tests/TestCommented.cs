/** Damien's Tests
         **
        [UnityTest]
        public IEnumerator PlayerFallCheck()
        {
            SetupScene();
            GameObject playerLoc = MonoBehaviour.Instantiate(Resources.Load<GameObject>("PlayerTesting"));
            float initPosY = playerLoc.transform.position.y;
            yield return new WaitForSecondsRealtime(8);
            if(playerLoc.transform.position.y != initPosY)
            {
                Debug.Log("The test failed");
                Assert.Fail();
            }
            else
            {
                Debug.Log("The test didn't fail");
            }
            yield return new WaitForSecondsRealtime(4);

            UnloadPrevScene();
            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayerMapFallCheck()
        {
            SetupScene();
            float xSpeed = 2f;
            yield return new WaitForSecondsRealtime(12);
            GameObject playerLoc = MonoBehaviour.Instantiate(Resources.Load<GameObject>("PlayerTesting"));
            playerLoc.GetComponent<Rigidbody>().velocity = new Vector3(xSpeed, 0, 0);
            for (int i = 1; i < 10; i++)
            {
                yield return new WaitForSecondsRealtime(4);
                if(playerLoc.transform.position.y < 0)
                {
                    Debug.Log("The test failed");
                    Assert.Fail();
                    yield break;
                }
                else
                {
                    Debug.Log("Test hasn't failed yet");
                }

            }
         

            UnloadPrevScene();
            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayerStressSpawn()
        {
            SetupScene();
            yield return new WaitForSeconds(5);
            var fps = 1 / Time.deltaTime;
            for (int i = 0; i < 5; i++)
            {
                fps = 1 / Time.deltaTime;
                for (int j = 0; j < 10; j++)
                {
                    MonoBehaviour.Instantiate(Resources.Load<GameObject>("PlayerTesting"));
                }
                Debug.Log(fps);
                yield return new WaitForSeconds(3);
                if (fps < 15)
                {
                    // if able to instantiate 50 object success
                    Debug.Log((i + 1) * 10);
                    if (i < 10)
                    {
                        Assert.Fail();
                        Debug.Log("Test Failed");
                    }
                    else
                    {
                        Debug.Log("Test has not yet failed.");
                    }
                    yield break;
                }
            }
            UnloadPrevScene();
            yield return null;
        }

        // **/