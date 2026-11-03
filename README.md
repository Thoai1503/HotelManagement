# HotelManagement

## Database connection

The database connection string is loaded from `Hotel.Web/.env` for local
development. This file is ignored by Git. Copy `Hotel.Web/.env.example` to
`Hotel.Web/.env` and replace the placeholder connection string with your own.

For deployment, set the `ConnectionStrings__DefaultConnection` environment
variable in the hosting environment. Do not commit credentials.
