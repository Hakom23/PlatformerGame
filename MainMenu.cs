using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer { 
public class MainMenu : MonoBehaviour
{
        public void StartGame()
        {
            SceneManager.LoadScene("Platformerscene");
        }

        public void OnApplicationQuit()
        {
           Application.Quit();
        }

    }

}