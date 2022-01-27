using UnityEngine;

public sealed partial class PuzzleSystem
{
    private void HandlerSetPiecePosition(PieceInteraction pieceInteraction)
    {
        _puzzleData.HandlerPieceClicked();

        Piece piece = _dictPieceInteractions[pieceInteraction];

        SwapPositions(piece);
        SwapRowCollumn(piece);
        SetCorrectPosition(piece);

        _puzzleStatusData.SaveGame(_puzzleStatus);

        if (_isFInishGame == true)
        {
            return;
        }

        EnablePieces();
    }

    private void SetCorrectPosition(Piece piece)
    {
        if (PieceIsInCorrectPosition(piece))
        {
            _piecesCorrectPosition[piece.Index] = true;

            if (!_piecesCorrectPosition.Contains(false))
            {
                _isFInishGame = true;
                _puzzleStatus.initialLoadGame = false;
                _puzzleData.HandlerAllPiecesInCorrectPosition();
            }
        }
        else
        {
            _piecesCorrectPosition[piece.Index] = false;
        }
    }

    private void EnablePieces()
    {
        PieceBase piece = _emptyPiece.piece;

        DesactivePieces();

        EnablePieceInteraction(piece.Row - 1, piece.Collumn);
        EnablePieceInteraction(piece.Row + 1, piece.Collumn);

        EnablePieceInteraction(piece.Row, piece.Collumn - 1);
        EnablePieceInteraction(piece.Row, piece.Collumn + 1);
    }

    private void EnablePieceInteraction(int row, int collum)
    {
        if (row > _puzzleData.PuzzleSize - 1 || row < 0 || collum > _puzzleData.PuzzleSize - 1 || collum < 0)
        {
            return;
        }

        Piece piece = (Piece)_rowsCollumns[row, collum];
        BoxCollider boxCollider = piece.PieceInteraction.BoxCollider;

        boxCollider.enabled = true;

        _activePieces.Add(boxCollider);
    }

    private void DesactivePieces()
    {
        if (_activePieces.Count == 0 || _activePieces == null)
        {
            return;
        }

        for (int i = 0; i < _activePieces.Count; i++)
        {
            BoxCollider boxCollider = _activePieces[i];
            boxCollider.enabled = false;
        }

        _activePieces.Clear();
    }

    private bool PieceIsInCorrectPosition(Piece piece)
    {
        int indexPiecePosition = IndexPiecePosition(piece.Row, piece.Collumn);

        return indexPiecePosition == piece.Index;
    }
}