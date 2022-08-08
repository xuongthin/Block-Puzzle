using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ShapeManager : MonoBehaviour
{
    public TextAsset shapeFile;
    private List<Shape> shapes;

    private void Start()
    {
        string content = shapeFile.text;
        for (int i = 0; i < content.Length; i++)
        {
            if (content[i] == '0' || content[i] == '1')
            {
                bool[,] matrix = new bool[5, 5];
                int x = 0, y = 0;
                for (; y < 5; i++)
                {
                    if (content[i] == '0' || content[i] == '1')
                    {
                        matrix[x, y] = content[i] == '1';
                        x++;
                        if (x > 4)
                        {
                            x = 0;
                            y++;
                        }
                    }
                }
                Shape shape = new Shape();
                shape.matrix = matrix;
                shape.bitMask = GetBitMask(matrix);
            }
        }
    }

    private void CreateShape(int[] input, bool createRotate, bool createFlip)
    {
        bool[,] matrix = new bool[5, 5];
        for (int i = 0; i < 5; i++)
        {

        }

        if (createRotate)
        {
            bool[,] rotateMatrix = matrix.
        }

        if (createRotate)
        {
            bool[,] rotateMatrix =
        }
    }

    private int GetBitMask(bool[,] matrix)
    {
        int result = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (matrix[i, j])
                {
                    int id = i + j * 8;
                    result += 1 << id;
                }
            }
        }
        return result;
    }
}

public class Shape
{
    public bool[,] matrix;
    public long bitMask;
}
