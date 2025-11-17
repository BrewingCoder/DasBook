using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace DasBook.Tests.Model.Integrations;

public class TestAppDbContext
{
    [Fact]
    public void CanCreateDbContext()
    {
        var options = new DbContextOptionsBuilder<DasBook.Model.AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var context = new DasBook.Model.AppDbContext(options);
        context.ShouldNotBeNull();
    }
    
    [Fact]
    public void CanCreateDbContext_DefaultConstructor()
    {
        var context = new DasBook.Model.AppDbContext();
        context.ShouldNotBeNull();
        
    }
    [Fact]
    public void CanAddUniverseEntity()
    {
        var options = new DbContextOptionsBuilder<DasBook.Model.AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_AddUniverse")
            .Options;

        using (var context = new DasBook.Model.AppDbContext(options))
        {
            var universe = new DasBook.Model.Universe { Name = "Test Universe" };
            context.Universes.Add(universe);
            context.SaveChanges();
        }

        using (var context = new DasBook.Model.AppDbContext(options))
        {
            var universe = context.Universes.FirstOrDefault(u => u.Name == "Test Universe");
            universe.ShouldNotBeNull();
            universe.Name.ShouldBe("Test Universe");
        }
    }
}