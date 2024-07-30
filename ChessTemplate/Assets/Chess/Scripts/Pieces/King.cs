using UnityEngine;
using Chess.Scripts.Core;

public class King : Piece
{
    public Transform playerPositions;// To get GameObjects Present on Player Position
    private ChessPlayerPlacementHandler _chessPlayerPlacementHandler;
    private int _row;
    private int _column;

    private int[,] friendlyPiecesPos; // To set 1 if friendlyPiece is there (can be bool too.)

    // Start is called before the first frame update
    void Start()
    {
        friendlyPiecesPos = new int[8, 8]; //Array init
        _chessPlayerPlacementHandler = GetComponent<ChessPlayerPlacementHandler>(); //We need this to get current position for the piece
        GetMyPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CalculatePossibleMoves()
    {
        GetMyPosition();
        CheckForFriendlyPieces();
        if (!isSelected) return;
        //For King
        ChessBoardPlacementHandler.Instance.ClearHighlights();
        for (var i = _row - 1; i <= _row + 1; i++) 
        {
            for (var j = _column - 1; j <= _column + 1; j++) 
            {
                if(i>= 0 && i<8 && j>=0 && j < 8)
                {
                    if (!(i == _row && j == _column) && friendlyPiecesPos[i, j] == 0)
                    {
                        ChessBoardPlacementHandler.Instance.Highlight(i, j);
                    }
                }  
            }
        }
    }

    //Getting Current Position of the piece
    void GetMyPosition()
    {
        _row = _chessPlayerPlacementHandler.row;
        _column = _chessPlayerPlacementHandler.column;
    }

    //Checking For FriendlyPieces
    public override void CheckForFriendlyPieces()
    {
        SetAllIntsToZero(); //Setting them each time to Zero
        foreach (Transform friendlyPieces in playerPositions)
        {
            ChessPlayerPlacementHandler tempChessPlayerPlacementHandler = friendlyPieces.GetComponent<ChessPlayerPlacementHandler>();
            var tRow = tempChessPlayerPlacementHandler.row;
            var tCol = tempChessPlayerPlacementHandler.column;

            friendlyPiecesPos[tRow, tCol] = 1;
        }
    }

    //Init array with 0 
    private void SetAllIntsToZero()
    {
        for (int i = 0; i < friendlyPiecesPos.GetLength(0); i++)
        {
            for (int j = 0; j < friendlyPiecesPos.GetLength(1); j++)
            {
                friendlyPiecesPos[i, j] = 0;
            }
        }
    }
}
