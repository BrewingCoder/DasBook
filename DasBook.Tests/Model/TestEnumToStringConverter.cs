using Shouldly;

namespace DasBook.Tests.Model;

public class  TestEnumToStringConverter
{
    private enum TestEnum
    {
        FirstValue,
        SecondValue,
        ThirdValue
    }

    [Fact]
    public void TestEnumToStringConverterFunctionality()
    {
        var converter = new DasBook.Model.Helpers.EnumToStringConverter<TestEnum>();

        var firstValueString = converter.ConvertToProvider(TestEnum.FirstValue);
        var secondValueString = converter.ConvertToProvider(TestEnum.SecondValue);
        
        firstValueString.ShouldBeEquivalentTo("FirstValue");
        secondValueString.ShouldBeEquivalentTo("SecondValue");

        var fromFirstValue = (TestEnum)(converter.ConvertFromProvider("FirstValue") ?? TestEnum.ThirdValue);
        var fromSecondValue = (TestEnum)(converter.ConvertFromProvider("SecondValue") ?? TestEnum.ThirdValue);
        
        fromFirstValue.ShouldBe(TestEnum.FirstValue);
        fromSecondValue.ShouldBe(TestEnum.SecondValue);
    }
}