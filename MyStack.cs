
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
        var resourceGroup = new ResourceGroup("resourceGroup", options: new CustomResourceOptions
        {
            Protect = false
        });

        // Create an Azure Storage Account
        var storageAccount = new Account("storage", new AccountArgs
        {
            
            ResourceGroupName = resourceGroup.Name,
            AccountReplicationType = "LRS",
            AccountTier = "standard",
            Tags =
            {
                { "Environment", "Dev" }
            }
        }, new CustomResourceOptions
        {
            DeleteBeforeReplace = true,
            Aliases = {new Alias { Name = "my-storage-account"} },
            CustomTimeouts = new CustomTimeouts { Create = TimeSpan.FromMinutes(30) },
            DependsOn = { resourceGroup },
            Parent = resourceGroup
        });

        // Export the connection string for the storage account
        this.ConnectionString = storageAccount.PrimaryBlobConnectionString;
        this.StorageUrn = storageAccount.Urn;
    }
    [Output]
    public Output<string> ConnectionString { get; set; }
    [Output] public Output<string> StorageUrn { get; set; }
}