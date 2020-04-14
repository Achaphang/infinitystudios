using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    //http://clearcutgames.net/home/?p=437 

    // Start is called before the first frame update
    void Start()
    {
       Singleton.Instance.DoSomeAwesomeStuff();
    }
    // Update is called once per frame
    void Update()
    {
    }
    // This field can be accesed through our singleton instance,
    // but it can't be set in the inspector, because we use lazy instantiation
    public int number;

    // Static singleton instance
    private static Singleton instance;

    // Static singleton property
    public static Singleton Instance
    {
        // Here we use the ?? operator, to return 'instance' if 'instance' does not equal null
        // otherwise we assign instance to a new component and return that
        get { return instance ?? (instance = new GameObject("Singleton").AddComponent<Singleton>()); }
    }

    // Instance method, this method can be accesed through the singleton instance
    public void DoSomeAwesomeStuff()
    {
        Debug.Log("Hunter Googled how to do a singlton pattern, Woohoo!");
    }
}
