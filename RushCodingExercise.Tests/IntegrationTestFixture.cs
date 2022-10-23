namespace RushCodingExercise.Tests
{
    public class IntegrationTestFixture
    {
        public IntegrationTestFixture()
        {
            HttpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com")
            };
        }

        public HttpClient HttpClient { get; private set; }

        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }
}
