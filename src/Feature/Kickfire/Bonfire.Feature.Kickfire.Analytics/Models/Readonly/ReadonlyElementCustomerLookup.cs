using System;
using System.Data;
using Bonfire.Feature.Kickfire.Analytics.Interfaces.Entries;

namespace Bonfire.Feature.Kickfire.Analytics.Models.Readonly
{
    [Serializable]
    public class ReadonlyElementCustomerLookup : IElementCustomerLookup
    {
        private string _CID;
        private string _name;
        private string _website;
        private string _street;
        private string _city;
        private string _regionShort;
        private int _confidence;
        private string _region;
        private string _postal;
        private string _countryShort;
        private string _country;
        private string _phone;
        private string _employees;
        private string _revenue;
        private string _category;
        private string _sicCode;
        private string _latitude;
        private string _facebook;
        private string _longitude;
        private string _stockSynbol;
        private string _twitter;
        private string _linkedIn;
        private string _linkedInID;
        private int _isISP;
        private DateTime _modified;

        public ReadonlyElementCustomerLookup(IElementCustomerLookup elementCustomerLookup)
        {
            _CID = elementCustomerLookup.CID;
            _name = elementCustomerLookup.name;
            _website = elementCustomerLookup.website;
            _street = elementCustomerLookup.street;
            _city = elementCustomerLookup.city;
            _regionShort = elementCustomerLookup.regionShort;
            _confidence = elementCustomerLookup.confidence;
            _region = elementCustomerLookup.region;
            _postal = elementCustomerLookup.postal;
            _countryShort = elementCustomerLookup.countryShort;
            _country = elementCustomerLookup.country;
            _phone = elementCustomerLookup.phone;
            _employees = elementCustomerLookup.employees;
            _revenue = elementCustomerLookup.revenue;
            _category = elementCustomerLookup.category;
            _sicCode = elementCustomerLookup.sicCode;
            _latitude = elementCustomerLookup.latitude;
            _facebook = elementCustomerLookup.facebook;
            _longitude = elementCustomerLookup.longitude;
            _stockSynbol = elementCustomerLookup.stockSymbol;
            _twitter = elementCustomerLookup.twitter;
            _linkedIn = elementCustomerLookup.linkedIn;
            _linkedInID = elementCustomerLookup.linkedInID;
            _isISP = elementCustomerLookup.isISP;
        }


        public string CID
        {
            get { return this._CID; }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string name
        {
            get
            {
                return this._name;
            }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string website
        {
            get
            {
                return this._website;
            }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string street
        {
            get
            {
                return this._street;
            }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string city
        {
            get
            {
                return this._city;
            }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string regionShort
        {
            get { return this._regionShort; }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string region
        {
            get { return this._region; }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string postal
        {
            get { return this._postal; }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string countryShort
        {
            get { return this._countryShort; }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string country
        {
            get { return this._country; }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string phone
        {
            get { return this._phone; }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string employees
        {
            get { return this._employees; }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string revenue
        {
            get { return this._revenue; }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string category
        {
            get { return this._category; }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string sicCode
        {
            get { return this._sicCode; }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string latitude
        {
            get { return this._latitude; }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string longitude
        {
            get { return this._longitude; }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string stockSymbol
        {
            get { return this._stockSynbol; }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string facebook
        {
            get { return this._facebook; }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string twitter
        {
            get { return this._twitter; }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string linkedIn
        {
            get { return this._linkedIn; }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public string linkedInID
        {
            get
            {
                return this._linkedInID;
            }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public int isISP
        {
            get { return this._isISP; }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public int confidence
        {
            get { return this._confidence; }
            set
            {
                throw new ReadOnlyException();
            }
        }

        public DateTime modified
        {
            get { return this._modified; }
            set
            {
                throw new ReadOnlyException();
            }
        }



        public bool IsEmpty
        {
            get { throw new NotImplementedException(); }
        }

        public Sitecore.Analytics.Model.Framework.IModelMemberCollection Members
        {
            get { throw new NotImplementedException(); }
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Traverse(string path, Sitecore.Analytics.Model.Framework.IModelMemberVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
