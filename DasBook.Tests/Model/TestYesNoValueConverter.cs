using Shouldly;

namespace DasBook.Tests.Model;

public class TestYesNoValueConverter
{
    [Fact]
    public void TestYesNoConverter()
    {
        var converter = new DasBook.Model.Helpers.YesNoConverter();

        var yes = converter.ConvertToProvider(true);
        var no = converter.ConvertToProvider(false);
        
        yes.ShouldBeEquivalentTo("Yes");
        no.ShouldBeEquivalentTo("No");

        var fromYes = (bool)(converter.ConvertFromProvider("Yes") ?? false);
        var fromNo =  (bool)(converter.ConvertFromProvider("No") ?? true) ;
        fromYes.ShouldBeTrue();
        fromNo.ShouldBeFalse();
    }
}