resource "azurerm_container_app" "planning_api" {
  name                         = "ca-ftrk-planning-api"
  resource_group_name          = "rg-shared"
  container_app_environment_id = var.cae_id
  revision_mode                = "Single"

  secret {
    name  = "planning-db-connection-string"
    value = var.planning_db_connection_string
  }

  template {
    container {
      name   = "planning-api"
      image  = "docker.io/gabbium/fintrack-planning-api:${var.image_version}"
      cpu    = 0.5
      memory = "1Gi"

      env {
        name  = "Authentication__OidcJwt__Authority"
        value = var.authentication_oidc_jwt_authority
      }

      env {
        name        = "ConnectionStrings__PlanningDb"
        secret_name = "planning-db-connection-string"
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

resource "azurerm_container_app_job" "planning_migrationservice" {
  name                         = "ca-ftrk-planning-migrsvc"
  resource_group_name          = "rg-shared"
  location                     = "brazilsouth"
  container_app_environment_id = var.cae_id
  replica_timeout_in_seconds   = 1800
  replica_retry_limit          = 0

  secret {
    name  = "planning-db-connection-string"
    value = var.planning_db_connection_string
  }

  manual_trigger_config {
    parallelism = 1
  }

  template {
    container {
      name   = "planning-migrationservice"
      image  = "docker.io/gabbium/fintrack-planning-migrationservice:${var.image_version}"
      cpu    = 0.25
      memory = "0.5Gi"

      env {
        name        = "ConnectionStrings__PlanningDb"
        secret_name = "planning-db-connection-string"
      }
    }
  }
}
