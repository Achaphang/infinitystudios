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
    void GenerateMathPuzzle(int difficultyInitial) {
        int difficulty = difficultyInitial;
        difficulty = 5;
        if (Globals.Instance != null)
            if (Globals.Instance.difficulty != -1)
                difficulty = difficulty - (2 - Globals.Instance.difficulty);

        string final = " = ";
        string partialVal = "????";
        int[] codeArr = keypad.GetCodeArr();

        int divisor = 11;
        int multiple = 1;

        int adder1 = 0;
        int adder2 = 0;

        if(difficulty <= 0) {
            partialVal = "" + codeArr[0] + codeArr[1] + codeArr[2] + codeArr[3];
        } else if (difficulty == 1) {
            partialVal = "?" + codeArr[1] + codeArr[2] + codeArr[3];
        } else if (difficulty == 2) {
            partialVal = "??" + codeArr[2] + codeArr[3];
        } else if (difficulty == 3) {
            partialVal = "???" + codeArr[3];
        }

        // BC's favorite number
        if (keypadCode == 513) {
            txt.text = "Dr BC's favorite number: " + partialVal;
            return;
        }

        if(keypadCode == 1969) {
            txt.text = "The year we went to the moon: " + partialVal;
            return;
        }

        if(keypadCode == 420) {
            txt.text = "Famous illegal plant number: " + partialVal;
            return;
        }

        if(keypadCode == 9001) {
            txt.text = "High power level: " + partialVal;
            return;
        }

        if(keypadCode == 42) {
            txt.text = "The answer to the ultimate question: " + partialVal;
            return;
        }

        int problemDecider;

        switch (difficulty)
        {
            // For easiest difficulty, always give an addition problem.
            case 0:
                adder1 = Random.Range(0, (int)keypadCode + 1);
                adder2 = keypadCode - adder1;
                // Example: looks like: 42 + 72 = ?114
                txt.text = adder1 + " + " + adder2 + final + partialVal;
                break;
            case 1:
                problemDecider = Random.Range(0, 2);
                if(keypadCode >= (1000) && problemDecider == 0)
                {
                    int modulator = 1;
                    divisor = modulator;
                    while (modulator < keypadCode / 6)
                    {
                        if (keypadCode % modulator == 0)
                            divisor = modulator;
                        modulator++;
                    }

                    multiple = keypadCode / divisor;
                    txt.text = divisor + " * " + multiple + final + partialVal;
                }
                else
                {
                    adder1 = Random.Range(0, (int)keypadCode + 1);
                    adder2 = keypadCode - adder1;
                    txt.text = adder1 + " + " + adder2 + final + partialVal;
                }
                break;
            case 2:
                problemDecider = Random.Range(0, 3);
                if (keypadCode >= (1000) && problemDecider == 0)
                {
                    int modulator = 1;
                    divisor = modulator;
                    while (modulator < keypadCode / 6)
                    {
                        if (keypadCode % modulator == 0)
                            divisor = modulator;
                        modulator++;
                    }

                    multiple = keypadCode / divisor;
                    txt.text = divisor + " * " + multiple + final + partialVal;
                } 
                else
                {
                    adder1 = Random.Range(0, (int)keypadCode + 1);
                    adder2 = keypadCode - adder1;
                    txt.text = adder1 + " + " + adder2 + final + partialVal;
                }
                break;
            case 3:
                string bin = "0b" + System.Convert.ToString(keypadCode, 2);
                txt.text = bin + final + partialVal;
                break;
            case 4:
                adder1 = Random.Range(0, (int)keypadCode + 1);
                string hex1 = "0x" + System.Convert.ToString(adder1, 16);
                adder2 = keypadCode - adder1;
                string hex2 = "0x" + System.Convert.ToString(adder2, 16);
                txt.text = hex1 + " + " + hex2 + final + partialVal;
                break;
            case 5: // This case should only appear on the hardest difficulty, make it pretty hard
                int[] primes1 = {7, 9, 13, 17, 19, 23, 29, 31};
                int[] primes2 = {37, 41, 43, 47, 53, 59, 61, 67};
                adder1 = primes1[Random.Range(0, 7)] * keypadCode;
                adder2 = primes2[Random.Range(0, 7)] * keypadCode;
                hex1 = "0x" + System.Convert.ToString(adder1, 16);
                hex2 = "0x" + System.Convert.ToString(adder2, 16);
                txt.text = "gcd(" + hex1 + ", " + hex2 + ")" + final + partialVal;
                break;
            default:
                Debug.Log("Error in terminal math generation");
                break;
        }
    }
}