using UnityEngine;

public class HSNameInput : MonoBehaviour
{
    [SerializeField]
    GameObject keyPrefab;

    [SerializeField]
    Transform firstRow, secondRow, thirdRow;
    string QUERTY;
    void Start()
    {
        QUERTY = "QWERTYUIOP,ASDFGHJKL,ZXCVBNM";
        int row = 0;
        GameObject currentKey;
        for (int i = 0; i < QUERTY.Length; i++)
        {
            string currentChar = QUERTY.Substring(i,1);
            if (currentChar == ",")
            {
                row++;
                continue;
            }
            if (row == 0)
            {
                currentKey = Instantiate(keyPrefab, firstRow);
            }
            else if (row == 1)
            {
                currentKey = Instantiate(keyPrefab, secondRow);
            }
            else
            {
                currentKey = Instantiate(keyPrefab, thirdRow);

            }
            currentKey.GetComponent<KeyClick>().Setup(currentChar);
        }
    }

    void Update()
    {
        
    }
}
