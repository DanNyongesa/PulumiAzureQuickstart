using System.Collections.Generic;
using Pulumi;
using Pulumi.Azure.Core;

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

            // signal to the UI that this resource has completed construction
            this.RegisterOutputs(new Dictionary<string, object>
            {
                { "resourceGroupName", resourceGroup.Name }
            });
        }
    }
}
