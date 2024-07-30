# Chess Game Project

## Description

A chess game implemented in Unity. This project includes the basic functionality for a chess game with piece movement, highlighting possible moves, and detecting enemy pieces.

## Features

- **Piece Movement**: Basic movement and highlighting for each chess piece, including Pawn, King, Rook, Knight, Queen, and Bishop.
- **Highlighting Moves**: Highlights possible moves for selected pieces and differentiates between friendly and enemy pieces.
- **Enemy Detection**: Highlights enemy pieces in red when they are in the range of movement.

## Unity Version

- Unity 2023.2.20f1

## Getting Started

### Prerequisites

- Unity 2023.2.20f1

### Installation

1. Clone or download this repository.
2. Open Unity Hub and select "Add" to add the project to your Unity Hub.
3. Open the project in Unity.

### Usage

1. Run the project in Unity.
2. Use the mouse to click on chess pieces to select them.
3. Selected pieces will show possible moves.
4. Enemy pieces within the range of movement will be highlighted in red.

## Project Structure

- `Assets/Scripts/`: Contains C# scripts for the game logic.
- `Assets/Scenes/`: Contains the Unity scene files.
- `Assets/Prefabs/`: Contains prefabs for chess pieces and other game objects.

## Scripts

- **Piece.cs**: Base class for all chess pieces.
- **King.cs**: Handles movement and highlights for the King piece.
- **Rook.cs**: Handles movement and highlights for the Rook piece.
- **Pawn.cs**: Handles movement and highlights for the Pawn piece.
- **Knight.cs**: Handles movement and highlights for the Knight piece.
- **Bishop.cs**: Handles movement and highlights for the Bishop piece.
- **Queen.cs**: Handles movement and highlights for the Queen piece.
- **PieceSelector.cs**: Manages piece selection and move highlighting.
- **ChessBoardPlacementHandler.cs**: Manages the board highlighting and placement.
- **ChessPlayerPlacementHandler.cs**: Handles placement and movement of player pieces (editable in the Unity Editor).
- **ChessEnemyPlacementHandler.cs**: Handles placement and movement of enemy pieces (editable in the Unity Editor).

## Acknowledgements

- Unity Technologies for the game engine.
