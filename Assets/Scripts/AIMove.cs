using UnityEngine;

public class AIMove : MonoBehaviour
{
    public float speed;

    [HideInInspector]
    public Vector3 directionPos;
    [HideInInspector]
    public bool moving = false;
    [HideInInspector]
    public GameObject directionObject;
    [HideInInspector]
    public GameObject lastPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moving == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, directionPos, speed * Time.deltaTime);

            if (transform.position == directionPos)
            {
                moving = false;

                DragPiece[] pieces = FindObjectsOfType<DragPiece>();
                foreach (DragPiece piece in pieces)
                {
                    piece.activated = true;
                }

                //Jika ada yang berada di posisi itu maka akan dimakan
                if (directionObject.GetComponent<BoxController>().pieceInHere != null)
                {
                    if (directionObject.GetComponent<BoxController>().pieceInHere.GetComponent<PieceController>())
                    {
                        Destroy(directionObject.GetComponent<BoxController>().pieceInHere);
                    }
                }


                lastPos.GetComponent<BoxController>().pieceInHere = null;
                lastPos.GetComponent<BoxController>().pieceOnSit = false;
                directionObject.GetComponent<BoxController>().pieceInHere = gameObject;
            }

            else
            {
                DragPiece[] pieces = FindObjectsOfType<DragPiece>();
                foreach (DragPiece piece in pieces)
                {
                    piece.activated = false;
                }
            }
        }
    }
}
