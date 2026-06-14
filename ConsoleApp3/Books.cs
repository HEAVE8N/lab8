// Book.cs
using System;

namespace BookCatalog
{
    [Serializable]
    public class Book
    {
        // Поля
        private int id;
        private string title;
        private string author;
        private int year;
        private double price;
        private int pages;

        public int Id
        {
            get 
            { 
                return id;
            }
            set 
            { 
                id = value; 
            }
        }

        public string Title
        {
            get 
            { 
                return title;
            }
            set 
            {
                title = value;
            }
        }

        public string Author
        {
            get 
            { 
                return author;
            }
            set 
            { 
                author = value;
            }
        }

        public int Year
        {
            get 
            { 
                return year; 
            }
            set 
            { 
                year = value;
            }
        }

        public double Price
        {
            get 
            { 
                return price; 
            }
            set 
            { 
                price = value;
            }
        }

        public int Pages
        {
            get 
            { 
                return pages;
            }
            set 
            { 
                pages = value;
            }
        }

        public Book()
        {
            id = 0;
            title = "Неизвестно";
            author = "Неизвестно";
            year = 2000;
            price = 0;
            pages = 0;
        }

        public Book(int id, string title, string author, int year, double price, int pages)
        {
            this.id = id;
            this.title = title;
            this.author = author;
            this.year = year;
            this.price = price;
            this.pages = pages;
        }

        public override string ToString()
        {
            return $"ID:{id,-3} | {title,-25} | {author,-15} | {year,-4} | {price,-6:C} | {pages,-4} стр.";
        }
    }
}
