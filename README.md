# Company Based Lookup and Personalization

This service will perform a lookup on the visitor's IP and try to identify the company that the IP is associated with. Using this information Sitecore can be personalized based on the company's name or on the industry that the company belongs to (based on the company's Sic Code).

Currently the service supports:
 * [KickFire](http://kickfire.io/) - Includes SicCode info
 * [Extreme Ip](http://extreme-ip-lookup.com/)

My next integration will be with MaxMind for company info.

## Installation

A quick install can be seen by watching [this video](https://vimeo.com/222762570).

### Prerequisites
This requires Sitecore 8.2 update 0 and above. This code uses Sitecore's Microsft dependancy injection logic.

To use the Kickfire service with this installation, you need a API key. Visit the site [Kickfire](https://www.kickfire.com/services/api) API site and create a new account. Then create a new Company endpoint (bottom of Api tab in `Create Another Access Point` section.) You can use the Extreme IP service which is free, but Industry personalization will not be possible.

### Installing

1. Install the latest update package from he package folder, in the root of the repo
2. Open the file `App_Config\Include\z_Bonfire\Bonfire.Feature.KickfireService.config`
3. Update the `<setting name= "Bonfire.Kickfire.ApiKey" value= "xxxxxxxxxxxxxx" />` with your API key you created on the KickFire website. 

### Personalizing on Industry
It is required to process the company's Sic Code to determine the industry the company belongs to. You must use KickFire service as it returns the compnany's Sic Code. Extreme IP service does not.

1. Download the SQL script for Sic Codes. https://github.com/buildabonfire/Bonfire.Kickfire.Analytics/tree/master/src/Feature/SicCodeService/code/sql
2. Create a SQL database and run the script. This will create a new table called "SicCodes". This has all the SicCodes and the industries that are associated with them.
3. Edit your connection strings file. Add a new connection string called SicCode that points to the database created above. The connection string name must be SicCode.
4. Open the file App_Config\Include\z_Bonfire\Kickfire.Settings.config. Update the setting `<setting name= "Bonfire.Kickfire.ProcessSicCode" value= "false" />` setting it to true. This will query the SQL databse when a SicCode was found in the API response.

### Switching from Kickfire to Extreme Ip
By default Kickfire service is enables, which costs money. You can switch to Extreme IP that give similar company detail, but no Industry personalization.

1. Disable the config file `\App_Config\Include\z_Bonfire\Bonfire.Feature.KickfireService.config`
2. Enable the config file `\App_Config\Include\z_Bonfire\Bonfire.Feature.ExtremeIpService.config.disable`


## Upgrade warning
To allow the contact search to search for company names as well as names and emails address, I had to override the provider `Sitecore.Cintel.ContactSearchProvider, Sitecore.Cintel`. I have to keep this up to date with the versions of Sitecore

## Speed warning if enabled
The Sitecore support patch (Sitecore.Support.396075.config) is included, but **disabled** with the package. This will allow Sitecore to wait for a specified period of time for the GeoIp to be resolved. This is important when trying to determine if the IP is a USA ip or not.

