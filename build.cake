#addin nuget:?package=Cake.FileHelpers&version=7.0.0
#addin nuget:?package=Cake.Sonar&version=5.0.0
#tool dotnet:?package=dotnet-sonarscanner&version=11.0.0

var target = Argument("target", "Analyze");
var configuration = Argument("configuration", "Debug");

Task("LoadEnvFile")
    .Does(() =>
{
    var envFilePath = MakeAbsolute(File("./DasBook.env")); // Assuming .env is in the root

    if (FileExists(envFilePath))
    {
        var lines = System.IO.File.ReadAllLines(envFilePath.FullPath);
        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            if (!string.IsNullOrWhiteSpace(trimmedLine) && !trimmedLine.StartsWith("#"))
            {
                var parts = trimmedLine.Split(new[] { '=' }, 2);
                if (parts.Length == 2)
                {
                    var key = parts[0].Trim();
                    var value = parts[1].Trim();
                    var obfuscatedValue = new string('*', value.Length);
                    Environment.SetEnvironmentVariable(key, value);
                    Information($"Loaded environment variable: {key}={obfuscatedValue}");
                }
            }
        }
    }
    else
    {
        Warning("'.env' file not found.");
    }
});

Task("Sonar-Begin")
    .IsDependentOn("LoadEnvFile")
    .Does(() =>
{
    var sonarSettings = new SonarBeginSettings
    {
        Key = EnvironmentVariable("secrets.SONAR_PROJECT_KEY"),
        Url = EnvironmentVariable("secrets.SONAR_HOST_URL"),
        Token = EnvironmentVariable("secrets.SONAR_TOKEN"),
        VsCoverageReportsPath = "coverage.xml",
        VsTestReportsPath = "./Dasbook.Tests/TestResults/TestResults.trx",
        UseCoreClr = true,
    };

    SonarBegin(sonarSettings);
});

Task("Sonar-End")
    .IsDependentOn("Build")
    .Does(() =>
{
    SonarEnd(new SonarEndSettings
    {
        Token = EnvironmentVariable("secrets.SONAR_TOKEN"),
    });
});

Task("Test")
    .IsDependentOn("LoadEnvFile")
    .IsDependentOn("Build")
    .ContinueOnError()
    .Does(() =>
{
    DotNetTest("./Dasbook.slnx", new DotNetTestSettings
    {
        DiagnosticOutput = true,
        Configuration = configuration,
        NoBuild = true,
        Verbosity = DotNetVerbosity.Minimal,
        Loggers = new[] { "trx;LogFileName=TestResults.trx" },
    });
    
     var settings = new DotNetToolSettings {
            DiagnosticOutput = false,
            ArgumentCustomization = args => 
                args.Append("collect \"dotnet test\"")
                .Append("-o coverage.xml")
                .Append("-f xml")
        };
     DotNetTool("coverage", settings);
      
});

Task("Build")
    .IsDependentOn("LoadEnvFile")
    .Does(() =>
    {
   
    
    DotNetBuild("./Dasbook.slnx",new DotNetBuildSettings
    {
        Configuration = configuration,
    });

});

Task("Analyze")
    .IsDependentOn("Sonar-Begin")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .IsDependentOn("Sonar-End");
RunTarget(target);