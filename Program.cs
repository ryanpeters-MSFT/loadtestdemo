var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => Results.Ok("looking good!"));

app.MapGet("/secure", (string apikey) =>
{
    const string successMessage = "you passed in the correct key!";
    const string noKeyMessage = "no key provided";
    const string mismatchMessage = "key did not match";

    if (string.IsNullOrWhiteSpace(apikey))
    {
        Console.WriteLine(noKeyMessage);

        return Results.BadRequest(noKeyMessage);
    }

    if (apikey == "123456789")
    {
        Console.WriteLine(successMessage);

        return Results.Ok(successMessage);
    }

    Console.WriteLine(mismatchMessage);

    return Results.BadRequest(mismatchMessage);
});

app.Run();