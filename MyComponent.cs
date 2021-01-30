using System;
using System.Collections.Generic;
using Pulumi;
using Pulumi.Azure.Core;
using Pulumi.Azure.Storage;

namespace AzureQuickstart
{
    class MyComponent : Pulumi.ComponentResource
    {
        public MyComponent(string name, ComponentResourceOptions opts)
            : base("pkg:index:MyComponent", name, opts)
        {
            // initialization logic.
            // Create an Azure Resource Group
            var resourceGroup = new ResourceGroup("resourceGroup", options: new CustomResourceOptions
            {
                Protect = false,
                Parent = this
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
                Aliases = { new Alias { Name = "my-storage-account" } },
                CustomTimeouts = new CustomTimeouts { Create = TimeSpan.FromMinutes(30) },
                DependsOn = { resourceGroup },
                Parent = this
            });

            // signal to the UI that this resource has completed construction
            this.RegisterOutputs(new Dictionary<string, object>
            {
                { "resourceGroupName", resourceGroup.Name },
                { "storageAccountConnectionString", storageAccount.PrimaryConnectionString }
            });
        }
    }
}
