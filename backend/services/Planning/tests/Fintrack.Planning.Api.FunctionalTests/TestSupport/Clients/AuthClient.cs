namespace Fintrack.Planning.Api.FunctionalTests.TestSupport.Clients;

public class AuthClient(HttpClient httpClient)
{
    public void LoginAsUser()
    {
        httpClient.DefaultRequestHeaders.Add("X-Test-UserId", Guid.NewGuid().ToString());
    }
}
