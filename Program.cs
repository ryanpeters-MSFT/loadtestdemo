var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// our super-secret master key (securely stored and retrieved... RIGHT??)
const string secretKey = "123456789";

// write to console with timestamp
var log = (string text) => Console.WriteLine($"{DateTime.Now} - {text}");

// unauthenticated path
app.MapGet("/", () =>
{
    const string successMessage = "well hello there anonymous user";

    log(successMessage);

    return Results.Ok(successMessage);
});

// retrieves our API key after "securely authenticating"
app.MapGet("/key", () =>
{
    log("key requested");
    
    return Results.Ok(secretKey);
});

// authenticated path
app.MapGet("/secure", (string apikey) =>
{
    const string successMessage = "you passed in the correct key!";
    const string noKeyMessage = "no key provided";
    const string mismatchMessage = "key did not match";

    if (string.IsNullOrWhiteSpace(apikey))
    {
        log(noKeyMessage);

        return Results.BadRequest(noKeyMessage);
    }

    if (apikey == secretKey)
    {
        log(successMessage);

        return Results.Ok(successMessage);
    }

    log(mismatchMessage);

    return Results.BadRequest(mismatchMessage);
});

app.Run();