using static System.Double;

namespace LabEntities;

public class FunctionRootFinder
{
    public Func<double, double>? Function { get; set; }
    public double LeftBorder { get; set; }
    public double RightBorder { get; set; }
    
    public Statistic? DichotomyMethod(double epsilon)
    {
        if ((LeftBorder * RightBorder > 0) || Function == null)
            return null;

        var methodStatistic = new Statistic();
        
        var leftBorder = LeftBorder;
        var rightBorder = RightBorder;
        var middle = NaN;
        
        while (Math.Abs(rightBorder - leftBorder) > epsilon)
        {
            middle = (rightBorder + leftBorder) / 2;

            if (Function(rightBorder) * Function(middle) < 0)
                leftBorder = middle;
            else
                rightBorder = middle;

            methodStatistic.Iterations += 2;
        }

        methodStatistic.ValueX = middle;
        methodStatistic.ValueY = Function(methodStatistic.ValueX);
        return methodStatistic;
    }

    public Statistic? GoldenRatio(double epsilon, string extr)
    {
        if (Function == null)
            return null;

        var methodStatistic = new Statistic();
        
        var phi = 0.5 * (1 + Math.Sqrt(5));
        var leftBorder = LeftBorder;
        var rightBorder = RightBorder;

        while (Math.Abs(rightBorder - leftBorder) > epsilon)
        {
            var x1 = rightBorder - (rightBorder - leftBorder) / phi;
            var x2 = leftBorder + (rightBorder - leftBorder) / phi;

            var y1 = Function(x1);
            var y2 = Function(x2);
            methodStatistic.Iterations += 2;
            
            if (extr == "min")
            {
                if (y1 >= y2)
                    leftBorder = x1;
                else
                    rightBorder = x2;
            }
            else
            {
                if (y1 <= y2)
                    leftBorder = x1;
                else
                    rightBorder = x2; 
            }
            
        }

        methodStatistic.ValueX = (leftBorder + rightBorder) / 2;
        methodStatistic.ValueY = Function(methodStatistic.ValueX);
        return methodStatistic;
    }

    private uint Fibonacci(uint n)
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

            methodStatistic.Iterations += 1;
        } while (iterations != 1);

        methodStatistic.ValueX = (x1 + x2) / 2;
        methodStatistic.ValueY = Function(methodStatistic.ValueX);
        return methodStatistic;
    }
}