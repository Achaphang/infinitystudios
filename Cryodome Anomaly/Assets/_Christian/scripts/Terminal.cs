using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Terminal : MonoBehaviour
{
    public Keypad keypad;
    Text title;
    Text txt;

    int keypadCode;

    void Start(){
        title = transform.GetChild(0).GetChild(1).GetComponent<Text>();
        txt = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        StartCoroutine(LateStart(2));
    }

    IEnumerator LateStart(float wait) {
        yield return new WaitForSeconds(wait);
        if (keypad != null) {
            keypadCode = keypad.GetCode();
            title.text = keypad.GetTitle();
            GenerateMathPuzzle(keypad.accessLevel);
        }
    }

    // Generates a moderately difficult math problem associated with a door's passcode.
    // Difficulty ranges from 0-4
    void GenerateMathPuzzle(int difficulty) {
        string final = " = ";
        string partialVal = "????";
        int[] codeArr = keypad.GetCodeArr();

        int divisor = 11;
        int multiple = 1;

        int adder1 = 0;
        int adder2 = 0;

        if (difficulty <= 1) {
            partialVal = "?" + codeArr[1] + codeArr[2] + codeArr[3];
        } else if (difficulty == 2) {
            partialVal = "??" + codeArr[2] + codeArr[3];
        } else if (difficulty == 3) {
            partialVal = "???" + codeArr[3];
        }

        // Division problem, gives for example "14 * 621 = ?"
        // Tends to be kinda hard, easy doors should not have this problem.
        if (keypadCode >= (1000) && difficulty >= 1) {
            int modulator = 1;
            divisor = modulator;
            while(modulator < keypadCode / 6) {
                if (keypadCode % modulator == 0)
                    divisor = modulator;
                modulator++;
            }

            multiple = keypadCode / divisor;
            txt.text = divisor + " * " + multiple + final + partialVal;
        } else {
            adder1 = Random.Range(0, (int)keypadCode + 1);
            adder2 = keypadCode - adder1;
            txt.text = adder1 + " + " + adder2 + final + partialVal;
        }
    }
}
