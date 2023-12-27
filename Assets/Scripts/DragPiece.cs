using UnityEngine;
using UnityEngine.EventSystems;

public class DragPiece : EventTrigger
{
    [HideInInspector]
    public bool dragging;

    private PieceController pieceController;

    [Header("Collider Size")]
    private float initialXSize;
    private float initialYSize;
    private BoxCollider2D boxCollider;

    [Header("Pergerakan Controller")]
    private MoveController moveController;

    [HideInInspector]
    public bool activated = true;

    // Start is called before the first frame update
    void Start()
    {
        pieceController = GetComponent<PieceController>();
        moveController = FindObjectOfType<MoveController>();

        boxCollider = GetComponent<BoxCollider2D>();
        initialXSize = boxCollider.size.x;
        initialYSize = boxCollider.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Ketika Bidak ditekan
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (activated == true)
        {
            if (dragging == false)
            {
                pieceController.DeactivatingBoxMove();
                pieceController.ActivatingBoxMove();
            }

            dragging = true;

            boxCollider.size = new Vector2(initialXSize / 2, initialYSize / 2);

            transform.SetAsLastSibling();

            moveController.pieceOnDrag = gameObject;
        }

    }

    //Ketika Bidak dilepas
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (activated == true)
        {
            if (pieceController.boxPos != null)
            {
                if (pieceController.boxPos != pieceController.lastBox) //Mengecek apakah pergerakan tidak di tempat yang sama
                {
                    moveController.posisiAwal = pieceController.lastBox.name;
                    moveController.posisiAkhir = pieceController.boxPos.name;
                    moveController.CheckingMoves();
                }
            }

            if (pieceController.boxPos.GetComponent<BoxController>().pieceInHere != null && pieceController.boxPos != pieceController.lastBox)
            {
                Destroy(pieceController.boxPos.GetComponent<BoxController>().pieceInHere);
            }

            pieceController.lastBox.GetComponent<BoxController>().pieceInHere = null;
            pieceController.lastBox.GetComponent<BoxController>().pieceOnSit = false;
            pieceController.lastBox.GetComponent<BoxController>().activated = false;
            pieceController.lastBox = pieceController.boxPos;

            if (dragging == true) pieceController.DeactivatingBoxMove();

            dragging = false;

            transform.position = pieceController.boxPos.transform.position;

            pieceController.boxPos.GetComponent<BoxController>().pieceOnSit = true;
            pieceController.lastBox.GetComponent<BoxController>().pieceInHere = gameObject;


            boxCollider.size = new Vector2(initialXSize, initialYSize);

        }
    }

}
