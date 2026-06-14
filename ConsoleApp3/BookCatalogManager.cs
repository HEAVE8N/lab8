// BookManager.cs
using System.Text;

namespace BookCatalog
{
    public class BookManager
    {
        private List<Book> books;

        public BookManager()
        {
            books = new List<Book>();
        }
        public void Load()
        {
            string file = "catalog.dat";
            if (!File.Exists(file))
            {
                return;
            }

            try
            {
                using (var fs = new FileStream(file, FileMode.Open))
                using (var br = new BinaryReader(fs, Encoding.UTF8))
                {
                    int count = br.ReadInt32();
                    books.Clear();
                    for (int i = 0; i < count; i++)
                    {
                        var b = new Book
                        {
                            Id = br.ReadInt32(),
                            Title = br.ReadString(),
                            Author = br.ReadString(),
                            Year = br.ReadInt32(),
                            Price = br.ReadDouble(),
                            Pages = br.ReadInt32()
                        };
                        books.Add(b);
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine($"Ошибка загрузки: {ex.Message}"); }
        }

        // Сохранение в бинарный файл
        private void Save()
        {
            string file = "catalog.dat";
            using (var fs = new FileStream(file, FileMode.Create))
            using (var bw = new BinaryWriter(fs, Encoding.UTF8))
            {
                bw.Write(books.Count);
                foreach (var b in books)
                {
                    bw.Write(b.Id);
                    bw.Write(b.Title ?? "");
                    bw.Write(b.Author ?? "");
                    bw.Write(b.Year);
                    bw.Write(b.Price);
                    bw.Write(b.Pages);
                }
            }
        }

        // 2. Просмотр
        public void ViewAll()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("\nБаза пуста\n");
            }
            else
            {
                foreach (var b in books)
                {
                    Console.WriteLine(b);
                }
            }
        }

        // 3. Удаление по ключу (ID)
        public bool Delete(int id)
        {
            var b = books.FirstOrDefault(x => x.Id == id);
            if (b != null)
            {
                books.Remove(b);
            }
            Save();
            return b != null;
        }

        // 4. Добавление
        public void Add(Book b)
        {
            if (books.Any(x => x.Id == b.Id))
            {
                throw new Exception("ID существует");
            }
            books.Add(b);
            Save();
        }

        public int NextId()
        {
            if (books.Count == 0)
            {
                return 1;
            }
            else
            {
                return books.Max(x => x.Id) + 1;
            }
        }

        // Запрос 1: список книг автора
        public List<Book> GetByAuthor(string author)
        {
            return books.Where(x => x.Author.Equals(author)).ToList();
        }

        // Запрос 2: список книг дешевле цены
        public List<Book> GetCheaper(double price)
        {
            return books.Where(x => x.Price < price).ToList();
        }
        // Запрос 3: средняя цена
        public double AvgPrice()
        {
            if (books.Any())
            {
                return books.Average(x => x.Price);
            }
            else
            {
                return 0;
            }
        }
        // Запрос 4: общее число страниц
        public int TotalPages()
        {
            return books.Sum(x => x.Pages);
        }
    }
}
