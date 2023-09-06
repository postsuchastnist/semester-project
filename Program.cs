using System;
using static System.Console;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Serialization;
using System.ComponentModel;



class Matrix //creating a class Matrix which creates a matrix and does operations on the matrices
{
    int rows, cols;
    double[,] a;
    public Matrix(int n, int m)
    {
        rows = n;
        cols = m;
        a = new double[rows, cols];
    }

    //readMatrix() just reads the input matrix in the natural way

    public void readMatrix()
    {
        for(int i = 0; i < rows; i ++)
        {
            string[] s = ReadLine().Split();
            for(int j = 0; j < cols; j ++)
            {
                a[i, j] = float.Parse(s[j]);
            } 
        }
    }

    // this function adds two different matrices. It will throw an error if matrices' dimensions are not equal. o/w it produces 
    // a simple algorithm for adding two matrices
    public static Matrix operator + (Matrix a, Matrix b)
    {
        if(a.rows != b.rows || a.cols != b.cols)
        {
            throw new Exception("Invalid data, try again");
        }
        Matrix res = new Matrix(a.rows, a.cols);
        for(int i = 0; i < a.rows; i ++)
        {
            for(int j = 0; j < a.cols; j ++)
            {
                res.a[i, j] = a.a[i, j] + b.a[i, j];
            }
        }
        return res;
    }

    //this function multiplies two matrices. Everything is as in add function, except for algorithm. The algorithm has time o(n^3).
    //One could use Strassen algorithm for matrix multiplication, which has a run-time o(n^2,81), but since Strassen algorithm is 
    // best appliable to square matrices, and assuming that input matrice will be of reasonable size(not very large), there is no 
    //big difference between those algorithms, so I used the algorithm which is easier in implementation.

    public static Matrix operator * (Matrix a, Matrix b)
    {
        if(a.cols != b.rows)
        {
            throw new Exception("Invalid data, try again");
        }
        Matrix res = new Matrix(a.rows, b.cols);
        for(int i = 0; i < a.rows; i ++)
        {
            for(int j = 0; j < b.cols; j ++)
            {
                res.a[i, j] = 0;
                for(int k = 0; k < a.cols; k ++)
                {
                    res.a[i, j] += a.a[i, k] * b.a[k, j];
                }
            }
        }
        return res;
    }

    //The following function finds the determinant of a matrix. It is a recursive algorithm, which finds determinants of matrices of
    //smaller sizes, i.e. n-1, n-2 and so on. If n == 1 is a base of recursion. It starts from the 0-th row and finds determinant
    //using Laplace expansion
    public double det()
    {
        if(rows != cols)
        {
            throw new Exception("Invalid data, Matrix should be squared");
        }
        int n = rows;
        if(n == 1)
        {
            return a[0, 0];
        }
        double det = 0;
        for(int col = 0; col < n; col ++)
        {
            Matrix submatrix = this.temp(0, col);
            double cofactor = a[0, col] * Math.Pow(-1, col) * submatrix.det();
            det += cofactor;
        }
        return det;
    }

    // this private function just counts the cofactor of a submatrix

    private double cofactor(int i, int j, Matrix a)
    {
        return Math.Pow(-1, i + j) * a.det();
    }

    // this function creates a submatrix Ai,j - matrix A without i-th row and j-th column
    // x and y are positions in the matrix and they increase iteratively if values are not in i-th row and j-th column
    private Matrix temp(int n, int m)
    {
        Matrix res = new Matrix(rows - 1, rows - 1);
        int x = 0, y = 0;
        for(int i = 0; i < rows; i ++)
        {
            int flag = 0;
            y = 0;
            for(int j = 0; j < rows; j ++)
            {
                if(i != n && j != m)
                {
                    flag = 1;
                    res.a[x, y] = a[i, j]; 
                    y ++;
                }
            }
            if(flag == 1)
            {
                x ++;
            }
        }
        return res;
    }

    //this function makes a matrix transpose
    private Matrix transpose()
    {
        Matrix res = new Matrix(rows, rows);
        for(int i = 0; i < rows; i ++)
        {
            for(int j = 0; j < rows; j ++)
            {
                res.a[i, j] = a[j, i];
            }
        }
        return res;
    }

    // for the matrix inverse algorithm I have chosen the following: A^(-1) = 1/det(A) * adj(A), where adj(A) is an adjugate matrix
    // and its entries are just transpose of cofactors matrix of A
    public Matrix inverse()
    {
        if(rows != cols)
        {
            throw new Exception("Invalid data, Matrix should be square");
        }
        Matrix help = new Matrix(rows, rows);
        for(int i = 0; i < rows; i ++) //this loop creates a cofactor matrix
        {
            for(int j = 0; j < rows; j ++)
            {
                Matrix copy = this;
                Matrix temp = copy.temp(i, j);
                help.a[i, j] = cofactor(i, j, temp);
            }
        }
        Matrix help1 = help.transpose(); //transpose a cofactor matrix
        Matrix res = new Matrix(rows, rows);
        double dt = this.det();
        for(int i = 0; i < rows; i ++) //find the inverse by dividing each entry of found matrix by det(A)
        {
            for(int j = 0; j < rows; j ++)
            {
                res.a[i, j] = help1.a[i, j] / dt;
            }
        }
        return res;
    }

    //this function just print a matrix to the terminal in a simple form
    public void print()
    {
        for(int i = 0; i < rows; i ++){
            for(int j = 0; j < cols; j ++){
                Write(a[i, j].ToString());
                Write(" ");
            }
            WriteLine();
        }

    }
}

// set of commands: add - defines addition of two matrices, mult - multiplication, inverse - inversion, det - determinant
//if the command is add or mult, programm will politely ask you to write 2 matrices, for other commands you only need 1 matrix
//exit - finishes the program
class MyProg
{
    static void Main()
    {
        bool flag = true;
        int n1 = 0, m1 = 0, n2 = 0, m2 = 0;
        while
            (flag) //in this (possibly infinite) loop I write consequences of each input command. I decided not to try hard on it, so 
            // everything is written in this loops and not separated on functions.
        {
            WriteLine(
                "Type the command which you want to execute(add - for matrix addition, mult - for matrix multiplication, inverse - for matrix inverse, det - for matrix determinant, exit - to terminate the programm): ");
            string? s = ReadLine();
            if (s == "exit" || s == "Exit")
            {
                flag = false;
                break;
            }

            if (s == "add" || s == "Add")
            {
                Write("Please, enter the number of rows and columns of the first matrix: ");
                string[] t = ReadLine().Split();
                for (int i = 0; i < 2; i++)
                {
                    n1 = int.Parse(t[0]);
                    m1 = int.Parse(t[1]);
                }

                WriteLine("Enter the first matrix: ");
                Matrix a = new Matrix(n1, m1);
                a.readMatrix();
                Write("Please, enter the number of rows and columns of the second matrix: ");
                string[] t1 = ReadLine().Split();
                for (int i = 0; i < 2; i++)
                {
                    n2 = int.Parse(t1[0]);
                    m2 = int.Parse(t1[1]);
                }

                WriteLine("Enter the first matrix: ");
                Matrix b = new Matrix(n2, m2);
                b.readMatrix();
                Matrix c = a + b;
                WriteLine("The sum of these matrices is: ");
                c.print();
            }

            if (s == "mult" || s == "Mult")
            {
                Write("Please, enter the number of rows and columns of the first matrix: ");
                string[] t = ReadLine().Split();
                for (int i = 0; i < 2; i++)
                {
                    n1 = int.Parse(t[0]);
                    m1 = int.Parse(t[1]);
                }

                WriteLine("Enter the first matrix: ");
                Matrix a = new Matrix(n1, m1);
                a.readMatrix();
                Write("Please, enter the number of rows and columns of the second matrix: ");
                string[] t1 = ReadLine().Split();
                for (int i = 0; i < 2; i++)
                {
                    n2 = int.Parse(t1[0]);
                    m2 = int.Parse(t1[1]);
                }

                WriteLine("Enter the first matrix: ");
                Matrix b = new Matrix(n2, m2);
                b.readMatrix();
                Matrix c = a * b;
                WriteLine("The sum of these matrices is: ");
                c.print();
            }

            if (s == "inverse" || s == "Inverse")
            {
                Write("Please, enter the number of rows and columns of the matrix: ");
                string[] t = ReadLine().Split();
                for (int i = 0; i < 2; i++)
                {
                    n1 = int.Parse(t[0]);
                    m1 = int.Parse(t[1]);
                }

                Matrix a = new Matrix(n1, m1);
                WriteLine("Enter the matrix: ");
                a.readMatrix();
                if (a.det() == 0)
                {
                    WriteLine("Matrix doesnt have an inverse");
                }
                else
                {
                    WriteLine("The matrix inverse is: ");
                    Matrix c = a.inverse();
                    c.print();
                }
            }

            if (s == "det" || s == "Det")
            {
                Write("Please, enter the number of rows and columns of the matrix: ");
                string[] t = ReadLine().Split();
                for (int i = 0; i < 2; i++)
                {
                    n1 = int.Parse(t[0]);
                    m1 = int.Parse(t[1]);
                }

                Matrix a = new Matrix(n1, m1);
                WriteLine("Enter the matrix: ");
                a.readMatrix();
                WriteLine("The matrix determinant is: ");
                WriteLine(a.det());
            }
        }
    }

}