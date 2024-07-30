using UnityEngine;
using Chess.Scripts.Core;

public class King : Piece
{
    public Transform playerPositions;// To get GameObjects Present on Player Position
    public Transform enemyPositions;// To get GameObjects Present on Enemy Position
    private ChessPlayerPlacementHandler _chessPlayerPlacementHandler;
    private int _row;
    private int _column;

    private int[,] friendlyPiecesPos; // To set 1 if friendlyPiece is there (can be bool too.)
    private int[,] enemyPiecesPos; // To set -1 if friendlyPiece is there (can be bool too.)

    // Start is called before the first frame update
    void Start()
    {
        friendlyPiecesPos = new int[8, 8];//friendlyArray init
        enemyPiecesPos = new int[8, 8];//enemyArraya init
        _chessPlayerPlacementHandler = GetComponent<ChessPlayerPlacementHandler>();//We need this to get current position for the piece
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
        //For King
        for (int i = _row - 1; i <= _row + 1; i++)
        {
            for (int j = _column - 1; j <= _column + 1; j++)
            {
                if (i >= 0 && i < 8 && j >= 0 && j < 8 && !(i == _row && j == _column))
                {
                    if (friendlyPiecesPos[i, j] == 0)
                    {
                        if (enemyPiecesPos[i, j] == -1)
                        {
                            ChessBoardPlacementHandler.Instance.RedHighlight(i, j);
                        }
                        else
                        {
                            ChessBoardPlacementHandler.Instance.Highlight(i, j);
                        }
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

    //Checking For EnemyPieces
    public override void CheckForEnemyPieces()
    {
        SetAllIntsToZero(enemyPiecesPos);
        foreach(Transform enemyPieces in enemyPositions)
        {
            ChessEnemyPlacementHandler tempChessEnemyPlacementHandler = enemyPieces.GetComponent<ChessEnemyPlacementHandler>();
            var tRow = tempChessEnemyPlacementHandler.row;
            var tCol = tempChessEnemyPlacementHandler.column;

            enemyPiecesPos[tRow, tCol] = -1;
        }
    }
}
