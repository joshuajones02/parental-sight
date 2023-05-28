# parental-sight
An open source .NET Windows Service to provide parental oversight

## Getting Started
Assuming you have set all of the appropriate application settings and secrets...

To debug the Windows service locally

1. Build it
2. Publish it
3. Create Windows Service
  1. Open VS 2022 Terminal as an Administrator
  2. Enter ````sc create "ps-winsvc" binPath="C:\_github\parental-sight\src\ParentalSight.WindowsService\pub\Debug\ParentalSight.WindowsService.exe"````
  3. Enter ````sc start "ps-winsvc"````
  4. Attach VS 2022 to service running
  5. Open the 'Services' from 'Administrative Tools', find the service and in properties select "Allow to interact with Desktop"