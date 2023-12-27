using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public GameObject moveController;
    public GameObject papan;

    [Header("Move Controller Atribute")]
    public string[] posisiAwal;
    public string[] posisiAkhir;

    [Header("Papan Atribute")]
    public GameObject[] bidakPutih;
    public GameObject[] bidakHitam;
    public string[] posPutih;
    public string[] posHitam;

    public int level;


    private void Awake()
    {
        moveController.GetComponent<MoveController>().enabled = false;
        papan.GetComponent<Image>().enabled = false;
        papan.GetComponent<BoardCreate>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Ketika Player Menekan Level
    public void LevelSelected()
    {
        MoveController controller = moveController.GetComponent<MoveController>();
        BoardCreate board = papan.GetComponent<BoardCreate>();

        controller.level = level;

        controller.posisiAwalBenar = new string[posisiAwal.Length];
        controller.posisiAkhirBenar = new string[posisiAkhir.Length];

        board.bidakPutih = new GameObject[bidakPutih.Length];
        board.bidakHitam = new GameObject[bidakHitam.Length];

        board.posPutih = new string[posPutih.Length];
        board.posHitam = new string[posHitam.Length];

        for (int i = 0; i < posisiAwal.Length; i++)
        {
            controller.posisiAwalBenar[i] = posisiAwal[i];
            controller.posisiAkhirBenar[i] = posisiAkhir[i];
        }

        for (int j = 0; j < bidakPutih.Length; j++)
        {
            board.bidakPutih[j] = bidakPutih[j];
            board.posPutih[j] = posPutih[j];
        }

        for (int k = 0; k < bidakHitam.Length; k++)
        {
            board.bidakHitam[k] = bidakHitam[k];
            board.posHitam[k] = posHitam[k];
        }


        SceneManager.LoadScene("PuzzleScene");

        DontDestroyOnLoad(moveController.gameObject);
        DontDestroyOnLoad(papan.gameObject);
    }
}
