using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    // �ʿ� �ҽ� �ҷ�����
    [Header("Source")]
    public GameObject tilePrefab;
    public Transform backgroundNode;
    public Transform boardNode;
    public Transform tetrominoNode;

    [Header("Setting")]
    [Range(4, 40)]
    public int boardWidth = 10;
    // ���� ����
    [Range(5, 20)]
    public int boardHeight = 20;
    // �������� �ӵ�
    public float fallcycle = 1.0f;
    // ��ġ ����
    public float offset_x = 0f;
    public float offset_y = 0f;

    // Ÿ�� ����
    Tile CreateTile(Transform parent, Vector2 position, Color color, int order = 1)
    {
        var go = Instantiate(tilePrefab); // tilePrefab�� ������ ������Ʈ ����
        go.transform.parent = parent; // �θ� ����
        go.transform.localPosition = position; // ��ġ ����
        var tile = go.GetComponent<Tile>(); // tilePrefab�� Tile��ũ��Ʈ �ҷ�����
        tile.color = color; // ���� ����
        tile.sortingOrder = order; // ���� ����

        return tile;
    }

    private int halfWidth;
    private int halfHeight;

    private void Start() // ������ ���۵ǰ� �� ���� ����
    {
        halfWidth = Mathf.RoundToInt(boardWidth * 0.5f); // �ʺ��� �߰��� �������ֱ�
        halfHeight = Mathf.RoundToInt(boardHeight * 0.5f); // ������ �߰��� �������ֱ�

        CreateBackground(); // ��� �����
        CreateTetromino(); // ��Ʈ�ι̳� �����
    }

    //// Ÿ�� ����
    //Tile CreateTile(Transform parent, Vector2 position, Color color, int order=1)
    //{

    //}

    // ��� Ÿ���� ����
    void CreateBackground()
    {
        Color color = Color.gray; // ��� �� ����(���ϴ� ������ ���� ����)

        // Ÿ�� ����
        color.a = 0.5f; // �׵θ��� ���� ���� ���� �ٲٱ�
        for (int x = -halfWidth; x < halfWidth; ++x)
        {
            for (int y = halfHeight; y > -halfHeight; --y)
            {
                CreateTile(backgroundNode, new Vector2(x, y), color, 0);
            }
        }

        // �¿� �׵θ�
        color.a = 1.0f;
        for (int y = halfHeight; y > -halfHeight; --y)
        {
            CreateTile(backgroundNode, new Vector2(-halfWidth - 1, y), color, 0);
            CreateTile(backgroundNode, new Vector2(halfWidth, y), color, 0);
        }

        // �Ʒ� �׵θ�
        for (int x = -halfWidth - 1; x <= halfWidth; ++x)
        {
            CreateTile(backgroundNode, new Vector2(x, -halfHeight), color, 0);
        }
    }

    // ��ũ�ι̳� ����
    void CreateTetromino()
    {
        int index = Random.Range(0, 7); // �������� 0~6 ������ �� ����
        // ���� �ڵ� ��� �Ʒ��� ���� �ڵ�� ���ϴ� Index��� Ȯ�� ����
        // int index = 1;

        Color32 color = Color.white;

        // ȸ�� ��꿡 ����ϱ� ���� ���ʹϾ� Ŭ����
        tetrominoNode.rotation = Quaternion.identity;
        //// ��Ʈ�ι̳� ���� ��ġ (�߾� ���) - ���� ��
        //tetrominoNode.position = new Vector2(0, halfHeight);
        // ��Ʈ�ι̳� ���� ��ġ (�߾� ���), �� ���� ���� 
        tetrominoNode.position = new Vector2(offset_x, halfHeight + offset_y);

        switch (index)
        {
            // ������ ���� ��Ʈ���� ��翡 ����ϰ� ����� ǥ�� (I, J, L, O, S, T, Z)

            case 0: // I
                color = new Color32(131, 221, 255, 255); // �ϴû�
                CreateTile(tetrominoNode, new Vector2(-2f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color);                
                break;
            case 1: // J
                color = new Color32(131, 151, 255, 255); // �Ķ���
                CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(-1f, 1.0f), color);                
                break;
            case 2: // ��
                color = new Color32(255, 184, 131, 255); // ��Ȳ��
                CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 1.0f), color);                
                break;
            case 3: // O
                color = new Color32(255, 251, 131, 255); // �����
                CreateTile(tetrominoNode, new Vector2(0f, 0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 1f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 1f), color);                
                break;
            case 4: // S
                color = new Color32(192, 255, 131, 255); // ���
                CreateTile(tetrominoNode, new Vector2(-1f, -1f), color);
                CreateTile(tetrominoNode, new Vector2(0f, -1f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color);                
                break;
            case 5: // T
                color = new Color32(169, 131, 255, 255); // �����
                CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 1.0f), color);                
                break;
            case 6: // Z
                color = new Color32(255, 131, 131, 255); // ������
                CreateTile(tetrominoNode, new Vector2(-1f, 1.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 1.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color);                
                break;

        }
    }
    // https://wikidocs.net/91229


}
