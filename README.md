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
2. Open the file `\App_Config\Modules\Kickfire\Bonfire.Feature.KickfireService.config`
3. Update the `<setting name= "Bonfire.Kickfire.ApiKey" value= "xxxxxxxxxxxxxx" />` with your API key you created on the KickFire website. 

4. Publish site
5. Deployment marketing definitions
