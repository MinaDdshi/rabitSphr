using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using RedditMockup.Common.Dtos;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace RedditMockup.IntegrationTest;

public class AccountControllerTest : IClassFixture<WebApplicationFactory<Program>>
{

    #region [Field(s)]

    private const string _baseAddress = "/api/Account";

    private const int _defaultTimeout = 2000;

    private readonly HttpClient _client;

    #endregion

    #region [Constructor]

    public AccountControllerTest(WebApplicationFactory<Program> factory) =>
        _client = factory.WithWebHostBuilder(builder =>
                            builder.UseEnvironment("Testing"))
                                    .CreateClient();



    #endregion

    #region [Method(s)]

    private async Task AuthenticateAsync()
    {
        var loginDto = new LoginDto()
        {
            Username = "sepehr_frd",
            Password = "sfr1376",
            RememberMe = true
        };

        var client = new RestClient(_client);

        var request = new RestRequest($"{_baseAddress}/Login")
        {
            Timeout = _defaultTimeout
        };

        request.AddJsonBody(loginDto);

        await client.ExecutePostAsync(request);
    }

    #endregion

    #region [Theory Method(s)]

    [Theory]
    [MemberData(nameof(GenerateLoginData))]
    public async Task Login_ReturnExpectedResult(LoginDto loginDto, bool expected)
    {
        #region [Arrange]

        var client = new RestClient(_client);

        var request = new RestRequest($"{_baseAddress}/Login")
        {
            Timeout = _defaultTimeout
        };

        request.AddJsonBody(loginDto);

        #endregion

        #region [Act]

        var response = await client.ExecutePostAsync<CustomResponse>(request);

        #endregion

        #region [Assert]

        response.Data?.IsSuccess.Should().Be(expected);

        #endregion
    }

    [Theory]
    [MemberData(nameof(GenerateIntegrationData))]
    public async Task LoginGetAllLogout_ReturnExpectedResult(LoginDto loginDto, TestResultCode expected)
    {
        #region [Arrange]

        var client = new RestClient(_client);

        #endregion

        #region [Act]

        #region [Login]

        var loginRequest = new RestRequest($"{_baseAddress}/Login")
        {
            Timeout = _defaultTimeout
        };

        loginRequest.AddJsonBody(loginDto);

        var loginResponse = await client.ExecutePostAsync<CustomResponse>(loginRequest);

        #endregion

        #region [GetAll]

        var getAllRequest = new RestRequest("/api/User")
        {
            Timeout = _defaultTimeout
        };

        var getAllResponse = await client.ExecuteGetAsync<CustomResponse>(getAllRequest);

        #endregion

        #region [Logout]

        var logoutRequest = new RestRequest($"{_baseAddress}/Logout")
        {
            Timeout = _defaultTimeout
        };

        var logoutResponse = await client.ExecutePostAsync<CustomResponse>(logoutRequest);

        #endregion

        #endregion

        #region [Assert]

        switch (expected)
        {
            case TestResultCode.AllFailed:

                loginResponse.Data?.IsSuccess.Should().BeFalse();

                getAllResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

                logoutResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

                break;

            case TestResultCode.AllSuccessful:

                loginResponse.Data?.IsSuccess.Should().BeTrue();

                getAllResponse.StatusCode.Should().Be(HttpStatusCode.OK);

                logoutResponse.StatusCode.Should().Be(HttpStatusCode.OK);

                break;

            case TestResultCode.Unauthorized:

                loginResponse.Data?.IsSuccess.Should().BeTrue();

                getAllResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);

                logoutResponse.StatusCode.Should().Be(HttpStatusCode.OK);

                break;

            default:
                Assert.True(false);
                break;
        }

        #endregion
    }

    #endregion

    #region [Data Method(s)]

    public static IEnumerable<object[]> GenerateLoginData()
    {
        return new List<object[]>
        {
            new object[]
            {
                new LoginDto
                {
                    Username = "sepehr_frd",
                    Password = "sfr1376",
                    RememberMe = true
                },
                true
            },

            new object[]
            {
                new LoginDto
                {
                    Username = "sepehr_frd",
                    Password = "asdasdasdasd",
                    RememberMe = false
                },
                false
            },

            new object[]
            {
                new LoginDto
                {
                    Username = "sepehr_d",
                    Password = "sfr1376",
                    RememberMe = false
                },
                false
            },

            new object[]
            {
                new LoginDto
                {
                    Username = "223",
                    Password = "sd2",
                    RememberMe = false
                },
                false
            }
        };
    }

    public static IEnumerable<object[]> GenerateIntegrationData()
    {
        return new List<object[]>
        {
            new object[]
            {
                new LoginDto
                {
                    Username = "abbas_booazaar",
                    Password = "abbasabbas",
                    RememberMe = true
                },
                TestResultCode.Unauthorized
            },

            new object[]
            {
                new LoginDto
                {
                    Username = "sepehr_frd",
                    Password = "sfr1376",
                    RememberMe = true
                },
                TestResultCode.AllSuccessful
            },

            new object[]
            {
                new LoginDto
                {
                    Username = "sepehr_frd",
                    Password = "sfr1231123376",
                    RememberMe = true
                },
                TestResultCode.AllFailed
            },

            new object[]
            {
                new LoginDto
                {
                    Username = "sepeasdfahr_frd",
                    Password = "sfr1376",
                    RememberMe = true
                },
                TestResultCode.AllFailed
            }
        };
    }

    #endregion

    public enum TestResultCode
    {
        AllFailed,
        AllSuccessful,
        Unauthorized
    }
}