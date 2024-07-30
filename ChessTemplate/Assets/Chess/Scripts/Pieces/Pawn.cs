using System.Collections;
using System.Collections.Generic;
using Chess.Scripts.Core;
using UnityEngine;

public class Pawn : Piece
{
    public Transform playerPositions;
    public Transform enemyPositions;
    private ChessPlayerPlacementHandler _chessPlayerPlacementHandler;
    private int _row;
    private int _column;
    private int[,] friendlyPiecesPos;
    private int[,] enemyPiecesPos;
    // Start is called before the first frame update
    void Start()
    {
        friendlyPiecesPos = new int[8, 8];
        enemyPiecesPos = new int[8, 8];
        _chessPlayerPlacementHandler = GetComponent<ChessPlayerPlacementHandler>();
        GetMyPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CalculatePossibleMoves()
    {
        GetMyPosition();
        ChessBoardPlacementHandler.Instance.ClearHighlights();
        CheckForFriendlyPieces();
        CheckForEnemyPieces();
        if (!isSelected) return;
        //For Pawn
        //One is mandatory
        //Highlight 1 block ahead
        int i = _row + 1; // position that is 1 block from pawn
        if (i < 8 && friendlyPiecesPos[i, _column] == 0 && enemyPiecesPos[i, _column] == 0)
        {
            ChessBoardPlacementHandler.Instance.Highlight(i, _column);

            // For first row
            if (_row == 1)
            {
                int i2 = _row + 2; // position that is 2 blocks from pawn
                if (i2 < 8 && friendlyPiecesPos[i2, _column] == 0 && enemyPiecesPos[i2, _column] == 0)
                {
                    ChessBoardPlacementHandler.Instance.Highlight(i2, _column);
                }
            }
        }
        //CrossCaptures - RedHighlight
        CrossCapture();
    }

    private void CrossCapture()
    {
        int nextRow = _row + 1;
        int leftColumn = _column - 1;
        int rightColumn = _column + 1;

        //to the left
        if (nextRow < 8 && leftColumn >= 0 && enemyPiecesPos[nextRow, leftColumn] == -1) 
        {
            ChessBoardPlacementHandler.Instance.RedHighlight(nextRow, leftColumn);
        }
        //to the right
        if (nextRow < 8 && rightColumn < 8 && enemyPiecesPos[nextRow, rightColumn] == -1)
        {
            ChessBoardPlacementHandler.Instance.RedHighlight(nextRow, rightColumn);
        }
    }

    void GetMyPosition()
    {
        _row = _chessPlayerPlacementHandler.row;
        _column = _chessPlayerPlacementHandler.column;
    }

    //Checking For FriendlyPieces
    public override void CheckForFriendlyPieces()
    {
        SetAllIntsToZero(friendlyPiecesPos); //Setting them each time to Zero
        foreach (Transform friendlyPieces in playerPositions)
        {
            ChessPlayerPlacementHandler tempChessPlayerPlacementHandler = friendlyPieces.GetComponent<ChessPlayerPlacementHandler>();
            var tRow = tempChessPlayerPlacementHandler.row;
            var tCol = tempChessPlayerPlacementHandler.column;

            friendlyPiecesPos[tRow, tCol] = 1;
        }
    }

    //Init array with 0 
    private void SetAllIntsToZero(int[,] piecePos)
    {
        for (int i = 0; i < piecePos.GetLength(0); i++)
        {
            for (int j = 0; j < piecePos.GetLength(1); j++)
            {
                piecePos[i, j] = 0;
            }
        }
    }

    public override void CheckForEnemyPieces()
    {
        SetAllIntsToZero(enemyPiecesPos);
        foreach (Transform enemyPieces in enemyPositions)
        {
            ChessEnemyPlacementHandler tempChessEnemyPlacementHandler = enemyPieces.GetComponent<ChessEnemyPlacementHandler>();
            var tRow = tempChessEnemyPlacementHandler.row;
            var tCol = tempChessEnemyPlacementHandler.column;

            enemyPiecesPos[tRow, tCol] = -1;
        }
    }
}
