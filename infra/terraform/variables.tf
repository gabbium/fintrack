variable "image_version" {
  description = "Docker image version"
  type        = string
}

variable "authentication_oidc_jwt_authority" {
  description = "OIDC authority URL"
  type        = string
}

variable "ledger_db_connection_string" {
  description = "Ledger Database connection string (secret)"
  type        = string
  sensitive   = true
}
