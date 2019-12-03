var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");

Task("Test")
    .Does(() =>
{
    Information("yolo");
});

RunTarget(target);
