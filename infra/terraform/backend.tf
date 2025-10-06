terraform {
  backend "azurerm" {
    resource_group_name  = "rg-shared"
    storage_account_name = "stgabbiumtfstate"
    container_name       = "tfstate"
    use_azuread_auth     = true
  }
}
