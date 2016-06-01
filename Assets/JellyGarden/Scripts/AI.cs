using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI : MonoBehaviour
{
    public static AI THIS;
    public bool gotTip;
    public bool allowShowTip;
    int tipID;
    public int corCount;
    private List<Item> nextMoveItems;
    // Use this for initialization
    void Start()
    {
        THIS = this;
    //    StartCoroutine(CheckPossibleCombines());
    //    StartCoroutine(reset());
    }

    IEnumerator reset()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            LevelManager.THIS.ReGenLevel();
        }
    }

    Square GetSquare(int row, int col)
    {
        return LevelManager.THIS.GetSquare(col, row);
    }

    void CheckSquare(Square square, int COLOR, bool moveThis = false)
    {
        if (square == null) return;
        if (square.item != null)
        {
            if (square.item.color == COLOR)
            {
                if (moveThis && square.type != SquareTypes.WIREBLOCK)
                {
                    nextMoveItems.Add(square.item);
                }
                else if(!moveThis)
                    nextMoveItems.Add(square.item);
            }
        }

    }

    public IEnumerator CheckPossibleCombines()
    {
//        print("check tip");
        yield return new WaitForSeconds(1);
        allowShowTip = true;
        int maxRow = LevelManager.THIS.maxRows;
        int maxCol = LevelManager.THIS.maxCols;
        //while (true)
        //{
            gotTip = false;
            while (LevelManager.THIS == null)
            {
                yield return new WaitForEndOfFrame();
            }
            while (LevelManager.THIS.gameStatus != GameState.Playing)
            {
                yield return new WaitForEndOfFrame();
            }

            if (!LevelManager.THIS.DragBlocked && LevelManager.THIS.gameStatus == GameState.Playing)
            {
                nextMoveItems = new List<Item>();

                if (LevelManager.THIS.gameStatus != GameState.Playing) yield break;
                Item it = GameObject.FindGameObjectWithTag("Item").GetComponent<Item>();

                for (int COLOR = 0; COLOR < it.items.Length; COLOR++)
                {
                    for (int col = 0; col < LevelManager.THIS.maxCols; col++)
                    {
                        for (int row = 0; row < LevelManager.THIS.maxRows; row++)
                        {
                            Square square = LevelManager.THIS.GetSquare(col, row);
                            if (square.type == SquareTypes.WIREBLOCK || square.item == null) continue;
                            //current square called x
                            //o-o-x
                            //	  o
                            if (col > 1 && row < maxRow - 1)
                            {
                                CheckSquare(GetSquare(row + 1, col), COLOR, true);
                                CheckSquare(GetSquare(row, col - 1), COLOR);
                                CheckSquare(GetSquare(row, col - 2), COLOR);
                            }
                            if (nextMoveItems.Count == 3 && GetSquare(row, col).CanGoInto())
                            {
                               // StartCoroutine(showTip(nextMoveItems[0], Vector3.up));
                                showTip(nextMoveItems);
                                yield break;
                            }
                            else
                                nextMoveItems.Clear();

                            //    o
                            //o-o x
                            if (col > 1 && row > 0)
                            {
                                CheckSquare(GetSquare(row - 1, col), COLOR, true);
                                CheckSquare(GetSquare(row, col - 1), COLOR);
                                CheckSquare(GetSquare(row, col - 2), COLOR);
                            }
                            if (nextMoveItems.Count == 3 && GetSquare(row, col).CanGoInto())
                            {
                               // StartCoroutine(showTip(nextMoveItems[0], Vector3.down));
                                showTip(nextMoveItems);
                                yield break;
                            }
                            else
                                nextMoveItems.Clear();

                            //x o o
                            //o
                            if (col < maxCol - 2 && row < maxRow - 1)
                            {
                                CheckSquare(GetSquare(row + 1, col), COLOR, true);
                                CheckSquare(GetSquare(row, col + 1), COLOR);
                                CheckSquare(GetSquare(row, col + 2), COLOR);
                            }
                            if (nextMoveItems.Count == 3 && GetSquare(row, col).CanGoInto())
                            {
                               // StartCoroutine(showTip(nextMoveItems[0], Vector3.up));
                                showTip(nextMoveItems);
                                yield break;
                            }
                            else
                                nextMoveItems.Clear();

                            //o
                            //x o o
                            if (col < maxCol - 2 && row > 0)
                            {
                                CheckSquare(GetSquare(row - 1, col), COLOR, true);
                                CheckSquare(GetSquare(row, col + 1), COLOR);
                                CheckSquare(GetSquare(row, col + 2), COLOR);
                            }
                            if (nextMoveItems.Count == 3 && GetSquare(row, col).CanGoInto())
                            {
                              //  StartCoroutine(showTip(nextMoveItems[0], Vector3.down));
                                showTip(nextMoveItems);
                                yield break;
                            }
                            else
                                nextMoveItems.Clear();

                            //o
                            //o
                            //x o
                            if (col < maxCol - 1 && row > 1)
                            {
                                CheckSquare(GetSquare(row, col + 1), COLOR, true);
                                CheckSquare(GetSquare(row - 1, col), COLOR);
                                CheckSquare(GetSquare(row - 2, col), COLOR);
                            }
                            if (nextMoveItems.Count == 3 && GetSquare(row, col).CanGoInto())
                            {
                               // StartCoroutine(showTip(nextMoveItems[0], Vector3.left));
                                showTip(nextMoveItems);
                                yield break;
                            }
                            else
                                nextMoveItems.Clear();

                            //x o
                            //o
                            //o
                            if (col < maxCol - 1 && row < maxRow - 2)
                            {
                                CheckSquare(GetSquare(row, col + 1), COLOR, true);
                                CheckSquare(GetSquare(row + 1, col), COLOR);
                                CheckSquare(GetSquare(row + 2, col), COLOR);
                            }
                            if (nextMoveItems.Count == 3 && GetSquare(row, col).CanGoInto())
                            {
                              //  StartCoroutine(showTip(nextMoveItems[0], Vector3.left));
                                showTip(nextMoveItems);
                                yield break;
                            }
                            else
                                nextMoveItems.Clear();

                            //	o
                            //  o
                            //o x
                            if (col > 0 && row > 1)
                            {
                                CheckSquare(GetSquare(row, col - 1), COLOR, true);
                                CheckSquare(GetSquare(row - 1, col), COLOR);
                                CheckSquare(GetSquare(row - 2, col), COLOR);
                            }
                            if (nextMoveItems.Count == 3 && GetSquare(row, col).CanGoInto())
                            {
                              //  StartCoroutine(showTip(nextMoveItems[0], Vector3.right));
                                showTip(nextMoveItems);
                                yield break;
                            }
                            else
                                nextMoveItems.Clear();

                            //o x
                            //  o
                            //  o
                            if (col > 0 && row < maxRow - 2)
                            {
                                CheckSquare(GetSquare(row, col - 1), COLOR, true);
                                CheckSquare(GetSquare(row + 1, col), COLOR);
                                CheckSquare(GetSquare(row + 2, col), COLOR);
                            }
                            if (nextMoveItems.Count == 3 && GetSquare(row, col).CanGoInto())
                            {
                              //  StartCoroutine(showTip(nextMoveItems[0], Vector3.right));
                                showTip(nextMoveItems);
                                yield break;
                            }
                            else
                                nextMoveItems.Clear();

                            //o-x-o-o
                            if (col < maxCol - 2 && col > 0)
                            {
                                CheckSquare(GetSquare(row, col - 1), COLOR, true);
                                CheckSquare(GetSquare(row, col + 1), COLOR);
                                CheckSquare(GetSquare(row, col + 2), COLOR);
                            }
                            if (nextMoveItems.Count == 3 && GetSquare(row, col).CanGoInto())
                            {
                             //   StartCoroutine(showTip(nextMoveItems[0], Vector3.right));
                                showTip(nextMoveItems);
                                yield break;
                            }
                            else
                                nextMoveItems.Clear();
                            //o-o-x-o
                            if (col < maxCol - 1 && col > 1)
                            {
                                CheckSquare(GetSquare(row, col + 1), COLOR, true);
                                CheckSquare(GetSquare(row, col - 1), COLOR);
                                CheckSquare(GetSquare(row, col - 2), COLOR);
                            }
                            if (nextMoveItems.Count == 3 && GetSquare(row, col).CanGoInto())
                            {
                             //   StartCoroutine(showTip(nextMoveItems[0], Vector3.left));
                                showTip(nextMoveItems);
                                yield break;
                            }
                            else
                                nextMoveItems.Clear();
                            //o
                            //x
                            //o
                            //o
                            if (row < maxRow - 2 && row > 0)
                            {
                                CheckSquare(GetSquare(row - 1, col), COLOR, true);
                                CheckSquare(GetSquare(row + 1, col), COLOR);
                                CheckSquare(GetSquare(row + 2, col), COLOR);
                            }
                            if (nextMoveItems.Count == 3 && GetSquare(row, col).CanGoInto())
                            {
                              //  StartCoroutine(showTip(nextMoveItems[0], Vector3.down));
                                showTip(nextMoveItems);
                                yield break;
                            }
                            else
                                nextMoveItems.Clear();

                            //o
                            //o
                            //x
                            //o
                            if (row < maxRow - 2 && row > 1)
                            {
                                CheckSquare(GetSquare(row + 1, col), COLOR, true);
                                CheckSquare(GetSquare(row - 1, col), COLOR);
                                CheckSquare(GetSquare(row - 2, col), COLOR);
                            }
                            if (nextMoveItems.Count == 3 && GetSquare(row, col).CanGoInto())
                            {
                             //   StartCoroutine(showTip(nextMoveItems[0], Vector3.up));
                                showTip(nextMoveItems);
                                yield break;
                            }
                            else
                                nextMoveItems.Clear();
                            //  o
                            //o x o
                            //  o
                            int h = 0;
                            int v = 0;
                            Item testItem = square.item;
                            Vector3 vDirection = Vector3.zero;
                            if (row < maxRow - 1)
                            {
                                square = GetSquare(row + 1, col);
                                if (square.item != null)
                                {
                                    if (square.item.color == COLOR)
                                    {
                                        vDirection = Vector3.up;
                                        nextMoveItems.Add(square.item);
                                        v++;
                                    }
                                }
                            }
                            if (row > 0)
                            {
                                square = GetSquare(row - 1, col);
                                if (square.item != null)
                                {
                                    if (square.item.color == COLOR)
                                    {
                                        vDirection = Vector3.down;
                                        nextMoveItems.Add(square.item);
                                        v++;
                                    }
                                }
                            }
                            if (col > 0)
                            {
                                square = GetSquare(row, col - 1);
                                if (square.item != null)
                                {
                                    if (square.item.color == COLOR)
                                    {
                                        vDirection = Vector3.right;
                                        nextMoveItems.Add(square.item);
                                        h++;
                                    }
                                }
                            }
                            if (col < maxCol - 1)
                            {
                                square = GetSquare(row, col + 1);
                                if (square.item != null)
                                {
                                    if (square.item.color == COLOR)
                                    {
                                        vDirection = Vector3.left;
                                        nextMoveItems.Add(square.item);
                                        h++;
                                    }
                                }
                            }
                            if (nextMoveItems.Count == 3 && GetSquare(row, col).CanGoInto() && GetSquare(row, col).type != SquareTypes.WIREBLOCK)
                            {
                                if (v > h && nextMoveItems[2].square.type != SquareTypes.WIREBLOCK)
                                { //StartCoroutine(showTip(nextMoveItems[2], new Vector3(Random.Range(-1f, 1f), 0, 0)));
                                    showTip(nextMoveItems);
                                    yield break;

                                }
                                else if (v < h && nextMoveItems[0].square.type != SquareTypes.WIREBLOCK)
                                { // StartCoroutine(showTip(nextMoveItems[0], new Vector3(0, Random.Range(-1f, 1f), 0)));
                                    showTip(nextMoveItems);
                                    yield break;

                                }
                                else
                                    nextMoveItems.Clear();
                            }
                            else
                                nextMoveItems.Clear();

                        }
                    }


                }
                    if (!LevelManager.THIS.DragBlocked)
                    {
                        if (!gotTip) LevelManager.THIS.NoMatches();
                    }

            }
            yield return new WaitForEndOfFrame();
            if (!LevelManager.THIS.DragBlocked)
                StartCoroutine(CheckPossibleCombines());

       // }
    }

    IEnumerator ai()
    {
        float startTime = Time.time;
        while (true)
        {
            while (LevelManager.THIS == null)
            {
                yield return new WaitForEndOfFrame();
            }
            if (!LevelManager.THIS.DragBlocked)
            {
                List<List<Item>> combines = LevelManager.THIS.GetMatches((FindSeparating)Random.Range(0, 3), 2);
                foreach (List<Item> items in combines)
                {

                    //foreach (Item item in items)
                    //{
                    Item item = items[items.Count - 1];
                    int testColor = item.color;
                    print(item.transform.position);
                    List<Item> testPositions = LevelManager.THIS.GetItemsCross(item.square);
                    foreach (Item testItem in testPositions)
                    {
                        if (item.color != testItem.color)
                        {
                            List<Item> testPositionsInside = LevelManager.THIS.GetItemsCross(testItem.square);
                            foreach (Item testItemInside in testPositionsInside)
                            {
                                if (testItemInside.color == item.color && !item.Equals(testItemInside))
                                {
                                    print(Vector3.Distance(testItem.transform.position, testItemInside.transform.position));
                                    if (!item.Equals(testItemInside) && testItemInside.color == item.color && Vector3.Distance(testItem.transform.position, testItemInside.transform.position) < 1.5f && Vector3.Distance(testItem.transform.position, testItemInside.transform.position) > 0)
                                    {
                                        print(testItem.transform.position + " " + testItemInside.transform.position);
                                        testItemInside.SwitchDirection(testItem.transform.position - testItemInside.transform.position);
                                      //  StartCoroutine(nextStep( nextMoveItems));
                                        yield break;
                                    }
                                }
                            }
                        }

                    }
                    //}
                }
                //foreach (List<Item> items in combines)
                //{
                //    foreach (Item item in items)
                //    {
                //        int testColor = item.COLOR;
                //        List<Item> testPositions = Level.THIS.GetItemsCross(item.square);
                //        foreach (Item testItem in testPositions)
                //        {
                //            if (!item.Equals(testItem))
                //            {
                //                try
                //                {
                //                    List<Item> testPositionsInside = Level.THIS.GetItemsCross(testItem.square);
                //                    foreach (Item testItemInside in testPositionsInside)
                //                    {

                //                        if (testItemInside.COLOR == item.COLOR && !item.Equals(testItemInside))
                //                        {
                //                            print(Vector3.Distance(testItem.transform.position, testItemInside.transform.position));
                //                            if (!item.Equals(testItemInside) && testItemInside.COLOR == item.COLOR && Vector3.Distance(testItem.transform.position, testItemInside.transform.position) < 1.5f && Vector3.Distance(testItem.transform.position, testItemInside.transform.position) > 0 && Random.value >= 0.7f)
                //                            {
                //                                print(testItem.transform.position + " " + testItemInside.transform.position);
                //                                testItemInside.SwitchDirection(testItemInside.transform.position - testItem.transform.position);
                //                                StartCoroutine(nextStep( nextMoveItems));
                //                                yield break;
                //                            }
                //                        }
                //                    }

                //                }
                //                catch (System.Exception)
                //                {

                //                }
                //            }

                //        }
                //    }
                //}
            }
            yield return new WaitForEndOfFrame();
        }
    }

    void showTip( List<Item> nextMoveItems)
    {
//        print("show tip");
        StopCoroutine(showTipCor(nextMoveItems));
        StartCoroutine(showTipCor(nextMoveItems));
    }
    IEnumerator showTipCor(List<Item> nextMoveItems)
    {
        gotTip = true;
        corCount++;
        if (corCount > 1) { corCount--; yield break; }
        if (LevelManager.THIS.DragBlocked && !allowShowTip) { corCount--; yield break; }
        tipID = LevelManager.THIS.moveID;
        //while (!LevelManager.THIS.DragBlocked && allowShowTip)
        //{
            yield return new WaitForSeconds(1);
            if (LevelManager.THIS.DragBlocked && !allowShowTip && tipID != LevelManager.THIS.moveID) { corCount--; yield break; }
            foreach (Item item in nextMoveItems)
            {
                if (item == null) { corCount--; yield break; }

            }
            foreach (Item item in nextMoveItems)
            {
                if (item != null)
                    item.anim.SetTrigger("tip");
            }
            yield return new WaitForSeconds(0);
            StartCoroutine(CheckPossibleCombines());
            corCount--;
       // }
    }
    IEnumerator nextStepAI(Item item, Vector3 delta)
    {
        //delta.x *= -1;
        //delta.y *= -1;
        //item.SwitchDirection(delta);
        yield return new WaitForSeconds(2);
        //StartCoroutine(CheckPossibleCombines());
    }


}
