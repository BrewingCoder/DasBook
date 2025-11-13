var target = Argument("target", "Build");
var configuration = Argument("configuration", "Debug");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////


Task("Build")
    .Does(() =>
{
    DotNetBuild("./Dasbook.slnx", new DotNetBuildSettings
    {
        Configuration = configuration,
    });
});

// Task("Test")
//     .IsDependentOn("Build")
//     .Does(() =>
// {
//     DotNetTest("./src/Example.sln", new DotNetTestSettings
//     {
//         Configuration = configuration,
//         NoBuild = true,
//     });
// });

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);