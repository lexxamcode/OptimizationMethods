namespace LabEntities;

public class FunctionExplorer
{
    public Func<double, double>? Function { get; set; }
    public double LeftBorder { get; set; }
    public double RightBorder { get; set; }
    
    public Statistic? DichotomyMethod(double epsilon)
    {
        if (Function == null)
            return null;

        var methodStatistic = new Statistic();
        var leftBorder = LeftBorder;
        var rightBorder = RightBorder;

        do
        {
            var middle = (leftBorder + rightBorder) / 2;
            var firstFunction = Function(middle - epsilon / 100);
            var secondFunction = Function(middle + epsilon / 100);
            methodStatistic.Iterations += 2;

            if (firstFunction < secondFunction)
                rightBorder = middle + epsilon / 100;
            else
                leftBorder = middle - epsilon / 100;

        } while (Math.Abs(rightBorder - leftBorder) > 2*epsilon);
        
        methodStatistic.ValueX = (rightBorder + leftBorder) / 2;
        methodStatistic.ValueY = Function(methodStatistic.ValueX);
        return methodStatistic;
    }

    public Statistic? GoldenRatio(double epsilon)
    {
        if (Function == null)
            return null;

        var methodStatistic = new Statistic();
        
        var phi = 0.5 * (1 + Math.Sqrt(5));
        var leftBorder = LeftBorder;
        var rightBorder = RightBorder;

        var x1 = rightBorder - (rightBorder - leftBorder) / phi;
        var x2 = leftBorder + (rightBorder - leftBorder) / phi;

        var y1 = Function(x1);
        var y2 = Function(x2);
        methodStatistic.Iterations += 2;
        
        while (Math.Abs(rightBorder - leftBorder) > 2 * epsilon)
        {
            if (y1 >= y2)
            {
                leftBorder = x1;
                x1 = x2;
                x2 = leftBorder + (rightBorder - leftBorder) / phi;
                y1 = y2;
                y2 = Function(x2);
            }
            else
            {
                rightBorder = x2;
                x2 = x1;
                x1 = rightBorder - (rightBorder - leftBorder) / phi;
                y2 = y1;
                y1 = Function(x1);
            }

            methodStatistic.Iterations++;
        }

        methodStatistic.ValueX = (leftBorder + rightBorder) / 2;
        methodStatistic.ValueY = Function(methodStatistic.ValueX);
        return methodStatistic;
    }

    private static uint Fibonacci(uint n)
    {
        if (n < 2)
            return n;
        else
            return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    public Statistic? FibonacciMethod(uint iterations)
    {
        if (Function == null)
            return null;
        
        var leftBorder = LeftBorder;
        var rightBorder = RightBorder;

        var x1 = leftBorder + (rightBorder - leftBorder) * Fibonacci(iterations - 2) / Fibonacci(iterations);
        var x2 = leftBorder + (rightBorder - leftBorder) * Fibonacci(iterations - 1) / Fibonacci(iterations);

        var methodStatistic = new Statistic();
        
        var y1 = Function(x1);
        var y2 = Function(x2);
        methodStatistic.Iterations += 2;
        
        do
        {
            iterations -= 1;
            if (y1 > y2)
            {
                leftBorder = x1;
                x1 = x2;
                x2 = rightBorder - (x1 - leftBorder);
                y1 = y2;
                y2 = Function(x2);
            }
            else
            {
                rightBorder = x2;
                x2 = x1;
                x1 = leftBorder + (rightBorder - x2);
                y2 = y1;
                y1 = Function(x1);
            }
            
            Console.WriteLine($"left: {leftBorder}; right: {rightBorder}; x1: {x1}; x2: {x2};");
            methodStatistic.Iterations += 1;
        } while (iterations != 1);

        methodStatistic.ValueX = (x1 + x2) / 2;
        methodStatistic.ValueY = Function(methodStatistic.ValueX);
        return methodStatistic;
    }
}