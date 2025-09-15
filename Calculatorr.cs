namespace ConsoleApp;

public class Calculator
{
    private double _memory;
    
    private const double MaxInput = 1e100;
    private const double MinInput = -1e100;
    private const double MaxSquareInput = 1e50;
    
    private static void ValidateValue(double value)
    {
        if (double.IsNaN(value))
            throw new ArgumentException("Введено нечисловое значение.");
        
        if (double.IsInfinity(value))
            throw new ArgumentException("Превышен допустимый диапазон значений.");
        
        if (value > MaxInput || value < MinInput)
            throw new ArgumentException($"Диапазон значений: {MinInput:E} - {MaxInput:E}.");
    }
    
    private static void CheckResult(double result, string operation)
    {
        if (double.IsNaN(result))
            throw new InvalidOperationException($"Операция {operation} не может быть выполнена.");
        
        if (double.IsInfinity(result))
            throw new OverflowException($"Результат операции {operation} слишком велик.");
    }
    
    private double Calculate(Func<double> operation, string operationName, params double[] values)
    {
        foreach (var value in values)
            ValidateValue(value);
            
        var result = operation();
        CheckResult(result, operationName);
        return result;
    }
    
    private void ExecuteMemoryAction(Action operation, string operationName, params double[] values)
    {
        foreach (var value in values)
            ValidateValue(value);
            
        operation();
    }

    public double Add(double first, double second) =>
        Calculate(() => first + second, "сложение", first, second);
    
    public double Subtract(double first, double second) =>
        Calculate(() => first - second, "вычитание", first, second);
    
    public double Multiply(double first, double second) =>
        Calculate(() => first * second, "умножение", first, second);
    
    public double Divide(double dividend, double divisor) =>
        Calculate(() => {
            if (divisor == 0)
                throw new DivideByZeroException("Деление на ноль невозможно.");
            return dividend / divisor;
        }, "деление", dividend, divisor);
    
    public double Percent(double value, double percent) =>
        Calculate(() => (value * percent) / 100, "процент", value, percent);
    
    public double Reciprocal(double value) =>
        Calculate(() => {
            if (value == 0)
                throw new DivideByZeroException("Обратное значение для нуля не существует.");
            return 1 / value;
        }, "обратное число", value);
    
    public double Square(double value) =>
        Calculate(() => {
            if (Math.Abs(value) > MaxSquareInput)
                throw new ArgumentException($"Максимальное значение для возведения в квадрат: {MaxSquareInput:E}");
            return value * value;
        }, "квадрат", value);
    
    public double SquareRoot(double value) =>
        Calculate(() => {
            if (value < 0)
                throw new ArgumentException("Квадратный корень из отрицательного числа не извлекается.");
            return Math.Sqrt(value);
        }, "квадратный корень", value);
    
    public void AddToMemory(double value) =>
        ExecuteMemoryAction(() => {
            var newMemoryValue = _memory + value;
            CheckResult(newMemoryValue, "память +");
            _memory = newMemoryValue;
        }, "память +", value);
    
    public void SubtractFromMemory(double value) =>
        ExecuteMemoryAction(() => {
            var newMemoryValue = _memory - value;
            CheckResult(newMemoryValue, "память -");
            _memory = newMemoryValue;
        }, "память -", value);
    
    public double GetMemory() => _memory;
    
    public void ResetMemory() => _memory = 0;
}