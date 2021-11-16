using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour
{
    [SerializeField]public static int objects_left = 3;
    float game_time_limit = 20f;


    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            objects_left = 1;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            objects_left = 2;
        }
        else
        {
            objects_left = 3;
        }
    }
    private void Update()
    {
        game_time_limit -= Time.deltaTime;

        Debug.Log(objects_left);
        if (objects_left == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            if(game_time_limit < 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

    }
}
