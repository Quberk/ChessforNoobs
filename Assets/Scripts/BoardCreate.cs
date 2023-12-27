using UnityEngine;
using UnityEngine.UI;

public class BoardCreate : MonoBehaviour
{
    public GameObject blackBox;
    public GameObject whiteBox;

    public float distance;

    public float xStart;
    public float yStart;

    private float xPos;
    private float yPos;

    private bool isBlack = false;

    private GameObject canvass;

    [Header("Tes")]
    public GameObject[] bidakPutih;
    public GameObject[] bidakHitam;
    public string[] posPutih;
    public string[] posHitam;

    // Start is called before the first frame update
    void Start()
    {
        canvass = GameObject.FindGameObjectWithTag("canvas");

        transform.SetParent(canvass.transform);
        GetComponent<RectTransform>().localScale = new Vector3(0.2723797f, 0.2723797f, 0.2723797f);
        GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 0f, 0f);

        //Pembuatan Koordinat Huruf A-H
        for (int i = 0; i < 8; i++)
        {
            xPos = xStart + (distance * i); // Penambahan jarak pada SUmbu X ketika telah melakukan perulangan sebanyak 8 kali di SUmbu Y

            if (isBlack == true) isBlack = false;
            else isBlack = true;

            for (int j = 0; j < 8; j++) // Pengulangan untuk sumbu Y pada Koordinat 1-8
            {
                yPos = yStart + (distance * j);// Penambahan jarak pada SUmbu Y
                Vector2 pos = new Vector2(xPos, yPos + 9f);

                if (isBlack == true)
                {
                    GameObject papan = Instantiate(blackBox, transform.position, Quaternion.identity);
                    papan.transform.SetParent(canvass.transform);
                    papan.GetComponent<RectTransform>().anchoredPosition = pos;
                    papan.GetComponent<RectTransform>().localScale = new Vector3(0.34f, 0.34f, 0.34f);
                    papan.GetComponent<BoxController>().coordinateX = i + 1;
                    papan.GetComponent<BoxController>().coordinateY = j + 1;

                    papan.gameObject.name = (char)(i+65) +"_"+ (j + 1).ToString(); //Pemberian nama pada Objek Papan sebagai Koordinat

                    isBlack = false;
                }

                else
                {
                    GameObject papan = Instantiate(whiteBox, transform.position, Quaternion.identity);
                    papan.transform.SetParent(canvass.transform);
                    papan.GetComponent<RectTransform>().anchoredPosition = pos;
                    papan.GetComponent<RectTransform>().localScale = new Vector3(0.34f, 0.34f, 0.34f);

                    papan.GetComponent<BoxController>().coordinateX = i + 1;
                    papan.GetComponent<BoxController>().coordinateY = j + 1;

                    papan.gameObject.name = (char)(i + 65) + "_" + (j + 1).ToString();
                    isBlack = true;
                }
                
                
            }
        }

        TestingPawn();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TestingPawn()
    {
        //Bidak Putih
        for (int i = 0; i < bidakPutih.Length; i++)
        {
            GameObject position = GameObject.Find(posPutih[i]);
            GameObject bidaks = Instantiate(bidakPutih[i], position.transform.position, Quaternion.identity);
            bidaks.GetComponent<PieceController>().firstBox = posPutih[i];
            position.GetComponent<BoxController>().pieceOnSit = true;
            position.GetComponent<BoxController>().pieceInHere = bidaks;
            bidaks.transform.SetParent(GameObject.FindGameObjectWithTag("canvas").transform);
            bidaks.GetComponent<RectTransform>().anchoredPosition = position.GetComponent<RectTransform>().anchoredPosition;
            bidaks.GetComponent<RectTransform>().localScale = new Vector3(0.2494546f, 0.2494546f, 0.2494546f);
        }

        //Bidak HItam
        for (int i = 0; i < bidakHitam.Length; i++)
        {
            GameObject position = GameObject.Find(posHitam[i]);
            GameObject bidaks = Instantiate(bidakHitam[i], position.transform.position, Quaternion.identity);
            //bidaks.GetComponent<PieceController>().firstBox = posHitam[i];
            position.GetComponent<BoxController>().pieceOnSit = true;
            position.GetComponent<BoxController>().pieceInHere = bidaks;
            bidaks.transform.SetParent(GameObject.FindGameObjectWithTag("canvas").transform);
            bidaks.GetComponent<RectTransform>().anchoredPosition = position.GetComponent<RectTransform>().anchoredPosition;
            bidaks.GetComponent<RectTransform>().localScale = new Vector3(0.2494546f, 0.2494546f, 0.2494546f);
        }

    }
}
