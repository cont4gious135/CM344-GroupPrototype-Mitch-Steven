using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFail : MonoBehaviour
{
    public GameObject levelFailUI;

    public void Fail()
    {
        Time.timeScale = 0f;
        levelFailUI.SetActive(true);
    }

    public void RestartButton() {
        //SceneManager.LoadScene("SampleScene");
    }
}
