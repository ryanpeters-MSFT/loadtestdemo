var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// unauthenticated path
app.MapGet("/", () =>
{
    const string successMessage = "well hello there anonymous user";

    Console.WriteLine(successMessage);

    return Results.Ok(successMessage);
});

// authenticated path
app.MapGet("/secure", (string apikey) =>
{
    // our super-secret master key
    const string key = "123456789";

    const string successMessage = "you passed in the correct key!";
    const string noKeyMessage = "no key provided";
    const string mismatchMessage = "key did not match";

    if (string.IsNullOrWhiteSpace(apikey))
    {
        Console.WriteLine(noKeyMessage);

        return Results.BadRequest(noKeyMessage);
    }

    if (apikey == key)
    {
        Console.WriteLine(successMessage);

        return Results.Ok(successMessage);
    }

    Console.WriteLine(mismatchMessage);

    return Results.BadRequest(mismatchMessage);
});

app.Run();