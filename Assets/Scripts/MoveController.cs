using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MoveController : MonoBehaviour
{
    [Header("Posisi Tiap Move")]
    public string[] posisiAwalBenar;
    public string[] posisiAkhirBenar;
    [HideInInspector]
    public string posisiAwal;
    [HideInInspector]
    public string posisiAkhir;

    [Header("Game Finish")]
    public GameObject losePanel;
    public GameObject winPanel;

    [Header("Level")]
    public int level;

    public bool putihBergerak = true;

    private int moveCounts = 0;

    private bool alreadyControlPanel = false;

    

    [Header("Game Over")]
    public float waitingTime;

    public float waitingCounter = 0f;
    [HideInInspector]
    public GameObject pieceOnDrag;
    private bool kalah;
    private bool menang;

    // Start is called before the first frame update
    void Start()
    {
        GameObject canvass = GameObject.FindGameObjectWithTag("canvas");
        transform.SetParent(canvass.transform);

        losePanel = GameObject.Find("Lose_Panel");
        winPanel = GameObject.Find("Win_Panel");

    }

    // Update is called once per frame
    void Update()
    {
        if (putihBergerak == false) CheckingMoves();

        if (alreadyControlPanel == false)
        {
            if (losePanel != null)
            {
                losePanel.SetActive(false);
                losePanel.transform.SetAsLastSibling();
            }
            if (winPanel != null)
            {
                winPanel.SetActive(false);
                winPanel.transform.SetAsLastSibling();
            }

            alreadyControlPanel = true;
        }

        //Game Over
        if (kalah == true) Kalah();
        if (menang == true) Menang();

    }

    public void CheckingMoves()
    {

        if (putihBergerak == true)
        {
            if (posisiAwal == posisiAwalBenar[moveCounts] && posisiAkhir == posisiAkhirBenar[moveCounts])
            {

                moveCounts += 1;

                //Jika sudah Menang
                if (moveCounts >= posisiAkhirBenar.Length)
                {
                    menang = true;
                    BoxController[] papan = FindObjectsOfType<BoxController>();
                    foreach (BoxController board in papan)
                    {
                        if (board.pieceInHere != null)
                        {
                            if (board.pieceInHere.gameObject.name == "Raja_Hitam(Clone)")
                            {
                                board.gameOver = true;
                                board.gameObject.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
                                Debug.Log("Berhasil");
                            }
                        }

                    }

                    DragPiece[] bidak = FindObjectsOfType<DragPiece>();
                    foreach (DragPiece pieces in bidak)
                    {
                        pieces.activated = false;
                    }
                }

                else putihBergerak = false;
            }

            //JIka salah maka Scene di Reset
            else
            {
                kalah = true;
                pieceOnDrag.GetComponent<Animator>().SetTrigger("wrong");

                DragPiece[] bidak = FindObjectsOfType<DragPiece>();
                foreach (DragPiece pieces in bidak)
                {
                    pieces.activated = false;
                }

            }

        }

        else
        {
            GameObject box = GameObject.Find(posisiAwalBenar[moveCounts]).GetComponent<BoxController>().pieceInHere;
            box.GetComponent<AIMove>().moving = true;
            box.GetComponent<AIMove>().directionObject = GameObject.Find(posisiAkhirBenar[moveCounts]);
            box.GetComponent<AIMove>().directionPos = GameObject.Find(posisiAkhirBenar[moveCounts]).transform.position;
            box.GetComponent<AIMove>().lastPos = GameObject.Find(posisiAwalBenar[moveCounts]);


            moveCounts += 1;
            putihBergerak = true;
        }

    }

    void Kalah()
    {
        waitingCounter += Time.deltaTime;
        if (waitingCounter >= waitingTime)
        {
            waitingCounter = 0;
            losePanel.SetActive(true);
            DestroyAllItem();
            kalah = false;
        }
    }

    void Menang()
    {
        waitingCounter += Time.deltaTime;
        if (waitingCounter >= waitingTime)
        {
            waitingCounter = 0;
            winPanel.SetActive(true);
            DestroyAllItem();
            menang = false;
        }
    }

    void DestroyAllItem()
    {
        GameObject[] kontrller = GameObject.FindGameObjectsWithTag("Controller");
        GameObject[] bidak = GameObject.FindGameObjectsWithTag("bidak");
        GameObject[] kotak = GameObject.FindGameObjectsWithTag("Box");

        foreach (GameObject controllers in kontrller)
        {
            //Destroy(controllers);
        }

        foreach (GameObject bidaks in bidak)
        {
            Destroy(bidaks);
        }

        foreach (GameObject boxes in kotak)
        {
            Destroy(boxes);
        }
    }
}
