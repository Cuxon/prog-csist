namespace ConsoleApp;

class Program
{
    static void Main()
    {
        var calculator = new Calculator();
        var isRunning = true;

        while (isRunning)
        {
            DisplayMenu(calculator);
            
            try
            {
                var choice = Console.ReadLine();
                var result = 0.0;

                switch (choice)
                {
                    case "1":
                        result = ExecuteBinaryOperation(calculator.Add, "+");
                        break;
                    case "2":
                        result = ExecuteBinaryOperation(calculator.Subtract, "-");
                        break;
                    case "3":
                        result = ExecuteBinaryOperation(calculator.Multiply, "*");
                        break;
                    case "4":
                        result = ExecuteBinaryOperation(calculator.Divide, "/");
                        break;
                    case "5":
                        result = ExecuteBinaryOperation(calculator.Percent, "%");
                        break;
                    case "6":
                        result = ExecuteUnaryOperation(calculator.Reciprocal, "1/x");
                        break;
                    case "7":
                        result = ExecuteUnaryOperation(calculator.Square, "x²");
                        break;
                    case "8":
                        result = ExecuteUnaryOperation(calculator.SquareRoot, "√x");
                        break;
                    case "9":
                        ExecuteMemoryAction(calculator.AddToMemory, "M+");
                        continue;
                    case "10":
                        ExecuteMemoryAction(calculator.SubtractFromMemory, "M-");
                        continue;
                    case "11":
                        Console.WriteLine($"Текущее значение памяти: {calculator.GetMemory()}");
                        break;
                    case "12":
                        calculator.ResetMemory();
                        Console.WriteLine("Память очищена");
                        break;
                    case "0":
                        isRunning = false;
                        continue;
                    default:
                        Console.WriteLine("Неверная команда!");
                        break;
                }

                if (ShouldDisplayResult(choice))
                {
                    Console.WriteLine($"Результат: {result}");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"Ошибка: {error.Message}");
            }

            if (isRunning)
            {
                WaitForUserInput();
            }
        }
    }

    static void DisplayMenu(Calculator calculator)
    {
        Console.Clear();
        Console.WriteLine("=== КАЛЬКУЛЯТОР ===");
        Console.WriteLine("1. Сложение (+)");
        Console.WriteLine("2. Вычитание (-)");
        Console.WriteLine("3. Умножение (*)");
        Console.WriteLine("4. Деление (/)");
        Console.WriteLine("5. Процент (%)");
        Console.WriteLine("6. Обратное число (1/x)");
        Console.WriteLine("7. Квадрат (x²)");
        Console.WriteLine("8. Квадратный корень (√x)");
        Console.WriteLine("9. Память + (M+)");
        Console.WriteLine("10. Память - (M-)");
        Console.WriteLine("11. Вызов памяти (MR)");
        Console.WriteLine("12. Очистка памяти (MC)");
        Console.WriteLine("0. Выход");
        Console.WriteLine($"Текущая память: {calculator.GetMemory()}");
        Console.Write("\nВыберите операцию: ");
    }

    static double ExecuteBinaryOperation(Func<double, double, double> operation, string symbol)
    {
        Console.Write("Введите первое число: ");
        var firstNumber = ReadNumber();
        Console.Write("Введите второе число: ");
        var secondNumber = ReadNumber();
        return operation(firstNumber, secondNumber);
    }

    static double ExecuteUnaryOperation(Func<double, double> operation, string symbol)
    {
        Console.Write("Введите число: ");
        var number = ReadNumber();
        return operation(number);
    }

    static void ExecuteMemoryAction(Action<double> operation, string symbol)
    {
        Console.Write("Введите число: ");
        var value = ReadNumber();
        operation(value);
        Console.WriteLine($"Операция {symbol} выполнена успешно");
    }

    static double ReadNumber()
    {
        while (true)
        {
            try
            {
                return double.Parse(Console.ReadLine() ?? "0");
            }
            catch
            {
                Console.Write("Некорректный ввод. Попробуйте снова: ");
            }
        }
    }

    static bool ShouldDisplayResult(string choice) =>
        choice is not ("9" or "10" or "11" or "12" or "0");

    static void WaitForUserInput()
    {
        Console.WriteLine("\nНажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }
}