using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSelector : MonoBehaviour
{
    public static PieceSelector Instance { get; private set; }
    private Piece selectedPiece;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            SelectPiece();
        }
    }

    private void SelectPiece()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            Piece piece = hit.collider.GetComponent<Piece>();
            if (piece != null)
            {
                if (selectedPiece != null)
                {
                    selectedPiece.Deselect();
                }
                selectedPiece = piece;
                selectedPiece.Select();
            }
        }
    }

    public Piece GetSelectedPiece()
    {
        return selectedPiece;
    }
}
