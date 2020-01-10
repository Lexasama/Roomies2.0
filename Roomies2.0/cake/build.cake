Setup(ctx => {
Information("Running tasks : ");
});

Teardown(ctx => {
    Information("Finished running tasks.");
});

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");


Task("Default")
    .IsDependentOn("Build");

Task("Clean")
    .Does(() =>
{
    CleanDirectories("**/bin/" + configuration);
    CleanDirectories("**/obj/" + configuration);
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetCoreRestore("../Roomies2.0.sln");
});

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
{
    DotNetCoreBuild("../Roomies2.0.sln", new DotNetCoreBuildSettings {
        Configuration = configuration
    });
});

// Task("Tests")
//     .IsDependentOn("Build")
//     .Does(() =>
// {
//     var testLocation = File("../src/Roomies2.DAL.Tests/Roomies2.DAL.Tests.csproj");
//     DotNetCoreTest(testLocation, new DotNetCoreTestSettings {
//         Configuration = configuration,
//         NoBuild = true,
//         ArgumentCustomization = args => 
//             args.Append("--collect")
//                 .AppendQuoted("Code Coverage")
//                 .Append("--logger")
//                 .Append("trx") 
//     });
// });

RunTarget(target);
