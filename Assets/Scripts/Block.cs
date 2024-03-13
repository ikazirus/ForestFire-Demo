using UnityEngine;

public class Block : MonoBehaviour
{
    public BlockType blockType = BlockType.EMPTY;
    [SerializeField] private bool isSeleted = false;
    [SerializeField] private bool onFire = false;

    [SerializeField] private GameObject selection;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject floor;

    [SerializeField] private GameObject tree01;
    [SerializeField] private GameObject tree02;

    [SerializeField] private GameObject house01;
    [SerializeField] private GameObject house02;


    private float moveSmoothness = 500f;

    private void Start()
    {

    }


    private void Update()
    {

        switch (blockType)
        {
            case BlockType.EMPTY:
                tree01.SetActive(false);
                tree02.SetActive(false);
                house01.SetActive(false);
                house02.SetActive(false);
                floor.SetActive(false);
                break;
            case BlockType.CLEAR:
                floor.SetActive(true);
                break;
            case BlockType.TREE01:
                tree01.SetActive(true);
                floor.SetActive(true);
                break;
            case BlockType.TREE02:
                tree02.SetActive(true);
                floor.SetActive(true);
                break;
            case BlockType.HOUSE01:
                house01.SetActive(true);
                floor.SetActive(true);
                break;
            case BlockType.HOUSE02:
                house02.SetActive(true);
                floor.SetActive(true);
                break;

        }

        if (isSeleted)
        {
            selection.SetActive(true);
        }
        else
        {
            selection.SetActive(false);
        }

        if (onFire && blockType != BlockType.EMPTY)
        {
            fire.SetActive(true);
        }
        else
        {
            fire.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (!blockType.Equals(BlockType.EMPTY) && onFire == false)
        {
            isSeleted = !isSeleted;
            if (GameManager.instance.firstBlock == null || !GameManager.instance.firstBlock.isSeleted)
            {
                GameManager.instance.firstBlock = this;
            }
            else
            {
                GameManager.instance.secondBlock = this;
                move();
            }

        }

    }

    public void move()
    {
        Vector3 pos1 = GameManager.instance.firstBlock.transform.position;
        Vector3 pos2 = GameManager.instance.secondBlock.transform.position;

        GameManager.instance.firstBlock.transform.position = Vector3.Lerp(GameManager.instance.firstBlock.transform.position,
           pos2, Time.deltaTime * moveSmoothness);

        GameManager.instance.secondBlock.transform.position = Vector3.Lerp(GameManager.instance.secondBlock.transform.position,
           pos1, Time.deltaTime * moveSmoothness);


        GameManager.instance.firstBlock.isSeleted = false;
        GameManager.instance.secondBlock.isSeleted = false;
        GameManager.instance.firstBlock = null;
        GameManager.instance.secondBlock = null;
    }

    public void SetFire(bool fire)
    {
        onFire = fire;
    }
}


public enum BlockType
{
    EMPTY,
    CLEAR,
    TREE01,
    TREE02,
    HOUSE01,
    HOUSE02
}
