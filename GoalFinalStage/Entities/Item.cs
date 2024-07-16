namespace GoalFinalStage.Entities
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<User> Users { get; set; } = new List<User>() { };
    }
}
