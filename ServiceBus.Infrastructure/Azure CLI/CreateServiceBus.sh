# Create resource group
az group create --name az204servicebusrg --location ukwest --tags environment=development

# Create service bus namespace
az servicebus namespace create --resource-group az204servicebusrg --name az204servicebusns --location ukwest --tags environment=development --sku basic

# Create chat message queue
az servicebus queue create --queue-name chatmessagequeue --namespace-name az204servicebusns --resource-group az204servicebusrg

# Create queue authorization rule with sender permission only
az servicebus queue authorization-rule create --name QueueSender --namespace-name az204servicebusns --queue-name chatmessagequeue --rights Send --resource-group az204servicebusrg

# Get connection string for authorization rule


