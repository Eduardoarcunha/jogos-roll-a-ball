using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    

    public void PlayGame()
    {
        GameManager.instance.ChangeState(GameManager.GameState.Play);
    }
}
