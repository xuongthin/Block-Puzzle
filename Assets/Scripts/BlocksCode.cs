using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksCode
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

    public readonly int[] L3 = {
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

    List<Matrix5x5> shapes;

    private void Start()
    {
        Matrix5x5 shapeI1 = new Matrix5x5(I1);
        Matrix5x5 shapeI2 = new Matrix5x5(I2);
        Matrix5x5 shapeI3 = new Matrix5x5(I3);
        Matrix5x5 shapeI4 = new Matrix5x5(I4);
        Matrix5x5 shapeI5 = new Matrix5x5(I5);
        Matrix5x5 shapeL2 = new Matrix5x5(L2);
        Matrix5x5 shapeL3 = new Matrix5x5(L3);
        Matrix5x5 shapeL33 = new Matrix5x5(L33);
        Matrix5x5 shapeT = new Matrix5x5(T);
        Matrix5x5 shapeO2 = new Matrix5x5(O2);
        Matrix5x5 shapeO3 = new Matrix5x5(O3);

        shapes.Add(shapeI1);
        shapes.Add(shapeI2);
        shapes.Add(shapeI3);
        shapes.Add(shapeI4);
        shapes.Add(shapeI5);
        shapes.Add(shapeL2);
        shapes.Add(shapeL3);
        shapes.Add(shapeL33);
        shapes.Add(shapeT);
        shapes.Add(shapeO2);
        shapes.Add(shapeO3);
    }
}
