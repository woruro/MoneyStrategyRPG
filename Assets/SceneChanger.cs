using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void GoToBattle()
    {
        SceneManager.LoadScene("BattleScene");
    }
}