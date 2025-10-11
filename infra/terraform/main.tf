data "azurerm_container_app_environment" "cae" {
  name                = "cae-shared"
  resource_group_name = "rg-shared"
}

module "ledger" {
  source = "./modules/ledger"

  cae_id                            = data.azurerm_container_app_environment.cae.id
  image_version                     = var.image_version
  authentication_oidc_jwt_authority = var.authentication_oidc_jwt_authority
  ledger_db_connection_string       = var.ledger_db_connection_string
}

module "planning" {
  source = "./modules/planning"

  cae_id                            = data.azurerm_container_app_environment.cae.id
  image_version                     = var.image_version
  authentication_oidc_jwt_authority = var.authentication_oidc_jwt_authority
  planning_db_connection_string     = var.planning_db_connection_string
}
