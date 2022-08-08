using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Matrix5x5
{
    public int[,] matrix;

    public Matrix5x5(int[] input)
    {
        matrix = new int[5, 5];
        int x = 0;
        int y = 0;
        for (int i = 0; i < input.Length && i < 5 * 5; i++)
        {
            matrix[x, y] = input[i];
            x++;
            if (x >= 5)
            {
                x = 0;
                y++;
            }
        }
    }

    public void FlipHorizontal()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 5; i++)
            {
                int temp = matrix[5 - 1 - i, j];
                matrix[5 - 1 - i, j] = matrix[i, j];
                matrix[i, j] = temp;
            }
        }
    }

    public void Rotate90()
    {
        for (int i = 0; i < 5 / 2; i++)
        {
            for (int j = i; j < 5 - i - 1; j++)
            {
                int temp = matrix[i, j];
                matrix[i, j] = matrix[j, 5 - 1 - i];
                matrix[j, 5 - 1 - i] = matrix[5 - 1 - i, 5 - 1 - j];
                matrix[5 - 1 - i, 5 - 1 - j] = matrix[5 - 1 - j, i];
                matrix[5 - 1 - j, i] = temp;
            }
        }
    }

    public int GetBitMask()
    {
        int result = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (matrix[i, j] > 0)
                {
                    int id = i + j * 8;
                    result += 1 << id;
                }
            }
        }
        return result;
    }
}
