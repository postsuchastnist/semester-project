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

    public void Matrixread(double[,] s)
    {
        for(int i = 0; i < rows; i ++)
        {
            for(int j = 0; j < cols; j ++)
            {
                a[i, j] = s[i, j];
            }
        }
    }
    // this function adds two different matrices. It will throw an error if matrices' dimensions are not equal. o/w it produces 
    // a simple algorithm for adding two matrices
    public static Matrix operator + (Matrix a, Matrix b)
    {
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
        int n = rows;
        double det = 1;
        for(int col = 0; col < n; col ++)
        {
            int swapRow = col;
            for(int row = col + 1; row < n; row ++)
            {
                if(Math.Abs(a[row, col]) > Math.Abs(a[swapRow, col]))
                {
                    swapRow = row;
                }
            }
            if(Math.Abs(a[swapRow, col]) == 0)
            {
                return 0;
            }
            if(swapRow != col)
            {
                for(int k = 0; k < n; k ++)
                {
                    double temp = a[col, k];
                    a[col, k] = a[swapRow, k];
                    a[swapRow, k] = temp;
                }
                det *= -1;
            }
            for(int row = col + 1; row < n; row ++)
            {
                double factor = a[row, col] / a[col, col];
                for(int k = col; k < n; k ++)
                {
                    a[row, k] -= factor * a[col, k];
                }
            }
            det *= a[col, col];
        }
        if(Math.Abs(det) < 0.0000000005)
        {    
            det = 0;
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

    
    static double[,] matlabinput(out int n, out int m, out int flag)
    {
        flag = 0;
        n = 0;
        m = 0;
        string[] s = ReadLine().Split('[', ']');
        if (s.Length != 3)
        {
            double[,] rs = { { 0 } };
            flag = 1;
            return rs;
        }
        string[] s1 = s[1].Split(';');
        n = s1.Count();
        m = s1[0].Split(',').Count();
        double[,] res = new double[n, m];
        for(int i = 0; i < n; i ++)
        {
            string[] temp = s1[i].Split(',');
            if (temp.Length != m)
            {
                double[,] rs = { { 0 } };
                flag = 1;
                return rs;
            }
            for(int j = 0; j < m; j ++)
            {
                if (double.TryParse(temp[j], out double k) == false)
                {
                    flag = 1;
                    res[i, j] = k;
                }
                else
                {
                    res[i, j] = double.Parse(temp[j]);
                }
            }
        }
        return res;
    }
    
    
    
    static void Add()
    {
        WriteLine("Enter the first matrix: ");
        double[,] input = matlabinput(out int n, out int m, out int fl);
        if (fl == 1)
        {
            while (fl == 1)
            {
                WriteLine("Incorrect input. Try again!");
                input = matlabinput(out int nq, out int mq, out int flq);
                fl = flq;
                n = nq;
                m = mq;
            }
        }
        Matrix a = new Matrix(n, m);
        a.Matrixread(input);
        WriteLine("Enter the second matrix: ");
        double[,] input1 = matlabinput(out int n1, out int m1, out int fl1);
        if (fl1 == 1)
        {
            while (fl1 == 1)
            {
                WriteLine("Incorrect input. Try again!");
                input1 = matlabinput(out int nq, out int mq, out int flq);
                fl1 = flq;
                n1 = nq;
                m1 = mq;
            }
        }
        Matrix b = new Matrix(n1, m1);
        b.Matrixread(input1);
        if(n != n1 || m != m1)
        {
            WriteLine("the sum is undefined. Try again!");
            Add();
        }
        else
        {
            Matrix c = a + b; 
            WriteLine("The sum of these matrices is: "); 
            c.print();
        }
    }


    static void Mult()
    {
        WriteLine("Enter the first matrix: ");
        double[,] input = matlabinput(out int n, out int m, out int fl);
        if (fl == 1)
        {
            while (fl == 1)
            {
                WriteLine("Incorrect input. Try again!");
                input = matlabinput(out int nq, out int mq, out int flq);
                fl = flq;
                n = nq;
                m = mq;
            }
        }
        Matrix a = new Matrix(n, m);
        a.Matrixread(input);
        WriteLine("Enter the second matrix: ");
        double[,] input1 = matlabinput(out int n1, out int m1, out int fl1);
        if (fl1 == 1)
        {
            while (fl1 == 1)
            {
                WriteLine("Incorrect input. Try again!");
                input1= matlabinput(out int nq, out int mq, out int flq);
                fl1 = flq;
                n1 = nq;
                m1 = mq;
            }
        }
        Matrix b = new Matrix(n1, m1);
        b.Matrixread(input1);
        if(m != n1)
        {
            WriteLine("multiplication is undefined. Try again!");
            Mult();
        }
        else
        {
            Matrix c = a * b;
            WriteLine("The mult of these matrices is: ");
            c.print();
        }
    }



    static void Inv()
    {
        WriteLine("Enter the matrix: ");
        double[,] input = matlabinput(out int n, out int m, out int fl);
        if (fl == 1 || n != m)
        { 
            WriteLine("Incorrect input. Try again!");
            Inv();
        }
        else
        {
            Matrix a = new Matrix(n, m);
            a.Matrixread(input);
            if(a.det() == 0)
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
    }


    static void Det()
    {
        WriteLine("Enter the matrix: ");
        double[,] input = matlabinput(out int n, out int m, out int fl);
        if (fl == 1 || n != m)
        {
            WriteLine("Incorrect input. Try again!");
            Det();
        }
        else
        {
            Matrix a = new Matrix(n, m);
            a.Matrixread(input);
            WriteLine("The matrix determinant is: "); 
            WriteLine(a.det());
        }
    }
    
    
    
    static void Main()
    {
        WriteLine("1 1 \n1 1");
        bool flag = true;
        while(flag) //in this (possibly infinite) loop I write consequences of each input command. I decided not to try hard on it, so 
        // everything is written in this loops and not separated on functions.
        {
            WriteLine("Type the command which you want to execute(add - for matrix addition, mult - for matrix multiplication, inverse - for matrix inverse, det - for matrix determinant, exit - to terminate the programm): ");
            string? s = ReadLine();
            if(s == "exit" || s == "Exit")
            {
                flag = false;
                break;
            }
            if(s == "add" || s == "Add")
            {
                Add();
            }
            if(s == "mult" || s == "Mult")
            {
                Mult();
            }
            if(s == "inverse" || s == "Inverse")
            {
                Inv();
            }

            if (s == "det" || s == "Det")
            {   
                Det();
            }
        }
    }

}