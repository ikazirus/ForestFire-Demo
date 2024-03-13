using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Level> levels;
    [Range(0, 10)]
    public int currentLevel = 0;

    [HideInInspector]
    public Block firstBlock;
    [HideInInspector]
    public Block secondBlock;

    public GameObject blockPrefab;
    public Transform levelTransform;


    [HideInInspector]
    public bool canMove = false;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

        for (int i = 0; i < levels[currentLevel].size.x; i++)
        {
            for (int j = 0; j < levels[currentLevel].size.y; j++)
            {
                GameObject go = Instantiate(blockPrefab.gameObject) as GameObject;
                int x = (int)Random.Range(0, 7);
                BlockType type = BlockType.EMPTY;

                switch (x)
                {
                    case 1:
                        type = BlockType.TREE01;
                        break;
                    case 2:
                        type = BlockType.TREE02;
                        break;
                    case 3:
                        type = BlockType.HOUSE01;
                        break;
                    case 4:
                        type = BlockType.HOUSE02;
                        break;
                    case 5:
                        type = BlockType.CLEAR;
                        break;
                    case 6:
                        type = BlockType.EMPTY;
                        break;
                    default:
                        type = BlockType.EMPTY;
                        break;
                }

                if (i == 0 && j == 0)
                {
                    type = BlockType.TREE01;
                    go.GetComponent<Block>().SetFire(true);
                }


                go.GetComponent<Block>().blockType = type;

                go.transform.position = new Vector3(i - (levels[currentLevel].size.x / 2), 0,
                    j - (levels[currentLevel].size.y / 2));
                go.transform.SetParent(levelTransform);
            }
        }
    }
}

[System.Serializable]
public class Level
{
    public string name;
    public Vector2Int size;

    public List<List<BlockType>> map;
}
