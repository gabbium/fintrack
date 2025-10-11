variable "cae_id" {
  description = "ID of the shared Container App Environment"
  type        = string
}

variable "image_version" {
  description = "Docker image version"
  type        = string
}

variable "authentication_oidc_jwt_authority" {
  description = "OIDC authority URL for authentication"
  type        = string
}

variable "planning_db_connection_string" {
  description = "Planning Database connection string (secret)"
  type        = string
  sensitive   = true
}
