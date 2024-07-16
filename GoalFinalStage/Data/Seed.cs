using GoalFinalStage.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GoalFinalStage.Data
{
    public class Seed
    {
        public static async Task SeedItems(DataContext context)
        {
            if (await context.Items.AnyAsync()) return;

            var itemData = await File.ReadAllTextAsync("Data/ItemSeedData.json");
            var items = JsonSerializer.Deserialize<List<Item>>(itemData);
            foreach (var item in items)
            {
                context.Items.Add(item);
            }

            await context.SaveChangesAsync();
        }
    }
}
