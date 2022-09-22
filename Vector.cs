using System.Configuration;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using StrawberryShake;
using VectorGraphQL;
using JsonSerializer = System.Text.Json.JsonSerializer;

internal class Vector
{
    private static Token _bearer;
    private static IVectorClient _client;

    public Vector()
    {
        var serviceCollection = new ServiceCollection();

        // Inspired by https://chillicream.com/docs/strawberryshake/networking/authentication
        serviceCollection
            .AddVectorClient()
            .ConfigureHttpClient(_client =>
            {
                _client.BaseAddress =
                    new Uri(ConfigurationManager.AppSettings["VectorEndPoint"]);
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", GetTokenAsync(_client).Result.AccessToken);
            });

        IServiceProvider services = serviceCollection.BuildServiceProvider();

        _client = services.GetRequiredService<IVectorClient>();
    }

    /// <summary>
    /// Method to return a bearer token
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    public async Task<Token> GetTokenAsync(HttpClient client)
    {
        string baseAddress = ConfigurationManager.AppSettings["VectorTokenEndPoint"];
        string grant_type = "client_credentials";
        string client_id = ConfigurationManager.AppSettings["ClientID"];
        string client_secret = ConfigurationManager.AppSettings["ClientSecret"];

        var form = new Dictionary<string, string>
                {
                    {"grant_type", grant_type},
                    {"client_id", client_id},
                    {"client_secret", client_secret},
                };

        HttpResponseMessage tokenResponse = await client.PostAsync(baseAddress, new FormUrlEncodedContent(form));
        var jsonContent = await tokenResponse.Content.ReadAsStringAsync();
        var tok = JsonSerializer.Deserialize<Token>(jsonContent);
        return tok;
    }

    #region "Person-Related Methods"
    public async Task<IReadOnlyList<IGetFirst100People_People_Nodes>> GetFirst100People()
    {
        IOperationResult<IGetFirst100PeopleResult> result = await _client.GetFirst100People.ExecuteAsync();
        result.EnsureNoErrors();

        return result.Data.People.Nodes;
    }

    public async Task<IReadOnlyList<IGetFirst100ActivePeopleAndProgress_People_Nodes>> GetFirst100ActivePeopleAndProgress()
    {
        IOperationResult<IGetFirst100ActivePeopleAndProgressResult> result = await _client.GetFirst100ActivePeopleAndProgress.ExecuteAsync();
        result.EnsureNoErrors();

        return result.Data.People.Nodes;
    }
    public async Task<IGetPersonById_Person> GetUserByGuid(Guid personId)
    {
        IOperationResult<IGetPersonByIdResult> result = await _client.GetPersonById.ExecuteAsync(personId.ToString().ToUpper());
        result.EnsureNoErrors();

        return result.Data.Person;
    }

    public async Task<IAddPerson_AddPerson> AddUser(Person person)
    {
        IOperationResult<IAddPersonResult> result;

        if (person.PositionId == Guid.Empty)
            result = await _client.AddPerson.ExecuteAsync(person.Address1,
            person.Address2,
            person.BeginDate,
            person.Email,
            person.ExternalUniqueId,
            person.FirstName,
            person.LastName,
            person.Phone,
            null,
            person.UserName
            );
        else
            result = await _client.AddPerson.ExecuteAsync(person.Address1,
            person.Address2,
            person.BeginDate,
            person.Email,
            person.ExternalUniqueId,
            person.FirstName,
            person.LastName,
            person.Phone,
            person.PositionId.ToString().ToUpper(),
            person.UserName
            );

        result.EnsureNoErrors();

        return result.Data.AddPerson;
    }

    public async Task<IUpdatePerson_Person> UpdateUser(Person person)
    {
        IOperationResult<IUpdatePersonResult> result = await _client.UpdatePerson.ExecuteAsync(person.Address1,
            person.Address2,
            person.Email,
            person.FirstName,
            person.LastName,
            person.PersonId.ToString().ToUpper(),
            person.Phone,
            person.UserName);
        result.EnsureNoErrors();

        return result.Data.Person;
    }

    public async Task<IDeactivatePerson_Person> DeactivatePersonById(Guid personId)
    {
        IOperationResult<IDeactivatePersonResult> result = await _client.DeactivatePerson.ExecuteAsync(personId.ToString().ToUpper());
        result.EnsureNoErrors();

        return result.Data.Person;
    }

    #endregion

    #region "Position-Related Methods"
    public async Task<IReadOnlyList<IGetAllPositions_Positions_Nodes>> GetAllPositions()
    {
        IOperationResult<IGetAllPositionsResult> result = await _client.GetAllPositions.ExecuteAsync();
        result.EnsureNoErrors();

        return result.Data.Positions.Nodes;
    }

    public async Task<IAddPosition_AddPosition> AddPosition(string name, string code, Guid parentId)
    {
        IOperationResult<IAddPositionResult> result;

        if (parentId == Guid.Empty)
            result = await _client.AddPosition.ExecuteAsync(name, code, null);
        else
            result = await _client.AddPosition.ExecuteAsync(name, code, parentId.ToString().ToUpper());

        result.EnsureNoErrors();

        return result.Data.AddPosition;
    }
    #endregion

}

public class Person
{
    public Person()
    {
    }
    public Person(IGetPersonById_Person person)
    {
        this.Address1 = person.Address1;
        this.Address2 = person.Address2;
        this.Email = person.Email;
        this.ExternalUniqueId = person.ExternalUniqueId;
        this.FirstName = person.First;
        this.LastName = person.Last;
        this.PersonId = new Guid(person.PersonId);
        this.Phone = person.Phone;
        this.UserName = person.Username;
    }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string BeginDate { get; set; }
    public string Email { get; set; }
    public string ExternalUniqueId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid PersonId { get; set; }
    public string Phone { get; set; }
    public Guid PositionId { get; set; }
    public string UserName { get; set; }
}

internal class Token
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
}