using DasBook.Model;
using Shouldly;

namespace DasBook.Tests.Model;

public class TestEntityBase
{
    [Fact]
    public void Test_EntityBase_Creation()
    {
        var entity = new Universe();
        entity.ShouldNotBeNull();
    }
    
    [Fact]
    public void Test_EntityBase_Id_Property()
    {
        var entity = new Universe
        {
            Id = 42
        };
        entity.ShouldNotBeNull();       
        entity.Id.ShouldBeOfType<long>();
        entity.Id.ShouldBe(42);
    }
    [Fact]
    public void Test_EntityBase_ShouldBeAssignableTo_IEntity()
    {
        var entity = new Universe();
        entity.ShouldBeAssignableTo<IEntity>();
    }
    [Fact]
    public void Test_EntityBase_ShouldHaveKeyAttribute_On_Id_Property()
    {
        var idProperty = typeof(Universe).GetProperty("Id");
        idProperty.ShouldNotBeNull();
        var keyAttribute = idProperty.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), false);
        keyAttribute.Length.ShouldBe(1);
    }
    [Fact]
    public void Test_EntityBase_ShouldHaveDatabaseGeneratedAttribute_On_Id_Property()
    {
        var idProperty = typeof(Universe).GetProperty("Id");
        idProperty.ShouldNotBeNull();
        var dbGeneratedAttribute = idProperty.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedAttribute), false);
        dbGeneratedAttribute.Length.ShouldBe(1);
        var attributeInstance = dbGeneratedAttribute[0] as System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedAttribute;
        attributeInstance.ShouldNotBeNull();
        attributeInstance.DatabaseGeneratedOption.ShouldBe(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
    }
    
    
    
    [Fact]
    public void Test_EntityBase_Name_Property()
    {
        var entity = new Universe
        {
            Name = "My Universe"
        };
        entity.ShouldNotBeNull();       
        entity.Name.ShouldBeOfType<string>();
        entity.Name.ShouldBe("My Universe");
    }

    [Fact]
    public void Test_EntityBase_ShouldHaveMaxLengthAttribute_On_Name_Property()
    {
        var nameProperty = typeof(Universe).GetProperty("Name");
        nameProperty.ShouldNotBeNull();
        var maxLengthAttribute =
            nameProperty.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.MaxLengthAttribute), false);
        maxLengthAttribute.Length.ShouldBe(1);
        var attributeInstance = maxLengthAttribute[0] as System.ComponentModel.DataAnnotations.MaxLengthAttribute;
        attributeInstance.ShouldNotBeNull();
        attributeInstance.Length.ShouldBe(255);
    }
    
}