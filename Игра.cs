using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Занимательный_матан
{
    class Игра
    {
        ///     Стартовое меню.
        static bool GameMenu()
        {
            string firstList;
            bool leadIn = true;
            bool leadOut = false;
            do
            {
                Console.WriteLine(
                    "\n\tСтартовое меню:\n\tПравила(1).\n\tНачать игру(2).\n\tВыйти из игры(3).");
                firstList = Console.ReadLine();
                switch (firstList)
                {
                    case "1":
                        {
                            Console.WriteLine(  //      Правила игры.
                                "\n Уровни сложности: (Лёгкий) (Средний) (Сложный, только против ИИ)" +
                                "\n\n   Лёгкий.\t Рекомендуется выбор диапазона (12-120) очков на игру." +
                                "\n\tИгроки ходят по очереди, каждый игрок в свой ход может отнять из счёта игры 1-4 очка." +
                                "\n\tКто отнимет последние очки из общего счёта - победит." +
                                "\n\n   Средний.\t Для победы нужно вспоминать квадраты чисел в диапазоне общего счёта игры," +
                                "\n\tигрок добавит 10 очков в свой банк, если после окончания хода общий счёт будет равен " +
                                "\n\tзначению любого квадрата числа. Победит игрок с большим количеством очков в конце игры." +
                                "\n\tИз выбранного диапазона определится общий счёт: (12|120), (105|195) или (128|255). " +
                                "\n\n   Тяжелый.\t Попробуй победить ИИ, набрав больше очков за квадраты чисел.");
                        }
                        break;
                    case "2":
                        return leadIn;      //      Начать игру.
                    case "3":
                        return leadOut;     //      Выход из игры.
                    default:
                        {
                            Console.WriteLine(
                                " Некорректный ввод.");
                        }
                        break;
                }
            } while (leadIn || firstList != "1" || leadIn == leadOut);
            return leadIn;
        }
        ///     Выбор противника на игру.
        /*
         Добавил после последнего отправленного дз.
         
         */
        static string Opponent(string question, string AI)
        {
            while (true)
            {
                if (question == "1" || question == "2")
                {
                    Console.Write($"\tИгрок 2, введи свой ник:\t");
                    string player = Console.ReadLine();
                    return player;
                }
                else return AI;
            }
        }
        ///     Выбор баланса на игру.
        static int GameScore()
        {
            while (true)
            {
                Console.WriteLine(
                "\n Счёт игры:\n Нажать (0). рекомендовано для лёгкого уровня сложности." +
                "\n Нажать (1) для среднего значения.\n Нажать (2) и голову сломаешь, пока ходы просчитаешь.");


                int choise = int.Parse(Console.ReadLine());
                if (choise >= 0 && choise <= 2)
                {
                    int[] from = { 12, 122, 197 };
                    int[] to = { 121, 196, 270 };
                    Random gameScore = new Random();
                    return gameScore.Next(from[choise], to[choise]);
                }
                else
                    Console.WriteLine("\n Некорректный ввод.");
            }
        }
        /*      Против игрока: Выбор игроком действия на ход; возврат счёта игры после вычета выбранного значения.
                Против ИИ: Действие и возврат значения действия игрока.*/
        static int MakeUserTurn(int totalPoints, string question)
        {
            int UpperBound = question == "1" ? 4 : 6;   // Верхняя граница для ввода игрока.

            int UserTry = int.Parse(Console.ReadLine());
            // Если игра против ИИ.
            if (question == "3")
            {
                while (UserTry < 1 || UserTry > UpperBound)
                {
                    Console.WriteLine(
                        $"\n Неправильный ввод, диапазон хода [1-{UpperBound}]");
                    UserTry = int.Parse(Console.ReadLine());
                }
                return UserTry;
            }
            // Если игра PvP.
            while (UserTry < 1 || UserTry > UpperBound)
            {
                Console.WriteLine(
                    $"\n Неправильный ввод, диапазон хода [1-{UpperBound}]");
                UserTry = int.Parse(Console.ReadLine());
            }
            return UserTry;
        }
        ///     Действие и возврат ИИ игроком значения его действия.
        static int MakeAITurn(int totalPoints)
        {
            int[] mathRoots = new int[19];
            for (int count = 0; count < mathRoots.Length; count++)
            {
                int square = count * count;
                int minValue = square + 7;
                int middleValue = square + 14;
                int maxValue = square + 18;
                while (totalPoints > square && totalPoints <= maxValue)
                {
                    for (int turn = 1; turn < 7; turn++)
                    {
                        if (totalPoints - turn == middleValue || totalPoints - turn == minValue || totalPoints - turn == square)
                            return turn;
                        else continue;
                    }
                    break;
                }
                if (square > totalPoints) break;
            }
            int usuallyTurn;
            Random rand = new Random();
            usuallyTurn = rand.Next(1, 7);
            return usuallyTurn;
        }
        ///     Определение всех квадратов чисел, максимальный из которых не больше значения счёта игры.
        static int MathSquare(int totalPoints)
        {
            int[] mathRoots = new int[19];
            for (int count = 0; count < mathRoots.Length; count++)
            {
                int square = count * count;
                mathRoots[count] = square;
                if (totalPoints == square)
                {
                    int courier = 10;
                    return courier;
                }
                if (square > totalPoints)
                {
                    break;
                }
            }
            return 0;
        }
        ///     Результаты матча.
        /*
         Добавил после последнего отправленного дз.
         
         */
        static string BattleResult(string[] Names, int playerScore, int opponentScore, string question)
        {
            while (true)
            {
                Console.WriteLine(
                    $"\n\n Баланс игрока {Names[0]}: {playerScore}." +
                    $"\n Баланс игрока {Names[1]}: {opponentScore}.");
                if (playerScore > opponentScore)
                    return $"Поздравляю с победой, {Names[0]}!";
                else if (playerScore < opponentScore)
                    return $"Поздравляю с победой, {Names[1]}!";
                else
                    return "Напряжённая игра, но в этот раз у Вас ничья!";
            }
        }
        ///     Вопрос-ответ(Да|Нет).
        static bool AskUserYesNo(string question)
        {
            Console.Write(question);
            return Console.ReadLine() == "Да";
        }

        static void Main(string[] args)
        {

            int totalPoints;
            ///     Начало новой игры.
            bool NewGame = GameMenu();
            while (NewGame)
            {
                ///     Генерация очков на матч.
                totalPoints = GameScore();
                while (totalPoints > 0)
                {
                    Console.WriteLine("\n Выбрать уровень сложности:\n\n Лёгкий (1)." +
                                                                    "\n Средни (2)." +
                                                                    "\n Тяжёлый (3).");
                    string question = Console.ReadLine();   //   Выбор уровня сложности.
                    while (question != "1" && question != "2" && question != "3")       // Выход за диапазон значений.
                    {
                        Console.WriteLine("Некорректный ввод, только 1, 2 или 3.");
                    }
                    Console.Write($"\tВведи свой ник:\t");
                    string player = Console.ReadLine();
                    string AI = " Computer";
                    string opponent = Opponent(question, AI);
                    string[] Names = new string[2];
                    Names[0] = player;
                    Names[1] = opponent;
                    var Turn = 0;
                    var Round = 0;
                    if (question == "1")    //      Условие для сложности: Легкий.
                    {
                        do
                        {
                            Round++;
                            string selectedPlayer = Turn % 2 == 1 ? player : opponent;
                            Console.WriteLine(
                                $"\n\n\tСчёт игры: {totalPoints}\n\t\tРаунд: {Turn + 1}.\n\n\t {selectedPlayer} твой выход!");

                            totalPoints -= MakeUserTurn(totalPoints, question);
                            if (totalPoints <= 0)
                            {
                                NewGame = AskUserYesNo(
                                    $"\n Поздравляю с победой, {selectedPlayer}! " +
                                    "\n Хочешь сыграть ещё раз?(Да|Нет)");
                            }
                            Turn++;
                        } while (totalPoints > 0); //      Условие продолжения матча.
                    }
                    //      Остальные сложности.
                    else
                    {
                        var playerScore = 0;
                        var opponentScore = 0;
                        int[] Bank = new int[2];
                        Bank[0] = playerScore;
                        Bank[1] = opponentScore;
                        do
                        {
                            Round++;
                            string selectedPlayer = Turn % 2 == 1 ? player : opponent;
                            Console.WriteLine(
                                $"\n Следующий раунд: {Round}." +
                                $"\n\n\t\t\tСчёт игры: {totalPoints}." +
                                $"\n\n\t\t\tРаунд {Round}. {selectedPlayer} твой выход!");

                            ///  Вычет действия игрока из счёта игры.
                            totalPoints -= selectedPlayer == AI ? MakeAITurn(totalPoints) : MakeUserTurn(totalPoints, question);
                            ///    Передать 10 очков, если счёт игры == любому квадрату числа или передать 0 очков.
                            int courier = MathSquare(totalPoints);
                            if (courier != 0)
                                Console.WriteLine(
                                    $"\n {courier} добавлено к твоему балансу.");

                            if (selectedPlayer != AI)
                            {
                                int scoreBank = Turn % 2 == 1   /*  В зависимости от кратности хода,    */
                                ? Bank[0] += courier    /*  добавить очки на баланс игрока      */
                                : Bank[1] += courier;           /*  или добавить очки на баланс ИИ.     */
                                Console.WriteLine(
                                    $"\tБаланс банка {selectedPlayer}: {scoreBank}.\t {totalPoints} осталось на счету игры.");
                            }
                            if (selectedPlayer == AI)
                            {
                                int scoreBank = opponentScore += courier;
                                Console.WriteLine(
                                    $"\tБаланс банка {selectedPlayer}: {scoreBank}.\t {totalPoints} осталось на счету игры.");
                            }

                            Console.WriteLine(
                                $"\n\tБаланс игроков: \t{player} - {playerScore}, \t{opponent} - {opponentScore}");

                            if (totalPoints <= 0)
                            {
                                string results = BattleResult(Names, playerScore, opponentScore, question);
                                NewGame = AskUserYesNo($"\t {results}\n Хочешь сыграть ещё раз?(Да|Нет)");
                            }
                            Turn++;
                        } while (totalPoints > 0); //      Условие продолжения матча.
}   }   }   }   }   }
