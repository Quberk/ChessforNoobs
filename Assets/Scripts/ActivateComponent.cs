using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActivateComponent : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("PuzzleScene"))
        {
            if (GetComponent<MoveController>() != null) GetComponent<MoveController>().enabled = true;
            if (GetComponent<BoardCreate>() != null) GetComponent<BoardCreate>().enabled = true;
            if (GetComponent<Image>() != null) GetComponent<Image>().enabled = true;
            this.enabled = false;
        }
    }
}
