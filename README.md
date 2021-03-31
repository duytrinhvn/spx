## Current State
- Buckets page (Admin): Create/Read/Update/Delete buckets
- Teams page (Admin): Create/Read/Update/Delete teams
- Invest page (Investor): See all the available buckets that match their favorite sport teams
- Login/Logout: 
    - Authentication: User can register for an Investor account using the User Interface. The Admin account have to be created by adding codes to Data/DbInitializer.cs file.
    - Authorization: Filters the content users can see based on their roles
        - The Buckets page and Teams page can only be available when user logins as an Admin
        - The Invest page can only be available when user logins as an Investors
- Default Roles and Users seeded: 
    - There are two roles, "Admin" and "Investor".
    - Default User accounts for testing:
        - the.admin@spx.ca - Password!1
        - the.investor@spx.ca - Password!1

## Next State
- Investing activities: Investors will be able to start trading shares after they accept the available buckets in the Invest page.
