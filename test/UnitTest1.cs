using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using MatrixClass;
using Xunit;

namespace tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var inputBuilder = new StringBuilder();
        inputBuilder.AppendLine("exit");
        var input = new StringReader(inputBuilder.ToString());
        Console.SetIn(input);

        var outputBuilder = new StringBuilder();
        var output = new StringWriter(outputBuilder);
        Console.SetOut(output);

        // Act
        Matrix.main();

        // Assert
        Assert.Contains("Terminating the program", outputBuilder.ToString());
    }

    [Fact]
    public void Test2()
    {
        var input =
            "det\n" + // Command to calculate determinant
            "[2, 2; 1, 0]\n" +
            "exit\n"; // Exit the program

        var expectedOutput =
            "Type the command which you want to execute(add - for matrix addition, mult - for matrix multiplication, inverse - for matrix inverse, det - for matrix determinant, exit - to terminate the programm): \n" +
            "Enter the matrix: \n" +
            "The matrix determinant is: \n" +
            "-2\n";

        using (var consoleInput = new StringReader(input))
        using (var consoleOutput = new StringWriter())
        {
            Console.SetIn(consoleInput);
            Console.SetOut(consoleOutput);

            // Act
            Matrix.main();

            // Assert
            var actualOutput = consoleOutput.ToString();
            Assert.Contains(expectedOutput, actualOutput);
        }
    }

    [Fact]
    public void Test3()
    {
        var input =
            "add\n" + // Command to calculate determinant
            "[2, 2; 1, 1]\n" +
            "[1, 1; 3, 3]\n" +
            "exit\n"; // Exit the program

        var expectedOutput =
            "Type the command which you want to execute(add - for matrix addition, mult - for matrix multiplication, inverse - for matrix inverse, det - for matrix determinant, exit - to terminate the programm): \n" +
            "Enter the first matrix: \n" +
            "Enter the second matrix: \n" +
            "The sum of these matrices is: \n" +
            "3 3 \n4 4 \n";

        using (var consoleInput = new StringReader(input))
        using (var consoleOutput = new StringWriter())
        {
            Console.SetIn(consoleInput);
            Console.SetOut(consoleOutput);

            // Act
            Matrix.main();

            // Assert
            var actualOutput = consoleOutput.ToString();
            Assert.Contains(expectedOutput, actualOutput);
        }
    }
    [Fact]
    public void Test4()
    {
        var input =
            "add\n" + // Command to calculate determinant
            "[aaa, 2; 1, 1]\n" +
            "[2, 2; 1, 1]\n" +
            "[1, 1; 3, 3]\n" +
            "exit\n"; // Exit the program

        var expectedOutput =
            "Type the command which you want to execute(add - for matrix addition, mult - for matrix multiplication, inverse - for matrix inverse, det - for matrix determinant, exit - to terminate the programm): \n" +
            "Enter the first matrix: \n" +
            "Incorrect input. Try again!\n" +
            "Enter the second matrix: \n" +
            "The sum of these matrices is: \n" +
            "3 3 \n4 4 \n";

        using (var consoleInput = new StringReader(input))
        using (var consoleOutput = new StringWriter())
        {
            Console.SetIn(consoleInput);
            Console.SetOut(consoleOutput);

            // Act
            Matrix.main();

            // Assert
            var actualOutput = consoleOutput.ToString();
            Assert.Contains(expectedOutput, actualOutput);
        }
    }
    [Fact]
    public void Test5()
    {
        var input =
            "add\n" + // Command to calculate determinant
            "[aaa, 2; 1, 1]\n" +
            "[2, 2; 1, 1]\n" +
            "[1, 1; 3, 3; 4, 4]\n" +
            "[2, 2; 1, 1]\n" +
            "[1, 1; 3, 3]\n" +
            "exit\n"; // Exit the program

        var expectedOutput =
            "Type the command which you want to execute(add - for matrix addition, mult - for matrix multiplication, inverse - for matrix inverse, det - for matrix determinant, exit - to terminate the programm): \n" +
            "Enter the first matrix: \n" +
            "Incorrect input. Try again!\n" +
            "Enter the second matrix: \n" +
            "the sum is undefined. Try again!\n" +
            "Enter the first matrix: \n" +
            "Enter the second matrix: \n" +
            "The sum of these matrices is: \n" +
            "3 3 \n4 4 \n";

        using (var consoleInput = new StringReader(input))
        using (var consoleOutput = new StringWriter())
        {
            Console.SetIn(consoleInput);
            Console.SetOut(consoleOutput);

            // Act
            Matrix.main();

            // Assert
            var actualOutput = consoleOutput.ToString();
            Assert.Contains(expectedOutput, actualOutput);
        }
    }
    [Fact]
    public void Test6()
    {
        var input =
            "mult\n" + // Command to calculate determinant
            "[9, 5, 2; 1, 8, 5; 3, 1, 6]\n" +
            "[3, 2; 1, 4; 5, 3]\n" +
            "exit\n"; // Exit the program

        var expectedOutput =
            "Type the command which you want to execute(add - for matrix addition, mult - for matrix multiplication, inverse - for matrix inverse, det - for matrix determinant, exit - to terminate the programm): \n" +
            "Enter the first matrix: \n" +
            "Enter the second matrix: \n" +
            "The mult of these matrices is: \n" +
            "42 44 \n36 49 \n40 28";

        using (var consoleInput = new StringReader(input))
        using (var consoleOutput = new StringWriter())
        {
            Console.SetIn(consoleInput);
            Console.SetOut(consoleOutput);

            // Act
            Matrix.main();

            // Assert
            var actualOutput = consoleOutput.ToString();
            Assert.Contains(expectedOutput, actualOutput);
        }
    }
    [Fact]
    public void Test7()
    {
        var input =
            "mult\n" + // Command to calculate determinant
            "[aaaa\n" +
            "[9, 5, 2; 1, 8, 5; 3, 1, 6]\n" +
            "[3, 2; 1, 4; 5, 3]\n" +
            "exit\n"; // Exit the program

        var expectedOutput =
            "Type the command which you want to execute(add - for matrix addition, mult - for matrix multiplication, inverse - for matrix inverse, det - for matrix determinant, exit - to terminate the programm): \n" +
            "Enter the first matrix: \n" +
            "Incorrect input. Try again!\n" +
            "Enter the second matrix: \n" +
            "The mult of these matrices is: \n" +
            "42 44 \n36 49 \n40 28";

        using (var consoleInput = new StringReader(input))
        using (var consoleOutput = new StringWriter())
        {
            Console.SetIn(consoleInput);
            Console.SetOut(consoleOutput);

            // Act
            Matrix.main();

            // Assert
            var actualOutput = consoleOutput.ToString();
            Assert.Contains(expectedOutput, actualOutput);
        }
    }
    [Fact]
    public void Test8()
    {
        var input =
            "mult\n" + // Command to calculate determinant
            "[aaaa\n" +
            "[9, 5, 2; 1, 8, 5; 3, 1, 6]\n" +
            "[3, 2; 1, 4; 5, 3; 1, 1]\n" +
            "[9, 5, 2; 1, 8, 5; 3, 1, 6]\n" +
            "[3, 2; 1, 4; 5, 3]\n" +
            "exit\n"; // Exit the program

        var expectedOutput =
            "Type the command which you want to execute(add - for matrix addition, mult - for matrix multiplication, inverse - for matrix inverse, det - for matrix determinant, exit - to terminate the programm): \n" +
            "Enter the first matrix: \n" +
            "Incorrect input. Try again!\n" +
            "Enter the second matrix: \n" +
            "multiplication is undefined. Try again!\n" +
            "Enter the first matrix: \n" +
            "Enter the second matrix: \n" +
            "The mult of these matrices is: \n" +
            "42 44 \n36 49 \n40 28";

        using (var consoleInput = new StringReader(input))
        using (var consoleOutput = new StringWriter())
        {
            Console.SetIn(consoleInput);
            Console.SetOut(consoleOutput);

            // Act
            Matrix.main();

            // Assert
            var actualOutput = consoleOutput.ToString();
            Assert.Contains(expectedOutput, actualOutput);
        }
    }
    [Fact]
    public void Test9()
    {
        var input =
            "inverse\n" + // Command to calculate determinant
            "[aaaa\n" +
            "[1, 2, 3; 0, 1, 4; 0, 0, 1]\n" +
            "exit\n"; // Exit the program

        var expectedOutput =
            "Type the command which you want to execute(add - for matrix addition, mult - for matrix multiplication, inverse - for matrix inverse, det - for matrix determinant, exit - to terminate the programm): \n" +
            "Enter the matrix: \n" +
            "Incorrect input. Try again!\n" +
            "Enter the matrix: \n" +
            "The matrix inverse is: \n" +
            "1 -2 5 \n0 1 -4 \n0 0 1";

        using (var consoleInput = new StringReader(input))
        using (var consoleOutput = new StringWriter())
        {
            Console.SetIn(consoleInput);
            Console.SetOut(consoleOutput);

            // Act
            Matrix.main();

            // Assert
            var actualOutput = consoleOutput.ToString();
            Assert.Contains(expectedOutput, actualOutput);
        }
    }
    [Fact]
    public void Test10()
    {
        var input =
            "inverse\n" + // Command to calculate determinant
            "[aaaa\n" +
            "[1, 2; 3, 4; 5, 6]\n" +
            "[1, 2, 3; 0, 1, 4; 0, 0, 1]\n" +
            "exit\n"; // Exit the program

        var expectedOutput =
            "Type the command which you want to execute(add - for matrix addition, mult - for matrix multiplication, inverse - for matrix inverse, det - for matrix determinant, exit - to terminate the programm): \n" +
            "Enter the matrix: \n" +
            "Incorrect input. Try again!\n" +
            "Enter the matrix: \n" +
            "Incorrect input. Try again!\n" +
            "Enter the matrix: \n" +
            "The matrix inverse is: \n" +
            "1 -2 5 \n0 1 -4 \n0 0 1";

        using (var consoleInput = new StringReader(input))
        using (var consoleOutput = new StringWriter())
        {
            Console.SetIn(consoleInput);
            Console.SetOut(consoleOutput);

            // Act
            Matrix.main();

            // Assert
            var actualOutput = consoleOutput.ToString();
            Assert.Contains(expectedOutput, actualOutput);
        }
    }
    [Fact]
    public void Test11()
    {
        var input =
            "inverse\n" + // Command to calculate determinant
            "[aaaa\n" +
            "[1, 2; 3, 4; 5, 6]\n" +
            "[1, 2, 3; 4, 5, 6; 7, 8, 9]\n" +
            "exit\n"; // Exit the program

        var expectedOutput =
            "Type the command which you want to execute(add - for matrix addition, mult - for matrix multiplication, inverse - for matrix inverse, det - for matrix determinant, exit - to terminate the programm): \n" +
            "Enter the matrix: \n" +
            "Incorrect input. Try again!\n" +
            "Enter the matrix: \n" +
            "Incorrect input. Try again!\n" +
            "Enter the matrix: \n" +
            "Matrix doesnt have an inverse\n";

        using (var consoleInput = new StringReader(input))
        using (var consoleOutput = new StringWriter())
        {
            Console.SetIn(consoleInput);
            Console.SetOut(consoleOutput);

            // Act
            Matrix.main();

            // Assert
            var actualOutput = consoleOutput.ToString();
            Assert.Contains(expectedOutput, actualOutput);
        }
    }
    [Fact]
    public void Test12()
    {
        var input =
            "det\n" + // Command to calculate determinant
            "[aaaa\n" +
            "[1, 2; 3, 4; 5, 6]\n" +
            "[1, 2, 3; 4, 5, 6; 7, 8, 9]\n" +
            "exit\n"; // Exit the program

        var expectedOutput =
            "Type the command which you want to execute(add - for matrix addition, mult - for matrix multiplication, inverse - for matrix inverse, det - for matrix determinant, exit - to terminate the programm): \n" +
            "Enter the matrix: \n" +
            "Incorrect input. Try again!\n" +
            "Enter the matrix: \n" +
            "Incorrect input. Try again!\n" +
            "Enter the matrix: \n" +
            "The matrix determinant is: \n" +
            "0\n";

        using (var consoleInput = new StringReader(input))
        using (var consoleOutput = new StringWriter())
        {
            Console.SetIn(consoleInput);
            Console.SetOut(consoleOutput);

            // Act
            Matrix.main();

            // Assert
            var actualOutput = consoleOutput.ToString();
            Assert.Contains(expectedOutput, actualOutput);
        }
    }
}