namespace SandwichStore
{
    public record Sandwich
    {
        // sandwich data model - in memory store db
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
    }
    public class inMemSandwichDB
    {
        private static List<Sandwich> _sandwiches = new List<Sandwich>()
        {
            new Sandwich{ Id=1, Name="Grilled Cheese Sandwich", Description="A buttered and grill-toasted bread filled with your choice of cheese.", Price=3.99m },
            new Sandwich{ Id=2, Name="Sloppy Joe Sandwich", Description="Ground beef, onion, garlic and bell pepper in a seasoned tomato sauce, served on a hamburger bun.", Price=5.99m },
            new Sandwich{ Id=3, Name="American Club Sandwich", Description="A buttered and grill-toasted bread filled with ham, bacon, turkey,tomatoes, lettuce and your choice of cheese.", Price=4.99m },
            new Sandwich{ Id=4, Name="Italian Knuckle Sandwich", Description="A buttered and grill-toasted bread filled with coppa, hot soppressata, pepperoni, hot pepper cheese, sliced pepperoncini, and Italian dressing.", Price=7.99m},
        };

        // get all sandwiches
        public static List<Sandwich> GetSandwiches()
        {
            return _sandwiches;
        }

        // get a sandwich
        public static Sandwich? GetSandwich(int id)
        {
            return _sandwiches.SingleOrDefault(sandwich => sandwich.Id == id);
        }

        // create sandwich
        public static Sandwich CreateSandwich(Sandwich sandwich)
        {
            _sandwiches.Add(sandwich);
            return sandwich;
        }

        // update sandwich
        public static Sandwich UpdateSandwich(Sandwich update)
        {
            _sandwiches = _sandwiches.Select(sandwich =>
            {
                if (sandwich.Id == update.Id)
                {
                    sandwich.Name = update.Name;
                }
                return sandwich;
            }).ToList();
            return update;
        }

        // delete sandwich
        public static void RemoveSandwich(int id)
        {
            _sandwiches = _sandwiches.FindAll(sandwich => sandwich.Id != id).ToList();
        }

    }
}
