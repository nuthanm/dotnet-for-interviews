//#region Unhandled-exception
// Question on Unhandled Exception
//////var numerator = 10;
//////var denominator = 0;

//////var result = numerator / denominator;
//////Console.WriteLine($"Result: {result}");

///*
//Output:
//Unhandled exception. System.DivideByZeroException: Attempted to divide by zero.
//   at Program.<Main>$(String[] args) in C: \Users\NuthanMurarysetty\Source\Repos\dotnet-for-interviews\dotnet-for-interviews\Questions-on-Exceptions\Program.cs:line 6

//Stops the execution of the program and prints the exception details to the console.
//To fix this abnormal termination, you can handle the exception using a try-catch block.
//*/

//#endregion

#region Handled-exception
//////var numeratorHandled = 10;
//////var denominatorHandled = 0;
//////try
//////{
//////    var resultHandled = numeratorHandled / denominatorHandled;
//////    Console.WriteLine($"Result: {resultHandled}");
//////}
//////catch (DivideByZeroException ex)
//////{
//////    Console.WriteLine($"Error: {ex.Message}");
//////}

// Output:
// Error: Attempted to divide by zero.
#endregion

////numeratorHandled = 10;
////denominatorHandled = 0;
////try
////{
////    var resultHandled = numeratorHandled / denominatorHandled;
////    Console.WriteLine($"Result: {resultHandled}");
////}
////catch (Exception ex)
////{
////    Console.WriteLine($"Error: {ex.Message}");
////}
////catch (DivideByZeroException ex)
////{
////    Console.WriteLine($"Error: {ex.Message}");
////}

/*
 * Output:
 * A previous catch clause already catches all exceptions of this or of a super type ('Exception') * 
 */


//////try
//////{
//////    // This block contains code that may throw an exception.
//////    Console.WriteLine("Trying to divide by zero...");
//////    var numeratorTry = 10;
//////    var denominatorTry = 0;
//////    var resultTry = numeratorTry / denominatorTry; // This will throw DivideByZeroException
//////    Console.WriteLine($"Result: {resultTry}");
//////}
//////finally
//////{
//////    // This block will always execute, regardless of whether an exception was thrown or caught.
//////    Console.WriteLine("Finally block executed.");
//////}

/*
 *Output:
 * Trying to divide by zero...
   Unhandled exception. System.DivideByZeroException: Attempted to divide by zero.
   at Program.<Main>$(String[] args) in C:\Users\NuthanMurarysetty\Source\Repos\dotnet-for-interviews\dotnet-for-interviews\Questions-on-Exceptions\Program.cs:line 65
  
   Note: Coding is accepting without any exception handling, so the program will terminate with an unhandled exception.
 */


//////try
//////{
//////    Console.WriteLine("Performing calculation...");
//////    return 10 / 2;
//////}
//////finally
//////{
//////    Console.WriteLine(
//////        "Finally block executed before returning value.");
//////}

// Output:
// Performing calculation...
// Finally block executed before returning value.


//try
//{
//    try
//    {
//        throw new Exception("Inner exception");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine("Caught exception: " + ex.Message);
//        throw;  // Re-throw the exception
//    }
//}
//finally
//{
//    Console.WriteLine("Finally block executed after exception re-throw.");
//}

#region Program-1
//var result = DivideNumber(10, 0);
//Console.WriteLine($"Result of division: {result}");

//int DivideNumber(int a, int b)
//{
//    try
//    {
//        return a / b;
//    }
//    catch (DivideByZeroException ex)
//    {
//        Console.WriteLine("Cannot divide by zero");
//        return 0;
//    }
//}
#endregion

#region Program-2

////Console.WriteLine("Enter two integers to divide:");
////int num1 = 0;
////int num2 = 0;
////bool isValidInput = false;

////do
////{

////    Console.WriteLine("Enter the first integer:");
////    isValidInput = int.TryParse(Console.ReadLine(), out num1);

////    if (!isValidInput)
////    {
////        Console.WriteLine("Invalid input. Please enter a valid integer.");
////        continue;
////    }

////    Console.WriteLine("Enter the second integer:");
////    isValidInput = int.TryParse(Console.ReadLine(), out num2);
////    if (!isValidInput)
////    {
////        Console.WriteLine("Invalid input. Please enter a valid integer.");
////        continue;
////    }

////} while (!isValidInput);

////result = DivideNumber(num1, num2);
////Console.WriteLine($"Result of division: {result}");
#endregion

#region Program-3

////int ParseAndDivide(string num1, string num2)
////{
////    try
////    {
////        int number1 = int.Parse(num1);
////        int number2 = int.Parse(num2);
////        return number1 / number2;
////    }
////    catch (FormatException ex)
////    {
////        Console.WriteLine($"Format error: {ex.Message}");
////        return 0; // or throw a custom exception
////    }
////    catch (DivideByZeroException ex)
////    {
////        Console.WriteLine($"Division by zero error: {ex.Message}");
////        return 0; // or throw a custom exception
////    }
////    catch (Exception ex)
////    {
////        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
////        return 0; // or throw a custom exception
////    }
////}

////string input1 = "10";
////string input2 = "0"; // Change this to a valid number to test successful division

////int result = ParseAndDivide(input1, input2);
////Console.WriteLine($"Result of division: {result}");

////string input3 = "abc"; // Invalid input to test FormatException
////string input4 = "5"; // Valid input
////int result2 = ParseAndDivide(input3, input4);
////Console.WriteLine($"Result of division with invalid input: {result2}");

#endregion

try
{
    Console.WriteLine("Trying to divide by zero...");
    Environment.Exit(0); // This will terminate the program immediately
}
finally
{
    Console.WriteLine("Finally block executed.");
    throw new Exception("finally exception");
}