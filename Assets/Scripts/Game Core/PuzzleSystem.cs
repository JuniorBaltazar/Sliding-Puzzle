using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

public sealed class PuzzleSystem : MonoBehaviour
{
    [SerializeField] private PuzzleData _puzzleData = null;
    [SerializeField] private PuzzleGameStatusData _puzzleStatusData;
    [SerializeField] private PieceCustomizationData _pieceCustomizationData = null;
    [SerializeField] private GameObject _puzzleTableObject = null;
    [SerializeField] private bool _randomize = true;

    private bool _isFInishGame = false;
    private EmptyPiece _emptyPiece = new EmptyPiece();

    private List<BoxCollider> _activePieces = new List<BoxCollider>();
    private List<Piece> _pieces = new List<Piece>();
    private List<bool> _piecesCorrectPosition = new List<bool>();

    public PieceBase[,] _rowsCollumns;

    private Dictionary<PieceInteraction, Piece> _dictPieceInteractions = new Dictionary<PieceInteraction, Piece>();

    private int AmountOfPieces => (int)Mathf.Pow(_puzzleData.PuzzleSize, 2) - 1;

    private void OnEnable()
    {
        PieceInteractionData.OnPieceClicked += HandlerSetPiecePosition;
    }

    private void OnDisable()
    {
        PieceInteractionData.OnPieceClicked -= HandlerSetPiecePosition;
    }

    private void Start()
    {
        int[] indices;

        _emptyPiece = new EmptyPiece();

        _puzzleStatusData.LoadGame();

        if (_puzzleStatusData.PuzzleStatus.initialLoadGame == false)
        {            
            indices = new int[(int)Mathf.Pow(_puzzleData.PuzzleSize, 2)];
            _rowsCollumns = new Piece[_puzzleData.PuzzleSize, _puzzleData.PuzzleSize];
            _puzzleStatusData.PuzzleStatus.puzzleSize = _puzzleData.PuzzleSize;

            for (int i = 0; i < indices.Length - 1; i++)
            {
                _piecesCorrectPosition.Add(false);
                indices[i] = i;
            }

            if (_randomize)
            {
                System.Random rng = new System.Random();
                rng.Shuffle(indices);
            }

            indices[indices.Length - 1] = -1;
            _puzzleStatusData.PuzzleStatus.indices = indices;
        }
        else
        {
            _rowsCollumns = new Piece[_puzzleStatusData.PuzzleStatus.puzzleSize, _puzzleStatusData.PuzzleStatus.puzzleSize];
            indices = _puzzleStatusData.PuzzleStatus.indices;

            for (int i = 0; i < indices.Length - 1; i++)
            {
                _piecesCorrectPosition.Add(false);
            }
        }       

        for (int row = 0; row < _puzzleData.PuzzleSize; row++)
        {
            for (int collumn = 0; collumn < _puzzleData.PuzzleSize; collumn++)
            {
                int i = IndexPiecePosition(row, collumn);

                bool newGame = i < AmountOfPieces;
                bool loadGame = _puzzleStatusData.PuzzleStatus.indices[i] != -1;

                bool isPiece = _puzzleStatusData.PuzzleStatus.initialLoadGame == false ? newGame : loadGame ;

                if (isPiece)
                {
                    GameObject pieceObj = Instantiate(_puzzleData.PiecePrefab, _puzzleTableObject.transform);
                    Piece piece = pieceObj.GetComponent<Piece>();
                    PieceInteraction pieceInteraction = piece.PieceInteraction;

                    pieceInteraction.BoxCollider.enabled = false;

                    piece.Index = indices[i];
                    piece.SetRowCollumn(row, collumn);

                    _rowsCollumns[row, collumn] = piece;

                    _pieces.Add(piece);
                    _dictPieceInteractions.Add(pieceInteraction, piece);

                    UpdatePiece(piece);
                    SetCorrectPosition(piece);

                    continue;
                }

                CreateEmptyPiece(i, row, collumn);                
            }
        }

        CustomizeAllPieces();        

        if (_isFInishGame == true)
        {
            _puzzleStatusData.PuzzleStatus.initialLoadGame = false;
            _puzzleStatusData.SaveGame();
            return;
        }
        else
        {
            _puzzleStatusData.PuzzleStatus.initialLoadGame = true;
            _puzzleStatusData.SaveGame();
        }

        EnablePieces();
    }

    private void CustomizeAllPieces()
    {
        for (int i = 0; i < _pieces.Count; i++)
        {
            Piece piece = _pieces[i];
            _pieceCustomizationData.SetPieceCustomizations(piece.PieceText, piece.PieceInteraction.Renderer);
        }
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

        _puzzleStatusData.PuzzleStatus.indices[emptyIndex] = -1;
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

    private void HandlerSetPiecePosition(PieceInteraction pieceInteraction)
    {
        _puzzleData.HandlerPieceClicked();

        Piece piece = _dictPieceInteractions[pieceInteraction];

        SwapPositions(piece);
        SwapRowCollumn(piece);
        SetCorrectPosition(piece);

        _puzzleStatusData.SaveGame();

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
                _puzzleStatusData.PuzzleStatus.initialLoadGame = false;
                _puzzleData.HandlerAllPiecesInCorrectPosition();
            }
        }
        else
        {
            _piecesCorrectPosition[piece.Index] = false;
        }
    }

    private void SwapPositions(Piece piece)
    {
        Vector3 piecePosition = piece.transform.position;

        piece.transform.position = _emptyPiece.position;
        _emptyPiece.position = piecePosition;
    }

    private void SwapRowCollumn(Piece piece)
    {
        int pieceRow = piece.Row;
        int pieceCollumn = piece.Collumn;

        int emptyRow = _emptyPiece.piece.Row;
        int emptyCollumn = _emptyPiece.piece.Collumn;

        //Switch value between Piece and Empty Piece
        piece.SetRowCollumn(emptyRow, emptyCollumn);
        _rowsCollumns[emptyRow, emptyCollumn] = piece;

        _emptyPiece.piece.SetRowCollumn(pieceRow, pieceCollumn);
        _rowsCollumns[pieceRow, pieceCollumn] = null;

        //Save piece positions
        int pieceIndexPosition = IndexPiecePosition(emptyRow, emptyCollumn);
        _puzzleStatusData.PuzzleStatus.indices[pieceIndexPosition] = piece.Index;

        int emptyPieceIndexPosition = IndexPiecePosition(pieceRow, pieceCollumn);
        _puzzleStatusData.PuzzleStatus.indices[emptyPieceIndexPosition] = -1;
    }

    public void EnablePieces()
    {
        PieceBase piece = _emptyPiece.piece;

        DesactivePieces();

        EnablePieceInteraction(piece.Row - 1, piece.Collumn);
        EnablePieceInteraction(piece.Row + 1, piece.Collumn);

        EnablePieceInteraction(piece.Row, piece.Collumn - 1);
        EnablePieceInteraction(piece.Row, piece.Collumn + 1);
    }

    public void EnablePieceInteraction(int row, int collum)
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

    public void DesactivePieces()
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

    private int IndexPiecePosition(int row, int collumn)
    {
        return row * _puzzleData.PuzzleSize + collumn;
    }
}