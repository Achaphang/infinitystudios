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
public IEnumerator PlayerVelocityBoundsCheck()
{
    SetupScene();
    yield return new WaitForSecondsRealtime(5);


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