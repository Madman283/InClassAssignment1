using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnswerDetector : MonoBehaviour
{
    public GameObject[] currentRow;
    public GameObject[] answerKey;
    public GameObject[] pins;
    public GameObject hintGrid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Evaluate();
        }
    }

    private void Evaluate()
    {
        Material[] answerMats = new Material[answerKey.Length];
        for (int i = 0; i < answerKey.Length; i++)
        {
            Material temp =
                answerKey[i].GetComponent<MeshRenderer>().material;
            answerMats[i] = temp;
        }

        Material[] currentMats = new Material[currentRow.Length];
        for (int i = 0; i < currentRow.Length; i++)
        {
            Material temp =
                currentRow[i].GetComponent<MeshRenderer>().material;
            currentMats[i] = temp;
        }

        Report(answerMats, currentMats);



    } 
    void Report(Material[] answerMats, Material[] currentMats)
    {
        int[] answerValues = new int[currentMats.Length];
        List<Material> compMats = answerMats.ToList();
        List<Color> colorAnswers = new List<Color>();

        foreach (var item in compMats)
        {
            colorAnswers.Add(item.color);
        }

        for (int i = 0; i < answerMats.Length; i++)
        {
            if (answerMats[i] == currentMats[i])
            {
                answerValues[i] = 1;
                Debug.Log($"{answerMats[i]} is the same in both");
            }

            else
            {
                
                Debug.Log($"{answerMats[i]} does not match {currentRow[i]}");

                

                if (compMats.Contains(currentMats[i]))
                    answerValues[i] = 0;
                else
                    answerValues[i] = -1;
            }
        }

        //This is an old code. It only does debugging messages so its not actually needed.
        // The answerMats code above is also not needed if wanted
        //for (int i = 0; i < currentMats.Length; i++)
        //{
        //    if (answerValues[i] == 1)
        //        Debug.Log("Correct Color/Position:");
        //    else if (answerValues[i] == 0)
        //        Debug.Log("Wrong Color/Position:");
        //    else
        //        Debug.Log("Color not used");

        //    Debug.Log($"At Index:{i}, nums is {currentRow[i]}, and nums2 is {currentMats[i]}");

           
        //}
        for (int i = 0; i < currentMats.Length; i++)
        {
            if (currentMats[i].color == answerMats[i].color)
            {
                answerValues[i] = 1;
                instantiateCorrectPin(hintGrid.transform.GetChild(i).transform);
            }
            else if (colorAnswers.Contains(currentMats[i].color))
            {
                answerValues[i] = 0;
                instantiateWrongPin(hintGrid.transform.GetChild(i).transform);
            }
            else
            {
                answerValues[i] = -1;
            }
        }
    }
    void instantiateCorrectPin(Transform transform)
    {
        GameObject pin = Instantiate(pins[0]);
        pin.transform.position = transform.position;


    }
    void instantiateWrongPin(Transform transform)
    {
        GameObject pin = Instantiate(pins[1]);
        pin.transform.position = transform.position;


    }
}
