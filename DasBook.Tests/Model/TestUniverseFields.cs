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
    
    [Fact]
    public void Universe_DescriptionLength_Computes_Correctly()
    {
        // Arrange
        var universe = new DasBook.Model.Universe();
        var description = WaffleGenerator.WaffleEngine.Html(
            paragraphs: 3, includeHeading: true, includeHeadAndBody: true
        );
        universe.Description = description;

        // Act
        var computedLength = universe.DescriptionLength;

        // Assert
        computedLength.ShouldBe(description.Length);
    }

    [Fact]
    public void Universe_Description_Field_Can_Be_Null()
    {
        // Arrange
        var universe = new DasBook.Model.Universe();

        // Act
        universe.Description = null;

        // Assert
        universe.Description.ShouldBeNull();
    }

    [Fact]
    public void Universe_Description_Field_Has_MaxLength_Attribute()
    {
        // Arrange
        var universeType = typeof(DasBook.Model.Universe);
        var descriptionProperty = universeType.GetProperty("Description");
        var maxLengthAttribute = descriptionProperty
            ?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.MaxLengthAttribute), false)
            .FirstOrDefault() as System.ComponentModel.DataAnnotations.MaxLengthAttribute;

        // Act & Assert
        maxLengthAttribute.ShouldNotBeNull();
    }

    [Fact]
    public void Universe_Description_Field_Has_Column_Attribute_With_VarcharMax()
    {
        // Arrange
        var universeType = typeof(DasBook.Model.Universe);
        var descriptionProperty = universeType.GetProperty("Description");
        var columnAttribute = descriptionProperty
            ?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.ColumnAttribute), false)
            .FirstOrDefault() as System.ComponentModel.DataAnnotations.Schema.ColumnAttribute;

        // Act & Assert
        columnAttribute.ShouldNotBeNull();
        columnAttribute.TypeName.ShouldBe("varchar(Max)");
    }
}