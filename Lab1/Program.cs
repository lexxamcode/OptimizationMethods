using LabEntities;

var functionExplorer = new FunctionExplorer
{
    Function = x => 1 - Math.Exp(-1*Math.Pow((x-2),4)),
    LeftBorder = -10,
    RightBorder = 15
};
const double epsilon = 1e-4;

Console.WriteLine("Dichotomy Method\n");
Statistic? dichotomyRoot = functionExplorer.DichotomyMethod(epsilon);
Console.WriteLine($"The minimum of the function is ({dichotomyRoot!.ValueX}, {dichotomyRoot.ValueY})");
Console.WriteLine($"In {dichotomyRoot.Iterations} function using");

Console.WriteLine("\n\nGolden Ratio Extremum location\n");
Statistic? goldenRatioExtremum = functionExplorer.GoldenRatio(epsilon);
Console.WriteLine($"The minimum of the function is ({goldenRatioExtremum!.ValueX}, {goldenRatioExtremum.ValueY})");
Console.WriteLine($"In {goldenRatioExtremum.Iterations} function using");

Console.WriteLine("\n\nFibonacci method of finding the extremum\n");
Statistic? fibonacciExtremum = functionExplorer.FibonacciMethod(20);
Console.WriteLine($"The minimum of the function is ({fibonacciExtremum!.ValueX}, {fibonacciExtremum.ValueY})");
Console.WriteLine($"In {fibonacciExtremum.Iterations} function using");

System.Console.ReadKey();