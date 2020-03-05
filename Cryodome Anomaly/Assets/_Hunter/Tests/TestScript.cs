using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;



namespace Tests
{
    public class CryoTests
    {
        /** Hunter's Tests
         **/
       /*[CryoTests]
        public IEnumerator StressSpawn()
        {
            var time = 1 / Time.deltaTime;
            for(int i =0; i < 100; i++)
            {
                time = 1 / Time.deltaTime;
                for(int j=0; j < 100; j++)
                {
                    MonoBehaviour.Instantiate(Resources.Load<GameObject>("Flashlight"));
                }
                Debug.Log(time);
                yield return new WaitForSeconds(1);
                if(time < 15)
                {
                    Debug.Log((i + 1) * 100);
                    if(i < 10)
                    {
                        Assert.Fail();
                    }
                    yield break;
                }
            }
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            //call Assert.Fail() if test fails
            yield return null;
        }

    }
    */
}