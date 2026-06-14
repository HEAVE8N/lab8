// Program.cs
namespace BookCatalog
{
    class Program
    {
        private static BookManager db;
        static void Main()
        {
            db = new BookManager();
            Console.Title = "Каталог книг";
            db.Load();

            while (true)
            {
                Console.WriteLine("\n═══════════════════════════════════");
                Console.WriteLine("1-Просмотр 2-Добавить 3-Удалить");
                Console.WriteLine("4-Запросы 0-Выход");
                Console.Write("Выбор: ");
                string cmd = Console.ReadLine();

                if (cmd == "0")
                {
                    break;
                }
                if (cmd == "1")
                {
                    Console.Clear(); db.ViewAll();
                }
                else if (cmd == "2")
                {
                    AddBook();
                }
                else if (cmd == "3")
                {
                    DeleteBook();
                }
                else if (cmd == "4")
                {
                    QueryMenu();
                }
                else
                {
                    Console.WriteLine("Ошибка!"); continue;
                }

                Console.WriteLine("\nНажмите любую клавишу...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void AddBook()
        {
            Console.Clear();
            try
            {
                Console.Write("Название: "); 
                string title = Console.ReadLine();
                Console.Write("Автор: "); 
                string author = Console.ReadLine();
                Console.Write("Год: "); 
                int year = int.Parse(Console.ReadLine());
                Console.Write("Цена: "); 
                double price = double.Parse(Console.ReadLine());
                Console.Write("Страниц: "); 
                int pages = int.Parse(Console.ReadLine());
                db.Add(new Book(db.NextId(), title, author, year, price, pages));
                Console.WriteLine("Добавлено");
            }
            catch (Exception ex) 
            { 
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void DeleteBook()
        {
            Console.Clear();
            db.ViewAll();
            Console.Write("\nID книги для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine(db.Delete(id) ? "Удалено" : "Не найдено");
            }
            else
            {
                Console.WriteLine("Неверный ID");
            }
        }

        static void QueryMenu()
        {
            Console.Clear();
            Console.WriteLine("\n═══════════════════════════════════");
            Console.WriteLine("1-Книги автора (список)");
            Console.WriteLine("2-Книги дешевле цены (список)");
            Console.WriteLine("3-Средняя цена всех книг (значение)");
            Console.WriteLine("4-Общее количество страниц (значение)");
            Console.Write("Выбор: ");
            string q = Console.ReadLine();

            if (q == "1")
            {
                Console.Write("Автор: "); string a = Console.ReadLine();
                var res = db.GetByAuthor(a);
                Console.WriteLine(res.Count > 0 ? string.Join("\n", res) : "Не найдено");
            }
            else if (q == "2")
            {
                Console.Write("Макс. цена: ");
                if (double.TryParse(Console.ReadLine(), out double p))
                {
                    var res = db.GetCheaper(p);
                    Console.WriteLine(res.Count > 0 ? string.Join("\n", res) : "Не найдено");
                }
                else
                {
                    Console.WriteLine("Неверная цена");
                }
            }
            else if (q == "3")
            {
                Console.WriteLine($"Средняя цена: {db.AvgPrice():C}");
            }
            else if (q == "4")
            {
                Console.WriteLine($"Всего страниц: {db.TotalPages()}");
            }
            else
            {
                Console.WriteLine("Неверный выбор");
            }
        }
    }
}