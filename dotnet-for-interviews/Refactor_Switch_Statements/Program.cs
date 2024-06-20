// See https://aka.ms/new-console-template for more information
using Refactor_Switch_Statements.Extensions;

Console.WriteLine("Better Way to make switch statements");

MultipleCases(10);
MultipleCasesWithWhenMethod(35);
MultipleCasesWithExtensions(30);
MultipleCasesWithLogicalPattern(24);
MultipleCasesWithRelationalPattern(25);

#region TraditioanlWay_Switch
void MultipleCases(int number)
{
    string result = string.Empty;
    switch (number)
    {
        case 10:
        case 11:
        case 12:
            result = "Numbers less than 20";
            break;
        case 30:
        case 33:
        case 35:
            result = "Numbers between 30 and 40";
            break;
        default:
            result = "Other numbers";
            break;
    }

    Console.WriteLine(result);
}
#endregion


#region Switch_With_When_Method
void MultipleCasesWithWhenMethod(int number)
{
    string result = string.Empty;
    switch (number)
    {
        case int n when (n > 10 & n < 20):
            result = "Numbers less than 20";
            break;
        case int n when (n >= 30 & n < 40):
            result = "Numbers between 30 and 40";
            break;
        default:
            result = "Other numbers";
            break;
    }

    Console.WriteLine(result);
}
#endregion


#region SwitchWithExtensions
void MultipleCasesWithExtensions(int number)
{
    var result = number switch
    {
        var x when x.In(10, 11, 12) => "Numbers between 10 and 20",
        var x when x.In(20, 21, 22) => "Numbers between 20 and 30",
        _ => "Numbers not in our range"
    };

    Console.WriteLine(result);
}
#endregion


#region SwitchWithLogicalPattern
void MultipleCasesWithLogicalPattern(int number)
{
    var result = number switch
    {
        10 or 11 or 12 => "Numbers between 10 and 20",
        20 or 21 or 22 => "Numbers between 20 and 30",
        _ => "Numbers not in our range"
    };

    Console.WriteLine(result);
}
#endregion


#region SwitchWithRelationalPattern
void MultipleCasesWithRelationalPattern(int number)
{
    var result = number switch
    {
        (>= 10) and (<= 20) => "Numbers between 10 and 20",
        (>= 20) and (<= 30) => "Numbers between 20 and 30",
        _ => "Numbers not in our range"
    };

    Console.WriteLine(result);
}
#endregion