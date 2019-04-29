.\bin\Debug\KickfireCore.Model.Builder.exe
Copy-Item ".\CompanyDataModel, 1.0.json" "C:\inetpub\wwwroot\ML.xconnect\App_data\Models\"
Copy-Item ".\CompanyDataModel, 1.0.json" "C:\inetpub\wwwroot\ML.xconnect\App_data\jobs\continuous\IndexWorker\App_data\Models\"

net stop kickfire.xconnect-MarketingAutomationService
net stop kickfire.xconnect-IndexWorker

Copy-Item ".\bin\debug\Bonfire.Feature.KickfireCore.*"  "C:\inetpub\wwwroot\ML.xconnect\bin\"
Copy-Item ".\bin\debug\Bonfire.Feature.KickfireCore.*"  "C:\inetpub\wwwroot\ML.xconnect\App_data\jobs\continuous\IndexWorker\"
Copy-Item ".\bin\debug\Bonfire.Feature.KickfireCore.*"  "C:\inetpub\wwwroot\ML.xconnect\App_data\jobs\continuous\AutomationEngine\"

net start kickfire.xconnect-MarketingAutomationService
net start kickfire.xconnect-IndexWorker