using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStateManager : MonoBehaviour
{
    public GameObject levelCompletedUI;
    // Start is called before the first frame update
    void Start()
    {
        redCheck = otherObject.GetComponent<RedKeyManager>().redCheck;
        blueCheck = otherObject.GetComponent<BlueKeyManager>().blueCheck;
        yellowCheck = otherObject.GetComponent<YellowKeyManager>().yellowCheck;
    }

    // Update is called once per frame
    void Update()
    {
        if (redCheck == true && blueCheck == true && yellowCheck == true)
        {
            if (levelCompletedUI != null)
                levelCompletedUI.SetActive(true);
        }
    }
}
