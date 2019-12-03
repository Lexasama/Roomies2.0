var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

Task("Test_task")
    .Does(() =>
{
    Information("yolo");
});

RunTarget(target);
