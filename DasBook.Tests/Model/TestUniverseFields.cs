using Bogus;
using Shouldly;

namespace DasBook.Tests.Model;

public class TestUniverseFields
{
    [Fact]
    public void Universe_Has_Description_Field()
    {
        // Arrange
        var universe = new DasBook.Model.Universe();
        var description = WaffleGenerator.WaffleEngine.Html(
            paragraphs: 4, includeHeading: true, includeHeadAndBody: true
        );

        // Act
        universe.Description = description;

        // Assert
        universe.Description.ShouldNotBeNullOrEmpty();
        universe.Description.ShouldBeEquivalentTo(description);
    }


}