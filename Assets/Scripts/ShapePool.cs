using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shape Pool", menuName = "Shape Pool")]
public class ShapePool : ScriptableObject
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

    [SerializeField] private int I1Probability;
    [SerializeField] private int I2Probability;
    [SerializeField] private int I3Probability;
    [SerializeField] private int I4Probability;
    [SerializeField] private int I5Probability;
    [SerializeField] private int L2Probability;
    [SerializeField] private int L23Probability;
    [SerializeField] private int L32Probability;
    [SerializeField] private int L33Probability;
    [SerializeField] private int ZProbability;
    [SerializeField] private int TProbability;
    [SerializeField] private int O2Probability;
    [SerializeField] private int O3Probability;

    private ProbabilityList<Shape> shapes;

    public void Init()
    {
        shapes = new ProbabilityList<Shape>();
        CreateShapes();
    }

    public Shape GetRandomShape()
    {
        return shapes.Get();
    }

    private void CreateShapes()
    {
        CreateShape(I1, false, false, I1Probability);
        CreateShape(I2, true, false, I2Probability);
        CreateShape(I3, true, false, I3Probability);
        CreateShape(I4, true, false, I4Probability);
        CreateShape(I5, true, false, I5Probability);
        CreateShape(L2, true, true, L2Probability);
        CreateShape(L23, true, true, L23Probability);
        CreateShape(L32, true, true, L32Probability);
        CreateShape(L33, true, true, L33Probability);
        CreateShape(Z, true, true, ZProbability);
        CreateShape(T, true, true, TProbability);
        CreateShape(O2, false, false, O2Probability);
        CreateShape(O3, false, false, O3Probability);
    }

    private void CreateShape(int[] input, bool createRotate, bool createFlip, int probability)
    {
        probability *= 4;
        probability /= createRotate ? 2 : 1;
        probability /= createFlip ? 2 : 1;

        bool[,] matrix = new bool[5, 5];
        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                matrix[i, j] = input[i + j * 5] > 0;
            }
        }

        CreateShape(matrix, probability);

        if (createRotate)
        {
            bool[,] rotateMatrix = matrix.Rotate();
            CreateShape(rotateMatrix, probability);
        }

        if (createFlip)
        {
            bool[,] flipMatrix = matrix.FlipHorizontal();
            CreateShape(flipMatrix, probability);

            if (createRotate)
            {
                bool[,] rotateFlipMatrix = flipMatrix.Rotate();
                CreateShape(rotateFlipMatrix, probability);
            }
        }
    }

    private void CreateShape(bool[,] matrix, int probabilities)
    {
        Shape shape = new Shape(matrix);
        shapes.Add(shape, probabilities);
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
