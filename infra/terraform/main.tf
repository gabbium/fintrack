provider "azurerm" {
  features {}
}

data "azurerm_container_app_environment" "cae" {
  name                = "cae-shared"
  resource_group_name = "rg-shared"
}

resource "azurerm_container_app" "ledger_api" {
  name                         = "ca-ftrk-ledger-api"
  resource_group_name          = "rg-shared"
  container_app_environment_id = data.azurerm_container_app_environment.cae.id
  revision_mode                = "Single"

  secret {
    name  = "ledger-db-connection-string"
    value = var.ledger_db_connection_string
  }

  template {
    container {
      name   = "ledger-api"
      image  = "docker.io/gabbium/fintrack-ledger-api:${var.image_version}"
      cpu    = 0.5
      memory = "1Gi"

      env {
        name  = "Authentication__OidcJwt__Authority"
        value = var.authentication_oidc_jwt_authority
      }

      env {
        name        = "ConnectionStrings__LedgerDb"
        secret_name = "ledger-db-connection-string"
      }
    }
  }

  ingress {
    external_enabled = true
    target_port      = 8080

    traffic_weight {
      latest_revision = true
      percentage      = 100
    }
  }
}

resource "azurerm_container_app_job" "ledger_migrationservice" {
  name                         = "ca-ftrk-ledger-migrationservice"
  resource_group_name          = "rg-shared"
  location                     = "brazilsouth"
  container_app_environment_id = data.azurerm_container_app_environment.cae.id
  replica_timeout_in_seconds   = 1800
  replica_retry_limit          = 0

  secret {
    name  = "ledger-db-connection-string"
    value = var.ledger_db_connection_string
  }

  manual_trigger_config {
    parallelism = 1
  }

  template {
    container {
      name   = "ledger-migrationservice"
      image  = "docker.io/gabbium/fintrack-ledger-migrationservice:${var.image_version}"
      cpu    = 0.25
      memory = "0.5Gi"

      env {
        name        = "ConnectionStrings__LedgerDb"
        secret_name = "ledger-db-connection-string"
      }
    }
  }
}
