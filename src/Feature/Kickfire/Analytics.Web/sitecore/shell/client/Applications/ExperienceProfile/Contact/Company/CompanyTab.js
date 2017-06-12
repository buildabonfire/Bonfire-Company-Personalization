define(
  ["sitecore",
    "/-/speak/v1/experienceprofile/DataProviderHelper.js",
    "/-/speak/v1/experienceprofile/CintelUtl.js"
  ],
  function (sc, providerHelper, cintelUtil, ExternalDataApiVersion) {
      var cidParam = "cid";

      var contactId = cintelUtil.getQueryParam(cidParam);
      var tableName = "";
      var baseUrl = "/sitecore/api/ao/v1/contacts/" + contactId + "/intel/";

      var app = sc.Definitions.App.extend({

          initialized: function () {
              $('.sc-progressindicator').first().show().hide();

              providerHelper.initProvider(this.CompanyDataProvider,
                tableName,
                baseUrl + "companyinfo",
                this.ExternalDataTabMessageBar);

              providerHelper.getData(this.CompanyDataProvider,
                $.proxy(function (jsonData) {
                    var dataSetProperty = "Data";
                    if (jsonData.data.dataSet != null && jsonData.data.dataSet.companyinfo.length > 0) {

                        var dataSet = jsonData.data.dataSet.companyinfo[0];
                        this.CompanyDataProvider.set(dataSetProperty, jsonData);
                        this.CompanyIdValue.set("text", dataSet.Name);

                        var webUrl = '';

                        if (dataSet.Website) {
                            webUrl = dataSet.Website;
                        }

                        this.WebsiteValue.set('navigateUrl', 'http://' + webUrl);
                        this.WebsiteValue.set('text', webUrl);

                        this.StreetValue.set("text", dataSet.Street);
                        this.CityValue.set("text", dataSet.City);
                        this.RegionValue.set("text", dataSet.Region);
                        this.PostalValue.set("text", dataSet.Postal);
                        this.CountryValue.set("text", dataSet.Country);
                        this.PhoneValue.set("text", dataSet.Phone);
                        this.EmployeesValue.set("text", dataSet.Employees);
                        this.RevenueValue.set("text", dataSet.Revenue);
                        this.CategoryValue.set("text", dataSet.Category);
                        this.SicCodeValue.set("text", dataSet.SicCode);
                        this.FacebookValue.set("text", dataSet.Facebook);
                        this.TwitterValue.set("text", dataSet.Twitter);
                        this.LinkedInValue.set("text", dataSet.LinkedIn);

                        
                        var url = '';

                        if (dataSet.LinkedInId) {
                            url = this.LinkedInIdValue.get('navigateUrl') + dataSet.LinkedInId;
                        } else {
                            this.LinkedInIdValue.set('text', '');
                        }

                        this.LinkedInIdValue.set('navigateUrl', url);
                        
                        this.IsISPValue.set("text", dataSet.IsIsp);
                        this.ConfidenceValue.set("text", dataSet.Confidence);

                    } else {
                        console.log("KireFire No data");
                        this.CompanyDetailsBorder.set("isVisible", false);
                        this.ExternalDataTabMessageBar.addMessage("notification", this.NoCompanyData.get("text"));
                    }
                }, this));

              this.setupCompanyInfo(baseUrl + "companyinfo?type=history");
          },

          setupCompanyInfo: function (intelBaseUrl) {

              providerHelper.initProvider(this.CompanyInfoProvider,
                  "companyinfo",
                  intelBaseUrl,
                  this.ExternalDataTabMessageBar);

              providerHelper.setupDataRepeater(this.CompanyInfoProvider, this.CompanyInfoRepeater);

              this.CompanyInfoRepeater.on("subAppLoaded", function (args) {
                  var data = args.data,
                      subapp = args.app;

                  subapp.InfoExpander.set("header", data.Name);
                  //subapp.InfoExpander.set("isOpen", "false");

                  cintelUtil.setText(subapp.CompanyIdValue, data.Name, true);

                  var webUrl = '';
                  if (data.Website) {
                      webUrl = data.Website;
                  }

                  subapp.WebsiteValue.set("navigateUrl", 'http://' + webUrl);
                  subapp.WebsiteValue.set("text", data.Website);

                  cintelUtil.setText(subapp.StreetValue, data.Street, true);
                  cintelUtil.setText(subapp.CityValue, data.City, true);
                  cintelUtil.setText(subapp.RegionValue, data.Region, true);
                  cintelUtil.setText(subapp.PostalValue, data.Postal, true);
                  cintelUtil.setText(subapp.CountryValue, data.Country, true);
                  cintelUtil.setText(subapp.PhoneValue, data.Phone, true);
                  cintelUtil.setText(subapp.EmployeesValue, data.Employees, true);
                  cintelUtil.setText(subapp.RevenueValue, data.Revenue, true);
                  cintelUtil.setText(subapp.CategoryValue, data.Category, true);
                  cintelUtil.setText(subapp.SicCodeValue, data.SicCode, true);
                  cintelUtil.setText(subapp.FacebookValue, data.Facebook, true);
                  cintelUtil.setText(subapp.TwitterValue, data.Twitter, true);
                  cintelUtil.setText(subapp.LinkedInValue, data.LinkedIn, true);


                  var url = '';

                  //if (data.LinkedInId) {
                  //    url = cintelUtil.setText(subapp.LinkedInIdValue.get('navigateUrl') + data.LinkedInId;
                  //} else {
                  //    cintelUtil.setText(subapp.LinkedInIdValue, "", true);
                  //}

                  //subapp.LinkedInIdValue.set("navigateUrl", url);
                  

                  cintelUtil.setText(subapp.IsISPValue, data.IsIsp, true);
                  cintelUtil.setText(subapp.ConfidenceValue, data.Confidence, true);



              }, this);

              providerHelper.getListData(this.CompanyInfoProvider);
          }
      });
      return app;
  });