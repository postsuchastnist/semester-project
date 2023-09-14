using System;
using System.Reflection;
using MatrixClass;
using Xunit;

namespace tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Matrix a = new Matrix(3, 3);
        double[,] input = {{1, 2, 3}, {4, 5, 6}, {7, 8, 9}};
        a.Matrixread(input);
        double res = a.det();
        Console.WriteLine(res);
        Assert.Equal(0, res);
    }
    [Fact]
    public void Test2()
    {
        Matrix a = new Matrix(3, 3);
        double[,] input = {{1, 2, 3}, {4, 5, 6}, {7, 8, 9}};
        a.Matrixread(input);
        Matrix b = new Matrix(3, 3);
        double[,] input1 = {{5, 7, 8}, {10, 15, 26}, {37, 28, 19}};
        b.Matrixread(input1);
        Matrix c = a + b;
        Matrix check = new Matrix(3, 3);
        double[,] checkm = {{6, 9, 11}, {14, 20, 32}, {44, 36, 28}};
        check.Matrixread(checkm);
        Assert.Equal(c.mat(), check.mat());
    }
    [Fact]
    public void Test3()
    {
        Matrix a = new Matrix(3, 2);
        double[,] input = {{1, 2}, {-1, 0}, {2, 4}};
        a.Matrixread(input);
        Matrix b = new Matrix(2, 3);
        double[,] input1 = {{1, 0, -1}, {2, 3, 0}};
        b.Matrixread(input1);
        Matrix c = a * b;
        double[,] checkm = {{5, 6, -1}, {-1, 0, 1}, {10, 12, -2}};
        Assert.Equal(c.mat(), checkm);
    }
    [Fact]
    public void Test4()
    {
        Matrix a = new Matrix(2, 2);
        double[,] input = {{1, -1}, {0, 2}};
        a.Matrixread(input);
        Matrix res = a.inverse();
        double[,] checkm = {{1, 0.5}, {0, 0.5}};
        Assert.Equal(checkm, res.mat());
    }
}