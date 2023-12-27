using UnityEngine;
using UnityEngine.UI;

public class BoxController : MonoBehaviour
{

    public bool activated = false;

    public bool gameOver = false;

    public int coordinateX, coordinateY;

    private Image image;

    public bool pieceOnSit = false;

    public GameObject pieceInHere;


    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activated == true) image.color = new Color32(0, 255, 4, 154);
        else if (activated == false && gameOver == false) image.color = new Color32(255, 255, 255, 255);

        //if (GameObject.Find(pieceInHere.name) == null) pieceInHere = null;
    }



}
