
using System;
using Pulumi;
using Pulumi.Azure.Core;
using Pulumi.Azure.Monitoring.Outputs;
using Pulumi.Azure.Storage;

class MyStack : Stack
{

    public MyStack()
    {
        // Create an Azure Resource Group
        var resourceGroup = new ResourceGroup("resourceGroup");

        // Create an Azure Storage Account
        var storageAccount = new Account("storage", new AccountArgs
        {
            ResourceGroupName = resourceGroup.Name,
            AccountReplicationType = "LRS",
            AccountTier = "standard",
            Tags =
            {
                {
                    "Environment", "Dev"
                }
            }
        });

        // Export the connection string for the storage account
        this.ConnectionString = storageAccount.PrimaryBlobConnectionString;
    }
    [Output]
    public Output<string> ConnectionString { get; set; }
}