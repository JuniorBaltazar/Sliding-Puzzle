using System.Collections.Generic;
using UnityEngine;

public class EmptyPiece
{
    public PieceBase piece = new PieceBase();
    public Vector3 position = new Vector3();
}

public sealed class PuzzleController : MonoBehaviour
{
    [SerializeField] private PuzzleData _puzzleData = null;
    [SerializeField] private GameObject _puzzleTableObject = null;
    [SerializeField] private bool _randomize = true;

    private EmptyPiece _emptyPiece = new EmptyPiece();

    public List<bool> _piecesCorretPosition = new List<bool>();

    private PieceBase[,] _rowsCollumns;

    private Dictionary<PieceInteraction, Piece> _dictPieceInteractions = new Dictionary<PieceInteraction, Piece>();

    private int AmountOfPieces => (int)Mathf.Pow(_puzzleData.PuzzleSize, 2) - 1;

    private void OnEnable()
    {
        PieceInteractionData.OnPieceClicked += SetPiecePosition;
    }

    private void OnDisable()
    {
        PieceInteractionData.OnPieceClicked -= SetPiecePosition;
    }

    private void SetPiecePosition(PieceInteraction pieceInteraction)
    {
        Piece piece = _dictPieceInteractions[pieceInteraction];

        SwapPositions(piece);
        SwapRowCollumn(piece);

        if (PieceIsInCorrectPosition(piece))
        {
            _piecesCorretPosition[piece.Index] = true;

            if (!_piecesCorretPosition.Contains(false))
            {
                Debug.Log($"Ganhou o jogo");
            }
        }
        else
        {
            _piecesCorretPosition[piece.Index] = false;
        }
    }

    private bool PieceIsInCorrectPosition(Piece piece)
    {
        int indexPiecePosition = IndexPiecePosition(piece.Row, piece.Collumn);

        return indexPiecePosition == piece.Index;
    }

    private void SwapRowCollumn(Piece piece)
    {
        int pieceRow = piece.Row;
        int pieceCollumn = piece.Collumn;

        int emptyRow = _emptyPiece.piece.Row;
        int emptyCollumn = _emptyPiece.piece.Collumn;

        piece.SetRowCollumn(emptyRow, emptyCollumn);
        _rowsCollumns[emptyRow, emptyCollumn] = piece;

        _emptyPiece.piece.SetRowCollumn(pieceRow, pieceCollumn);
        _rowsCollumns[pieceRow, pieceCollumn] = _emptyPiece.piece;
    }

    private void SwapPositions(Piece piece)
    {
        Vector3 piecePosition = piece.transform.position;

        piece.transform.position = _emptyPiece.position;
        _emptyPiece.position = piecePosition;
    }

    private void Start()
    {
        int[] indices = new int[AmountOfPieces];
        _rowsCollumns = new Piece[_puzzleData.PuzzleSize, _puzzleData.PuzzleSize];

        for (int i = 0; i < indices.Length; i++)
        {
            _piecesCorretPosition.Add(false);
            indices[i] = i;
        }

        if (_randomize)
        {
            System.Random rng = new System.Random();
            rng.Shuffle(indices);
        }

        for (int row = 0; row < _puzzleData.PuzzleSize; row++)
        {
            for (int collumn = 0; collumn < _puzzleData.PuzzleSize; collumn++)
            {
                ;
                int i = IndexPiecePosition(row, collumn);
                if (i < AmountOfPieces)
                {
                    GameObject pieceObj = Instantiate(_puzzleData.PiecePrefab);
                    Piece piece = pieceObj.GetComponent<Piece>();
                    PieceInteraction pieceInteraction = piece.PieceInteraction;

                    pieceInteraction.BoxCollider.enabled = false;
                    piece.Index = indices[i];
                    piece.SetRowCollumn(row, collumn);

                    _rowsCollumns[row, collumn] = piece;

                    _dictPieceInteractions.Add(pieceInteraction, piece);

                    UpdatePiece(piece);

                    continue;
                }

                CreateEmptyPiece(i, row, collumn);
            }
        }
    }

    private int IndexPiecePosition(int row, int collumn)
    {
        return row * _puzzleData.PuzzleSize + collumn;
    }

    private void CreateEmptyPiece(int emptyIndex, int row, int collumn)
    {
        Bounds bnds = _puzzleTableObject.transform.CalculateBounds();

        float rowSize = bnds.center.x - bnds.extents.x;
        float collumnSize = bnds.center.z - bnds.extents.z;
        float surface = bnds.center.y + bnds.extents.y;
        float size = _puzzleData.PuzzleSize;

        Vector3 piecePosition = new Vector3(rowSize + (row + 0.5f) / size, surface, collumnSize + (collumn + 0.5f) / size);

        _emptyPiece.piece.SetRowCollumn(row, collumn);
        _emptyPiece.piece.Index = emptyIndex;
        _emptyPiece.position = piecePosition;

        _rowsCollumns[row, collumn] = _emptyPiece.piece;
    }

    private void UpdatePiece(Piece tile)
    {
        Bounds bnds = _puzzleTableObject.transform.CalculateBounds();

        float left = bnds.center.x - bnds.extents.x;
        float top = bnds.center.z - bnds.extents.z;
        float surface = bnds.center.y + bnds.extents.y;

        float size = _puzzleData.PuzzleSize;

        float scaledInset = _puzzleData.PieceSeparation / size;

        Vector3 piecePosition = new Vector3(left + (tile.Row + 0.5f) / size, surface, top + (tile.Collumn + 0.5f) / size);
        Vector3 pieceScale = new Vector3((1f / size) - (2 * scaledInset), 1, (1f / size) - (2 * scaledInset));        

        tile.transform.position = piecePosition;
        tile.transform.localScale = pieceScale;
    }
}
