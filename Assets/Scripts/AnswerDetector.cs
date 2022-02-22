using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnswerDetector : MonoBehaviour
{
    public GameObject[] currentRow;
    public GameObject[] answerKey;
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

        Material[] currentRowMats = new Material[currentRow.Length];
        for (int i = 0; i < currentRow.Length; i++)
        {
            Material temp =
                currentRow[i].GetComponent<MeshRenderer>().material;
            currentRowMats[i] = temp;
        }

        Report(answerMats, currentRowMats);

        int xx = 13;



    } 

    void Report(Material[] answerMats, Material[] currentRowMats)
    {
        int[] answers = new int[answerMats.Length];

        for (int i = 0; i < answerMats.Length; i++)
        {
            if (answerMats[i] == currentRowMats[i])
            {
                answers[i] = 1;
                Debug.Log($"{answerMats[i]} is the same in both");
            }

            else
            {
                
                Debug.Log($"{answerMats[i]} does not match {currentRow[i]}");

                List<Material> compMats = answerMats.ToList();

                if (compMats.Contains(currentRowMats[i]))
                    answers[i] = 0;
                else
                    answers[i] = -1;
            }
        }
        for (int i = 0; i < currentRowMats.Length; i++)
        {
            if (answers[i] == 1)
                Debug.Log("Correct Color/Position:");
            else if (answers[i] == 0)
                Debug.Log("Wrong Color/Position:");
            else
                Debug.Log("Color not used");

            Debug.Log($"At Index:{i}, nums is {currentRow[i]}, and nums2 is {currentRowMats[i]}");
        }
    }
}
