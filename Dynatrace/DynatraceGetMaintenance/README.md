# Get Maintanences - Activity to get maintenances of Dynatrace platform.

Dependencies:
1. Newtonsoft.Json

Remark:
1. Create an account in Dynatrace and get an environment id | https://{your-environment-id}.live.dynatrace.com.
2. Create a host in https://{your-environment-id}.live.dynatrace.com.
3. Create a token in | https://{your-environment-id}.live.dynatrace.com -> Settings -> Integration -> Dynatrace API -> Generate Token.
4. Create a host.

Mandatory fields :<br />
AuthorizationToken(string) - Use your existing user token or you can create the User Token with following steps | https://{your-environment-id}.live.dynatrace.com -> Settings -> Integration -> Dynatrace API -> Generate Token<br />
EnvironmentId(string) - Get environment id from URI | https://{your-environment-id}.live.dynatrace.com. <br />
Type(string) - Can be one of these values (If Unknown or not set, all maintenance windows are returned.)
1. Planned
2. Unknown
3. Unplanned

[See more info in official documentation][https://www.dynatrace.com/support/help/extend-dynatrace/dynatrace-api/environment-api/maintenance-windows/]