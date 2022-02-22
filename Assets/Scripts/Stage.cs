using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    // 필요 소스 불러오기
    [Header("Source")]
    public GameObject tilePrefab;
    public Transform backgroundNode;
    public Transform boardNode;
    public Transform tetrominoNode;

    [Header("Setting")]
    [Range(4, 40)]
    public int boardWidth = 10;
    // 높이 설정
    [Range(5, 20)]
    public int boardHeight = 20;
    // 떨어지는 속도
    public float fallcycle = 1.0f;
    // 위치 보정
    public float offset_x = 0f;
    public float offset_y = 0f;

    // 타일 생성
    Tile CreateTile(Transform parent, Vector2 position, Color color, int order = 1)
    {
        var go = Instantiate(tilePrefab); // tilePrefab를 복제한 오브젝트 생성
        go.transform.parent = parent; // 부모 지정
        go.transform.localPosition = position; // 위치 지정
        var tile = go.GetComponent<Tile>(); // tilePrefab의 Tile스크립트 불러오기
        tile.color = color; // 색상 지정
        tile.sortingOrder = order; // 순서 지정

        return tile;
    }

    private int halfWidth;
    private int halfHeight;

    private void Start() // 게임이 시작되고 한 번만 실행
    {
        halfWidth = Mathf.RoundToInt(boardWidth * 0.5f); // 너비의 중간값 설정해주기
        halfHeight = Mathf.RoundToInt(boardHeight * 0.5f); // 높이의 중간값 설정해주기

        CreateBackground(); // 배경 만들기
        CreateTetromino(); // 테트로미노 만들기
    }

    //// 타일 생성
    //Tile CreateTile(Transform parent, Vector2 position, Color color, int order=1)
    //{

    //}

    // 배경 타일을 생성
    void CreateBackground()
    {
        Color color = Color.gray; // 배경 색 설정(원하는 색으로 설정 가능)

        // 타일 보드
        color.a = 0.5f; // 테두리와 구분 위해 투명도 바꾸기
        for (int x = -halfWidth; x < halfWidth; ++x)
        {
            for (int y = halfHeight; y > -halfHeight; --y)
            {
                CreateTile(backgroundNode, new Vector2(x, y), color, 0);
            }
        }

        // 좌우 테두리
        color.a = 1.0f;
        for (int y = halfHeight; y > -halfHeight; --y)
        {
            CreateTile(backgroundNode, new Vector2(-halfWidth - 1, y), color, 0);
            CreateTile(backgroundNode, new Vector2(halfWidth, y), color, 0);
        }

        // 아래 테두리
        for (int x = -halfWidth - 1; x <= halfWidth; ++x)
        {
            CreateTile(backgroundNode, new Vector2(x, -halfHeight), color, 0);
        }
    }

    // 테크로미노 생성
    void CreateTetromino()
    {
        int index = Random.Range(0, 7); // 랜덤으로 0~6 사이의 값 생성
        // 위의 코드 대신 아래와 같은 코드로 원하는 Index모양 확인 가능
        // int index = 1;

        Color32 color = Color.white;

        // 회전 계산에 사용하기 위한 쿼터니언 클래스
        tetrominoNode.rotation = Quaternion.identity;
        //// 테트로미노 생성 위치 (중앙 상단) - 수정 전
        //tetrominoNode.position = new Vector2(0, halfHeight);
        // 테트로미노 생성 위치 (중앙 상단), 값 보정 적용 
        tetrominoNode.position = new Vector2(offset_x, halfHeight + offset_y);

        switch (index)
        {
            // 구분을 위해 테트리스 모양에 비슷하게 영어로 표현 (I, J, L, O, S, T, Z)

            case 0: // I
                color = new Color32(131, 221, 255, 255); // 하늘색
                CreateTile(tetrominoNode, new Vector2(-2f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color);                
                break;
            case 1: // J
                color = new Color32(131, 151, 255, 255); // 파란색
                CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(-1f, 1.0f), color);                
                break;
            case 2: // ㅣ
                color = new Color32(255, 184, 131, 255); // 주황색
                CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 1.0f), color);                
                break;
            case 3: // O
                color = new Color32(255, 251, 131, 255); // 노란색
                CreateTile(tetrominoNode, new Vector2(0f, 0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 1f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 1f), color);                
                break;
            case 4: // S
                color = new Color32(192, 255, 131, 255); // 녹색
                CreateTile(tetrominoNode, new Vector2(-1f, -1f), color);
                CreateTile(tetrominoNode, new Vector2(0f, -1f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color);                
                break;
            case 5: // T
                color = new Color32(169, 131, 255, 255); // 보라색
                CreateTile(tetrominoNode, new Vector2(-1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 1.0f), color);                
                break;
            case 6: // Z
                color = new Color32(255, 131, 131, 255); // 빨간색
                CreateTile(tetrominoNode, new Vector2(-1f, 1.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 1.0f), color);
                CreateTile(tetrominoNode, new Vector2(0f, 0.0f), color);
                CreateTile(tetrominoNode, new Vector2(1f, 0.0f), color);                
                break;

        }
    }
    // https://wikidocs.net/91229


}
