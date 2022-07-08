﻿using Azure.Storage.Blobs;

using Microsoft.Extensions.Configuration;
using Weather.Infrastructure.Extensions;

namespace Weather.Infrastructure
{
    public class BlobContainerClientFactory : IBlobContainerClientFactory
    {
        private readonly IConfiguration _configuration;

        public BlobContainerClientFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public BlobContainerClient CreateClient()
        {
            var blobServiceClient = new BlobServiceClient(_configuration.GetAzureBlobServiceConnectionString());
            var containerClient = blobServiceClient.GetBlobContainerClient(_configuration.GetAzureBlobServiceContainerName());
            return containerClient;
        }
    }
}
