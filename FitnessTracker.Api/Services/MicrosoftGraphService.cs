using AutoMapper;
using FitnessTracker.Api.Models;
using FitnessTracker.Api.Models.Settings;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace FitnessTracker.Api.Services
{
    public class MicrosoftGraphService : IMicrosoftGraphService
    {
        private readonly string _url;
        private readonly IMapper _mapper;
        private readonly string _domain = "@davidgodi84gmail.onmicrosoft.com";

        public MicrosoftGraphService(IConfiguration config, IMapper mapper)
        {
            var microsoftGraphApi = config.GetSection(nameof(MicrosoftGraphApi))
                .Get(typeof(MicrosoftGraphApi)) as MicrosoftGraphApi;

            if(microsoftGraphApi == null && string.IsNullOrEmpty(microsoftGraphApi.BaseUrl))
            {
                throw new ArgumentNullException(nameof(microsoftGraphApi.BaseUrl), "");
            }

            _url = microsoftGraphApi.BaseUrl;
            _mapper = mapper;
        }

        public async Task<GraphServiceClient> GetGraphServiceClient()
        {
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            string accessToken = await azureServiceTokenProvider
                .GetAccessTokenAsync(_url);

            return new GraphServiceClient(
                new DelegateAuthenticationProvider((requestMessage) =>
                {
                    requestMessage
                    .Headers
                    .Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                    return Task.CompletedTask;
                }));
        }

        public async Task<UserAD> GetLoggedInUser()
        {
            var graphServiceClient = await GetGraphServiceClient();

            User user = await graphServiceClient.Me
                    .Request()
                    .GetAsync();

            return _mapper.Map<User, UserAD>(user);
        }

        public async Task<UserAD> GetUserById(string id)
        {
            var graphServiceClient = await GetGraphServiceClient();

            User user = await graphServiceClient.Users[id]
                    .Request()
                    .GetAsync();

            return _mapper.Map<User, UserAD>(user);
        }

        public async Task<UserAD> Search(string urlEncodedKey)
        { 
            var graphServiceClient = await GetGraphServiceClient();

            urlEncodedKey = urlEncodedKey.Replace("\n", "");

            var results = await graphServiceClient.Users
                    .Request()
                    .Filter($"userPrincipalName eq '{urlEncodedKey}{_domain}' or displayName eq '{urlEncodedKey}'")
                    .GetAsync();

            var user = results.Count > 0 ? results.First() : null;

            return _mapper.Map<User, UserAD>(user);
        }

        public async Task<UserAD> PartialSearch(string key)
        {
            var graphServiceClient = await GetGraphServiceClient();

            var results = await graphServiceClient.Users
                    .Request()
                    .Filter($"userPrincipalName eq '{key}' or displayName eq '{key}' or mail eq {key}")
                    .GetAsync();

            var user = results.Count > 0 ? results.First() : null;

            return _mapper.Map<User, UserAD>(user);
        }

        public async Task<UserAD> Create(UserAD user)
        {
            var micrographUser = _mapper.Map<UserAD, User>(user);
            micrographUser.UserPrincipalName = $"{user.GivenName.First()}{user.Surname}{_domain}";
            micrographUser.DisplayName = $"{user.GivenName} {user.Surname}";
            micrographUser.MailNickname = $"{user.GivenName.First()}{user.Surname}";
            micrographUser.AccountEnabled = true;
            micrographUser.PasswordProfile = new PasswordProfile
            {
                ForceChangePasswordNextSignIn = true,
                Password = user.Password
            };

            var graphServiceClient = await GetGraphServiceClient();

            var results = await graphServiceClient.Users
                .Request()
                .AddAsync(micrographUser);

            return _mapper.Map<User, UserAD>(results);
        }

        public async Task<UserAD> Update(UserAD user)
        {
            var micrographUser = _mapper.Map<UserAD, User>(user);
            micrographUser.AccountEnabled = true;
            micrographUser.PasswordProfile = new PasswordProfile
            {
                ForceChangePasswordNextSignIn = true,
                Password = user.Password
            };

            var graphServiceClient = await GetGraphServiceClient();

            var results = await graphServiceClient.Me
                .Request()
                .UpdateAsync(micrographUser);

            return _mapper.Map<User, UserAD>(results);
        }

        public async Task<bool> UpdatePassword(string password)
        {
            var graphServiceClient = await GetGraphServiceClient();

            var results = await graphServiceClient.Me
               .Request()
               .UpdateAsync(new User
               {
                   PasswordProfile = new PasswordProfile
                   {
                       ForceChangePasswordNextSignIn = false,
                       Password = password
                   }
               });

            return results != null;
        }

        public async Task<bool> Delete(string id)
        {
            var graphServiceClient = await GetGraphServiceClient();

            var user = await GetUserById(id);
            if(user == null)
            {
                return false;
            }

            await graphServiceClient.Users[id]
                .Request()
                .DeleteAsync();

            return true;
        }
    }
}
