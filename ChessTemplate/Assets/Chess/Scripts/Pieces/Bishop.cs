using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chess.Scripts.Core;
using System;

public class Bishop : Piece
{
    public Transform playerPositions;
    public Transform enemyPositions;
    private ChessPlayerPlacementHandler _chessPlayerPlacementHandler;

    private int[,] friendlyPiecesPos;
    private int[,] enemyPiecesPos;

    private int _row;
    private int _column;
    //Tracking Vars for debugging
    private int topRightBlock, topLeftBlock, bottomRightBlock, bottomLeftBlock;

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
        //topRightBlock
        topRightBlock = -1;
        for (int i = _row + 1, j = _column + 1; i < 8 && j < 8; i++, j++)
        {
            if (friendlyPiecesPos[i, j] == 1)
            {
                topRightBlock = i;
                break;
            }
            if (enemyPiecesPos[i,j] == -1)
            {
                ChessBoardPlacementHandler.Instance.RedHighlight(i, j);
                break;
            }
            ChessBoardPlacementHandler.Instance.Highlight(i, j);
            
        }
        //topLeftBlock
        topLeftBlock = -1;
        for (int i = _row + 1, j = _column - 1; i < 8 && j >= 0; i++, j--) 
        {
            if (friendlyPiecesPos[i, j] == 1)
            {
                topLeftBlock = i;
                break;
            }
            if (enemyPiecesPos[i, j] == -1)
            {
                ChessBoardPlacementHandler.Instance.RedHighlight(i, j);
                break;
            }
            ChessBoardPlacementHandler.Instance.Highlight(i, j);
        }
        //bottomRightBlock
        bottomRightBlock = -1;
        for (int i = _row - 1, j = _column + 1; i >=0 && j < 8;i--,j++)
        {
            if (friendlyPiecesPos[i, j] == 1)
            {
                bottomRightBlock = j;
                break;
            }
            if (enemyPiecesPos[i, j] == -1)
            {
                ChessBoardPlacementHandler.Instance.RedHighlight(i, j);
                break;
            }
            ChessBoardPlacementHandler.Instance.Highlight(i, j);
        }
        //bottomLeftBlock
        bottomLeftBlock = -1;
        for (int i=_row-1,j = _column - 1; i >= 0 && j >= 0; i--, j--)
        {
            if (friendlyPiecesPos[i, j] == 1)
            {
                bottomLeftBlock = j;
                break;
            }
            if (enemyPiecesPos[i, j] == -1)
            {
                ChessBoardPlacementHandler.Instance.RedHighlight(i, j);
                break;
            }
            ChessBoardPlacementHandler.Instance.Highlight(i, j);
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

    //Check For Enemy Pieces
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
