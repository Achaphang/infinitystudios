using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Stimpack functionality.
 * Stimpack value reflects the number
 * of seconds that regen speed is increased.
 * Dynimcally bound function used by
 * SuperStimpack
 */
public class Stimpack : MonoBehaviour
{
    public virtual float GetStimpackValue() {
        return 20.0F;
    }
}
