using Headspring;

namespace Domain
{
    public class Category : Enumeration<Category, int>
    {
        public static Category Books = new Category(1, "Books");
        public static Category Websites = new Category(2, "Websites");
        public static Category Videos = new Category(3, "Videos");

        public Category(int value, string displayName) : base(value, displayName) { }
    }
}