using Fintranet.Test.Application.Abstractions.Repositories;
using Fintranet.Test.Domain.Entities;
using Fintranet.Test.Domain.RemoteDataModel;
using Fintranet.Test.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XAct;

namespace Fintranet.Test.Infrastructure.RemoteData
{
    public static class RemoteDataInitializer
    {
        public static async Task AddRemoteMongoAtlasData(IRemoteDataRepository remoteDataRepository, IConfiguration configuration)
        {
            var database = getOutsideDataStoreDatabaseFromConfiguration(configuration);
            var dataSource = getOutsideDataStoreDataSourceFromConfiguration(configuration);
            var url = getOutsideDataStoreUrlFromConfiguration(configuration);
            var apiKey = getOutsideDataStoreApiKeyFromConfiguration(configuration);

            // Add Inirial Congestion Rules
            RemoteDataApiBody apiBody = new RemoteDataApiBody()
            {
                Collection = "CongestionRules",
                Database = database,
                DataSource = dataSource
            };
            var congestionRules = await CallRemoteDataApi<CongestionRule[]>(apiBody, url, apiKey);
            if (!congestionRules.IsNullOrEmpty())
            {
                remoteDataRepository.RemoveAllCongestionRules();
                remoteDataRepository.AddRangeCongestionRules(congestionRules);
            }

            // Add Inirial Congestion Fees
            apiBody.Collection = "CongestionFees";
            var congestionFees = await CallRemoteDataApi<CongestionFee[]>(apiBody, url, apiKey);
            if (!congestionFees.IsNullOrEmpty())
            {
                remoteDataRepository.RemoveAllCongestionFees();
                remoteDataRepository.AddRangeCongestionFees(congestionFees);
            }

            // Add Inirial Vehicles
            apiBody.Collection = "Vehicles";
            var vehicles = await CallRemoteDataApi<Vehicle[]>(apiBody, url, apiKey);
            if (!vehicles.IsNullOrEmpty())
            {
                remoteDataRepository.RemoveAllVehicles();
                remoteDataRepository.AddRangeVehicles(vehicles);
            }

            // Add Inirial Vehicles
            apiBody.Collection = "TollFreeVehicles";
            var tollFreeVehicles = await CallRemoteDataApi<TollFreeVehicle[]>(apiBody, url, apiKey);
            if (!tollFreeVehicles.IsNullOrEmpty())
            {
                remoteDataRepository.RemoveAllTollFreeVehicles();
                remoteDataRepository.AddRangeTollFreeVehicles(tollFreeVehicles);
            }
        }

        private static async Task<T> CallRemoteDataApi<T>(RemoteDataApiBody remoteDataApiBody, string url, string apiKey)
        {
            var client = new RestClient(url);
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Access-Control-Request-Headers", "*");
            request.AddHeader("api-key", apiKey);
            var body = JsonSerializer.Serialize(remoteDataApiBody);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.PostAsync(request);
            var res = JsonSerializer.Deserialize<DocumentsRemoteData>(response.Content);
            return JsonSerializer.Deserialize<T>(res.Result.ToString());
        }

        private static string getOutsideDataStoreDatabaseFromConfiguration(IConfiguration configuration)
        {
            var OutsideDataStoreDatabase = configuration.GetSection("OutsideDataStore:Database").Get<string>();
            if (string.IsNullOrWhiteSpace(OutsideDataStoreDatabase))
            {
                throw new Exception("OutsideDataStore:Database is not set.");
            }
            return OutsideDataStoreDatabase;
        }

        private static string getOutsideDataStoreDataSourceFromConfiguration(IConfiguration configuration)
        {
            var OutsideDataStoreDataSource = configuration.GetSection("OutsideDataStore:DataSource").Get<string>();
            if (string.IsNullOrWhiteSpace(OutsideDataStoreDataSource))
            {
                throw new Exception("OutsideDataStore:DataSource is not set.");
            }
            return OutsideDataStoreDataSource;
        }

        private static string getOutsideDataStoreUrlFromConfiguration(IConfiguration configuration)
        {
            var OutsideDataStoreUrl = configuration.GetSection("OutsideDataStore:Url").Get<string>();
            if (string.IsNullOrWhiteSpace(OutsideDataStoreUrl))
            {
                throw new Exception("OutsideDataStore:Url is not set.");
            }
            return OutsideDataStoreUrl;
        }

        private static string getOutsideDataStoreApiKeyFromConfiguration(IConfiguration configuration)
        {
            var OutsideDataStoreApiKey = configuration.GetSection("OutsideDataStore:ApiKey").Get<string>();
            if (string.IsNullOrWhiteSpace(OutsideDataStoreApiKey))
            {
                throw new Exception("OutsideDataStore:ApiKey is not set.");
            }
            return OutsideDataStoreApiKey;
        }
    }
}
