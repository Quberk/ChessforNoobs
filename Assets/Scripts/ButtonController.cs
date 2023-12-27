using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject moveController;
    public GameObject papan;

    public GameObject[] level;
    private GameObject nextLevelPrefab;

    private Vector3 startSize;

    public GameObject loadingPanel;

    // Start is called before the first frame update
    void Start()
    {
        startSize = gameObject.GetComponent<RectTransform>().localScale;
        loadingPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadAsynchronously (string sceneName)
    {
        //AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        
        yield return null;
    }

    public void OnPointerDown(PointerEventData data)
    {
        float newScaleX = GetComponent<RectTransform>().localScale.x * 20 / 100;
        float newScaleY = GetComponent<RectTransform>().localScale.y * 20 / 100;
        float newScaleZ = GetComponent<RectTransform>().localScale.z * 20 / 100;
        float newSizeX = GetComponent<RectTransform>().localScale.x + newScaleX;
        float newSizeY = GetComponent<RectTransform>().localScale.y + newScaleY;
        float newSizeZ = GetComponent<RectTransform>().localScale.z + newScaleZ;
        GetComponent<RectTransform>().localScale = new Vector3(newSizeX, newSizeY, newSizeZ);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<RectTransform>().localScale = startSize;
    }


    public void PuzzleBtn()
    {
        SceneManager.LoadScene("LevelSelection");
        //StartCoroutine(LoadAsynchronously("LevelSelection"));
        loadingPanel.SetActive(true);
    }

    public void TheoryBtn()
    {

    }

    public void SettingBtn()
    {

    }

    public void BackBtn()
    {
        SceneManager.LoadScene("Home");
        //StartCoroutine(LoadAsynchronously("Home"));
        loadingPanel.SetActive(true);
    }

    public void HomeBtn()
    {
        SceneManager.LoadScene("Home");
        //StartCoroutine(LoadAsynchronously("Home"));
        loadingPanel.SetActive(true);
    }

    public void NextBtn()
    {
        moveController = FindObjectOfType<MoveController>().gameObject;
        papan = FindObjectOfType<BoardCreate>().gameObject;

        moveController.transform.SetParent(null);
        papan.transform.SetParent(null);

        GameObject moveKontroller = Instantiate(moveController);
        GameObject papanBaru = Instantiate(papan);

        moveKontroller.gameObject.name = "MoveController";
        papanBaru.gameObject.name = "Papan";

        nextLevelPrefab = Instantiate(level[moveController.GetComponent<MoveController>().level]);

        Destroy(moveController);
        Destroy(papan);

        nextLevelPrefab.GetComponent<Image>().enabled = false;
        nextLevelPrefab.GetComponent<Button>().enabled = false;
        LevelController levelController = nextLevelPrefab.GetComponent<LevelController>();

        Destroy(nextLevelPrefab);

        //MoveController moveController1 = moveController.GetComponent<MoveController>();
        //BoardCreate boardCreate = papan.GetComponent<BoardCreate>();

        moveKontroller.GetComponent<MoveController>().posisiAwalBenar = new string[levelController.posisiAwal.Length];
        moveKontroller.GetComponent<MoveController>().posisiAkhirBenar = new string[levelController.posisiAwal.Length];

        papanBaru.GetComponent<BoardCreate>().bidakPutih = new GameObject[levelController.bidakPutih.Length];
        papanBaru.GetComponent<BoardCreate>().bidakHitam = new GameObject[levelController.bidakHitam.Length];
        papanBaru.GetComponent<BoardCreate>().posPutih = new string[levelController.bidakPutih.Length];
        papanBaru.GetComponent<BoardCreate>().posHitam = new string[levelController.bidakHitam.Length];

        for (int j = 0; j < levelController.posisiAwal.Length; j++)
        {
            moveKontroller.GetComponent<MoveController>().posisiAwalBenar[j] = levelController.posisiAwal[j];
            moveKontroller.GetComponent<MoveController>().posisiAkhirBenar[j] = levelController.posisiAkhir[j];
        }

        for (int k = 0; k < levelController.bidakHitam.Length; k++)
        {
            papanBaru.GetComponent<BoardCreate>().bidakHitam[k] = levelController.bidakHitam[k];
            papanBaru.GetComponent<BoardCreate>().posHitam[k] = levelController.posHitam[k];
        }

        for (int l = 0; l < levelController.bidakPutih.Length; l++)
        {
            papanBaru.GetComponent<BoardCreate>().bidakPutih[l] = levelController.bidakPutih[l];
            papanBaru.GetComponent<BoardCreate>().posPutih[l] = levelController.posPutih[l];
        }

        moveKontroller.GetComponent<MoveController>().level += 1; 

        moveKontroller.GetComponent<MoveController>().enabled = false;
        papanBaru.GetComponent<BoardCreate>().enabled = false;

        moveKontroller.GetComponent<ActivateComponent>().enabled = true;
        papanBaru.GetComponent<ActivateComponent>().enabled = true;


        DontDestroyOnLoad(moveKontroller);
        DontDestroyOnLoad(papanBaru);

        SceneManager.LoadScene("PuzzleScene");
    }

    public void RetryBtn()
    {
        moveController = FindObjectOfType<MoveController>().gameObject;
        papan = FindObjectOfType<BoardCreate>().gameObject;

        moveController.transform.SetParent(null);
        papan.transform.SetParent(null);

        GameObject moveKontroller = Instantiate(moveController);
        GameObject papanBaru = Instantiate(papan);

        moveKontroller.GetComponent<MoveController>().enabled = false;
        papanBaru.GetComponent<BoardCreate>().enabled = false;

        moveKontroller.GetComponent<ActivateComponent>().enabled = true;
        papanBaru.GetComponent<ActivateComponent>().enabled = true;


        DontDestroyOnLoad(moveKontroller);
        DontDestroyOnLoad(papanBaru);

        SceneManager.LoadScene("PuzzleScene");

    }

    public void CustomSceneLoad(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
