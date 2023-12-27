using UnityEngine;
using UnityEngine.EventSystems;

public class PieceController : MonoBehaviour
{
    public bool hitam;

    [Header("Bidak")]
    public bool pion;
    public bool kuda;
    public bool gajah;
    public bool benteng;
    public bool ratu;
    public bool raja;

    [Header("FirstPos")]
    public string firstBox;

    [Header("Activated Box")]
    [HideInInspector]
    public GameObject lastBox;
    private int xFirstCoordinate;
    private int yFirstCoordinate;
    private int xCoordinate;
    private int yCoordinate;

    [HideInInspector]
    public bool canBeDrag = false;

    [HideInInspector]
    public GameObject boxPos;
    private bool already = false;

    private DragPiece dragPiece;

    private bool firstPosition = true;

    // Start is called before the first frame update
    void Start()
    {
        dragPiece = GetComponent<DragPiece>();

    }

    // Update is called once per frame
    void Update()
    {
        if (already == false)
        {
            boxPos = GameObject.Find(firstBox);
            lastBox = GameObject.Find(firstBox);
            already = true;
        }

        if (dragPiece.dragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }


    }

    public void ActivatingBoxMove()
    {
        if (pion == true)
        {
            GameObject[] boxController = GameObject.FindGameObjectsWithTag("Box");

            if (hitam == false)
            {
                yCoordinate = yFirstCoordinate;
                xCoordinate = xFirstCoordinate;
                yCoordinate += 1;
            }

            else if (hitam == true)
            {
                yCoordinate = yFirstCoordinate;
                xCoordinate = xFirstCoordinate;
                yCoordinate -= 1;
            }

            foreach (GameObject boxes in boxController)
            {
                BoxController controller = boxes.GetComponent<BoxController>();
                if (controller.coordinateY == yCoordinate && controller.coordinateX == xCoordinate && controller.pieceInHere == null) ActivatingBox();

            }

            //Bagian Menyamping
            yCoordinate = yFirstCoordinate;
            xCoordinate = xFirstCoordinate;
            xCoordinate += 1;
            yCoordinate += 1;

            
            foreach (GameObject boxes in boxController)
            {
                BoxController controller = boxes.GetComponent<BoxController>();
                if (controller.coordinateY == yCoordinate && controller.coordinateX == xCoordinate && controller.pieceInHere!= null)
                    if (controller.pieceInHere.GetComponent<AIMove>()) ActivatingBox();
            }

            yCoordinate = yFirstCoordinate;
            xCoordinate = xFirstCoordinate;
            xCoordinate -= 1;
            yCoordinate += 1;

            foreach (GameObject boxes in boxController)
            {
                BoxController controller = boxes.GetComponent<BoxController>();
                if (controller.coordinateY == yCoordinate && controller.coordinateX == xCoordinate && controller.pieceInHere != null) 
                    if (controller.pieceInHere.GetComponent<AIMove>())ActivatingBox();
            }
        }

        else if (kuda == true)
        {

            for (int i = -2; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (Mathf.Abs(i) == 1)
                    {

                        xCoordinate = xFirstCoordinate;
                        yCoordinate = yFirstCoordinate;

                        xCoordinate += i;
                        yCoordinate += 2;

                        ActivatingBox();

                        yCoordinate = yFirstCoordinate;
                        yCoordinate -= 2;

                        ActivatingBox();
                    }

                    else if (Mathf.Abs(i) == 2)
                    {
                        xCoordinate = xFirstCoordinate;
                        yCoordinate = yFirstCoordinate;

                        xCoordinate += i;
                        yCoordinate += 1;

                        ActivatingBox();

                        yCoordinate = yFirstCoordinate;
                        yCoordinate -= 1;

                        ActivatingBox();
                    }

                }

            }
        }

        else if (benteng == true)
        {
            Benteng();
        }

        else if (gajah == true)
        {
            Gajah();
        }

        else if (ratu == true)
        {
            Gajah();
            Benteng();
        }

        else if (raja == true)
        {

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {

                    xCoordinate = xFirstCoordinate;
                    yCoordinate = yFirstCoordinate;

                    xCoordinate += i;
                    yCoordinate += j;

                    if (!(i == 0 && j == 0))
                    {
                        ActivatingBox();
                    }

                }
            }


            xCoordinate += 1;
            yCoordinate += 1;


        }
    }

    //Fungsi untuk mengaktifkan Box yang dituju
    private bool ActivatingBox()
    {
        bool stop = false;
        GameObject[] boxControllers = GameObject.FindGameObjectsWithTag("Box");
        foreach (GameObject boxes in boxControllers)
        {
            BoxController boxControl = boxes.GetComponent<BoxController>();
            if (boxControl.coordinateY == yCoordinate && boxControl.coordinateX == xCoordinate && boxControl.pieceOnSit == false)
            {
                boxControl.activated = true;
            }

            //Jika Koordinat yang ingin dituju merupakan koordinat yang sedang diduki oleh hitam
            else if (boxControl.coordinateY == yCoordinate && boxControl.coordinateX == xCoordinate && boxControl.pieceOnSit == true &&
                boxControl.pieceInHere != null)
            {
                stop = true;
                if (boxControl.pieceInHere.GetComponent<AIMove>() == true)
                {
                    boxControl.activated = true;
                }

            }


            //Jika Koordinat yang ingin dituju sedang diduki oleh bidak teman maka otomatis di Cancel
            else if (boxControl.coordinateY == yCoordinate && boxControl.coordinateX == xCoordinate && boxControl.pieceOnSit == true) stop = true;
        }

        return stop;
    }

    public void DeactivatingBoxMove()
    {
        GameObject[] boxControllers = GameObject.FindGameObjectsWithTag("Box");

        foreach (GameObject boxes in boxControllers)
        {
            BoxController boxControl = boxes.GetComponent<BoxController>();
            boxControl.activated = false;
            lastBox.GetComponent<BoxController>().activated = true;
        }
    }

    void Benteng()
    {
        xCoordinate = xFirstCoordinate;
        yCoordinate = yFirstCoordinate;
        //Atas
        for (int i = yCoordinate; i < 9; i++)
        {
            xCoordinate += 0;
            yCoordinate += 1;

            //Jika Nilai return dari ACtivating Box = benar, maka pengulngan akan dibatalkan
            if (ActivatingBox() == true) {
                break;
            }
                
        }

        xCoordinate = xFirstCoordinate;
        yCoordinate = yFirstCoordinate;
        //Bawah
        for (int i = yCoordinate; i > 0; i--)
        {
            xCoordinate += 0;
            yCoordinate -= 1;

            //Jika Nilai return dari ACtivating Box = benar, maka pengulngan akan dibatalkan
            if (ActivatingBox() == true)
            {
                break;
            }
        }

        xCoordinate = xFirstCoordinate;
        yCoordinate = yFirstCoordinate;
        //Kanan
        for (int i = xCoordinate; i < 9; i++)
        {
            xCoordinate += 1;
            yCoordinate += 0;

            //Jika Nilai return dari ACtivating Box = benar, maka pengulngan akan dibatalkan
            if (ActivatingBox() == true)
            {
                break;
            }

        }

        xCoordinate = xFirstCoordinate;
        yCoordinate = yFirstCoordinate;
        //Kiri
        for (int i = xCoordinate; i > 0; i--)
        {
            xCoordinate -= 1;
            yCoordinate += 0;

            //Jika Nilai return dari ACtivating Box = benar, maka pengulngan akan dibatalkan
            if (ActivatingBox() == true)
            {
                break;
            }
        }
    }

    void Gajah()
    {
        //Kanan Atas
        xCoordinate = xFirstCoordinate;
        yCoordinate = yFirstCoordinate;

        for (int i = xCoordinate; i < 16; i++)
        {
            xCoordinate += 1;
            yCoordinate += 1;

            //Jika Nilai return dari ACtivating Box = benar, maka pengulngan akan dibatalkan
            if (ActivatingBox() == true)
            {
                break;
            }
        }

        //Kiri Atas
        xCoordinate = xFirstCoordinate;
        yCoordinate = yFirstCoordinate;

        for (int i = xCoordinate; i < 16; i++)
        {
            xCoordinate -= 1;
            yCoordinate += 1;

            //Jika Nilai return dari ACtivating Box = benar, maka pengulngan akan dibatalkan
            if (ActivatingBox() == true)
            {
                break;
            }
        }

        //Kanan Bawah
        xCoordinate = xFirstCoordinate;
        yCoordinate = yFirstCoordinate;

        for (int i = xCoordinate; i < 16; i++)
        {
            xCoordinate += 1;
            yCoordinate -= 1;

            //Jika Nilai return dari ACtivating Box = benar, maka pengulngan akan dibatalkan
            if (ActivatingBox() == true)
            {
                break;
            }
        }

        //Kiri Bawah
        xCoordinate = xFirstCoordinate;
        yCoordinate = yFirstCoordinate;

        for (int i = xCoordinate; i < 16; i++)
        {
            xCoordinate -= 1;
            yCoordinate -= 1;

            //Jika Nilai return dari ACtivating Box = benar, maka pengulngan akan dibatalkan
            if (ActivatingBox() == true)
            {
                break;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box") && collision.gameObject.GetComponent<BoxController>().activated == true) 
        {
            boxPos = collision.gameObject;
            xFirstCoordinate = boxPos.GetComponent<BoxController>().coordinateX;
            yFirstCoordinate = boxPos.GetComponent<BoxController>().coordinateY;
        } 

        else if (collision.gameObject.CompareTag("Box") && firstPosition == true)
        {

            xFirstCoordinate = boxPos.GetComponent<BoxController>().coordinateX;
            yFirstCoordinate = boxPos.GetComponent<BoxController>().coordinateY;
            boxPos.gameObject.GetComponent<BoxController>().activated = true;
            firstPosition = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            boxPos = lastBox;
        }
    }


}
