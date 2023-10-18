using LabEntities;

namespace Lab1;

public static class Program
{
    public static void Main(string[] arg)
    {
        var functionRootFinder = new FunctionRootFinder
        {
            Function = x => 4 - Math.Exp(x) - 2 * Math.Pow(x, 2),
            LeftBorder = 0,
            RightBorder = 10
        };
        const double epsilon = 1e-2;
        
        Statistic? dichotomyRoot = functionRootFinder.DichotomyMethod(epsilon);
        Statistic? goldenRatioExtremum = functionRootFinder.GoldenRatio(epsilon, "min");
        Statistic? fibonacciExtremum = functionRootFinder.FibonacciMethod(30);

        if (dichotomyRoot is null || goldenRatioExtremum is null || fibonacciExtremum is null)
        {
            Console.WriteLine("Function is not set or incorrect borders");
            return;
        }
        
        Console.WriteLine("Dichotomy Method");
        Console.WriteLine($"The root of the function is ({dichotomyRoot.ValueX}, {dichotomyRoot.ValueY})");
        Console.WriteLine($"In {dichotomyRoot.Iterations} function using");
        
        Console.WriteLine("Golden Ratio Extremum location");
        Console.WriteLine($"The extremum of the function is ({goldenRatioExtremum.ValueX}, {goldenRatioExtremum.ValueY})");
        Console.WriteLine($"In {goldenRatioExtremum.Iterations} function using");
        
        Console.WriteLine("Fibonacci method of finding the extremum");
        Console.WriteLine($"The extremum of the function is ({fibonacciExtremum.ValueX}, {fibonacciExtremum.ValueY})");
        Console.WriteLine($"In {fibonacciExtremum.Iterations} function using");
    }
}