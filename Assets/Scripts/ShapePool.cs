using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShapePool
{
    public readonly int[] I1 = {
        0, 0, 0, 0, 0,
        0, 0, 0, 0, 0,
        0, 0, 1, 0, 0,
        0, 0, 0, 0, 0,
        0, 0, 0, 0, 0
    };

    public readonly int[] I2 = {
        0, 0, 0, 0, 0,
        0, 0, 1, 0, 0,
        0, 0, 1, 0, 0,
        0, 0, 0, 0, 0,
        0, 0, 0, 0, 0
    };

    public readonly int[] I3 = {
        0, 0, 0, 0, 0,
        0, 0, 1, 0, 0,
        0, 0, 1, 0, 0,
        0, 0, 1, 0, 0,
        0, 0, 0, 0, 0
    };

    public readonly int[] I4 = {
        0, 0, 1, 0, 0,
        0, 0, 1, 0, 0,
        0, 0, 1, 0, 0,
        0, 0, 1, 0, 0,
        0, 0, 0, 0, 0
    };

    public readonly int[] I5 = {
        0, 0, 1, 0, 0,
        0, 0, 1, 0, 0,
        0, 0, 1, 0, 0,
        0, 0, 1, 0, 0,
        0, 0, 1, 0, 0
    };

    public readonly int[] L2 = {
        0, 0, 0, 0, 0,
        0, 0, 1, 0, 0,
        0, 0, 1, 1, 0,
        0, 0, 0, 0, 0,
        0, 0, 0, 0, 0
    };

    public readonly int[] L23 = {
        0, 0, 0, 0, 0,
        0, 0, 0, 0, 0,
        0, 0, 1, 1, 1,
        0, 0, 1, 0, 0,
        0, 0, 0, 0, 0
    };

    public readonly int[] L32 = {
        0, 0, 1, 0, 0,
        0, 0, 1, 0, 0,
        0, 0, 1, 1, 0,
        0, 0, 0, 0, 0,
        0, 0, 0, 0, 0
    };

    public readonly int[] L33 = {
        0, 0, 1, 0, 0,
        0, 0, 1, 0, 0,
        0, 0, 1, 1, 1,
        0, 0, 0, 0, 0,
        0, 0, 0, 0, 0
    };

    public readonly int[] Z = {
        0, 0, 0, 0, 0,
        0, 1, 1, 0, 0,
        0, 0, 1, 1, 0,
        0, 0, 0, 0, 0,
        0, 0, 0, 0, 0
    };

    public readonly int[] T = {
        0, 0, 0, 0, 0,
        0, 0, 1, 0, 0,
        0, 1, 1, 1, 0,
        0, 0, 0, 0, 0,
        0, 0, 0, 0, 0
    };

    public readonly int[] O2 = {
        0, 0, 0, 0, 0,
        0, 1, 1, 0, 0,
        0, 1, 1, 0, 0,
        0, 0, 0, 0, 0,
        0, 0, 0, 0, 0
    };

    public readonly int[] O3 = {
        0, 0, 0, 0, 0,
        0, 1, 1, 1, 0,
        0, 1, 1, 1, 0,
        0, 1, 1, 1, 0,
        0, 0, 0, 0, 0
    };

    private List<Shape> shapes;

    public ShapePool()
    {
        shapes = new List<Shape>();
        CreateShapes();
    }

    public Shape GetRandomShape()
    {
        int id = Random.Range(0, shapes.Count);
        return shapes[id];
    }

    private void CreateShapes()
    {
        CreateShape(I1, false, false);
        CreateShape(I2, true, false);
        CreateShape(I3, true, false);
        CreateShape(I4, true, false);
        CreateShape(I5, true, false);
        CreateShape(L2, true, true);
        CreateShape(L23, true, true);
        CreateShape(L32, true, true);
        CreateShape(L33, true, true);
        CreateShape(Z, true, true);
        CreateShape(T, true, true);
        CreateShape(O2, false, false);
        CreateShape(O3, false, false);
    }

    private void CreateShape(int[] input, bool createRotate, bool createFlip)
    {
        bool[,] matrix = new bool[5, 5];
        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                matrix[i, j] = input[i + j * 5] > 0;
            }
        }

        CreateShape(matrix);

        if (createRotate)
        {
            bool[,] rotateMatrix = matrix.Rotate();
            CreateShape(rotateMatrix);
        }

        if (createFlip)
        {
            bool[,] flipMatrix = matrix.FlipHorizontal();
            CreateShape(flipMatrix);

            if (createRotate)
            {
                bool[,] rotateFlipMatrix = flipMatrix.Rotate();
                CreateShape(rotateFlipMatrix);
            }
        }
    }

    private void CreateShape(bool[,] matrix)
    {
        Shape shape = new Shape(matrix);
        shapes.Add(shape);
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

[System.Serializable]
public class Shape
{
    public bool[,] matrix;

    public Shape(bool[,] matrix)
    {
        if (matrix.GetLength(0) == 5 || matrix.GetLength(1) == 5)
        {
            this.matrix = matrix;
        }
        else
        {
            matrix = new bool[5, 5];
            Debug.Log("Invalid param to create shape");
        }
    }
}
