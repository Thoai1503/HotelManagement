# HotelManagement

## Database connection

The database connection string is loaded from `Hotel.Web/.env` for local
development. This file is ignored by Git. Copy `Hotel.Web/.env.example` to
`Hotel.Web/.env` and replace the placeholder connection string with your own.

For deployment, set the `ConnectionStrings__DefaultConnection` environment
variable in the hosting environment. Do not commit credentials.

## Tenant page JavaScript

Tenant page interactions are written in TypeScript under
`Hotel.Web/wwwroot/js/Areas/Tenant/Controllers`, organized by controller and
action. Register each action in `Hotel.Web/wwwroot/js/App.ts`, mark its view
with matching `page-controller` and `page-action` attributes, and run
`npm ci` followed by `npm run build` from `Hotel.Web`. The build type-checks
the sources and generates development and minified bundles under
`Hotel.Web/wwwroot/js`. See the [JavaScript guide](Hotel.Web/JAVASCRIPT_GUIDE.md)
for the step-by-step workflow and examples.
