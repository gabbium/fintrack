namespace BuildingBlocks.Api.FunctionalTests.Assertions;

public static class HttpResponseMessageAssertions
{
    private static readonly JsonSerializerOptions s_json = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public static async Task<T> ShouldBeCreatedWithBodyAndLocation<T>(
        this HttpResponseMessage response,
        Func<T, string> expectedLocationPath)
    {
        response.StatusCode.ShouldBe(HttpStatusCode.Created);

        var body = await response.Content.ReadFromJsonAsync<T>(s_json);
        body.ShouldBeOfType<T>();

        var location = response.Headers.Location;
        location.ShouldNotBeNull();
        location.LocalPath.ShouldBe(expectedLocationPath(body));

        return body;
    }

    public static async Task<T> ShouldBeCreatedWithBody<T>(this HttpResponseMessage response)
    {
        response.StatusCode.ShouldBe(HttpStatusCode.Created);

        var body = await response.Content.ReadFromJsonAsync<T>(s_json);
        body.ShouldBeOfType<T>();

        return body;
    }

    public static async Task<T> ShouldBeOkWithBody<T>(this HttpResponseMessage response)
    {
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var body = await response.Content.ReadFromJsonAsync<T>(s_json);
        body.ShouldBeOfType<T>();

        return body;
    }

    public static void ShouldBeOk(this HttpResponseMessage response)
    {
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    public static void ShouldBeNoContent(this HttpResponseMessage response)
    {
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }

    public static async Task ShouldBeBadRequestWithValidation(this HttpResponseMessage response)
    {
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>(s_json);
        problem.ShouldNotBeNull();
    }

    public static async Task ShouldBeBadRequestWithProblem(this HttpResponseMessage response)
    {
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>(s_json);
        problem.ShouldNotBeNull();
    }

    public static void ShouldBeUnauthorizedWithBearerChallenge(this HttpResponseMessage response)
    {
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        response.Headers.WwwAuthenticate.ShouldContain(
            header => string.Equals(header.Scheme, "Bearer", StringComparison.OrdinalIgnoreCase));
    }

    public static async Task ShouldBeNotFoundWithProblem(this HttpResponseMessage response)
    {
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>(s_json);
        problem.ShouldNotBeNull();
    }

    public static async Task ShouldBeConflictWithProblem(this HttpResponseMessage response)
    {
        response.StatusCode.ShouldBe(HttpStatusCode.Conflict);
        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>(s_json);
        problem.ShouldNotBeNull();
    }

    public static async Task ShouldBeUnprocessableEntityWithProblem(this HttpResponseMessage response)
    {
        response.StatusCode.ShouldBe(HttpStatusCode.UnprocessableEntity);
        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>(s_json);
        problem.ShouldNotBeNull();
    }
}
