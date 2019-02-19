using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;

namespace CatalyticConverterManagement
{

    class JsonDb
    {
        public List<CompanyDb> Companies;
        public List<CategoryDb> Categories;
        public List<ConverterDb> Converters;
        public List<AnalysisDb> Analysis;
    }
    class JsonDatabase : IDatabase
    {
        JsonDb _db;
        string _dbPath;

        public JsonDatabase(string DbPath)
        {
            _dbPath = DbPath;
            if (!File.Exists(DbPath))
            {
                _db = new JsonDb();
                SaveDb();
            }
            else
            {
                LoadDb();
            }
        }

        private void LoadDb()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var json = File.ReadAllText(_dbPath);
            _db = serializer.Deserialize(json, typeof(JsonDb)) as JsonDb;
        }

        private void SaveDb()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            File.WriteAllText(_dbPath, serializer.Serialize(_db));
        }

        public void AddAnalysis(AnalysisDb item)
        {
            //_db.Analysis.Add(item);
        }

        public void AddCategory(CategoryDb item)
        {
            throw new NotImplementedException();
        }

        public void AddCompany(CompanyDb item)
        {
            throw new NotImplementedException();
        }

        public void AddConverter(ConverterDb item)
        {
            throw new NotImplementedException();
        }

        public List<AnalysisDb> GetAnalysis()
        {
            throw new NotImplementedException();
        }

        public List<CategoryDb> GetCategories()
        {
            throw new NotImplementedException();
        }

        public List<CompanyDb> GetCompanies()
        {
            throw new NotImplementedException();
        }

        public List<ConverterDb> GetConverters()
        {
            throw new NotImplementedException();
        }

        public void RemoveAnalysis(AnalysisDb item)
        {
            throw new NotImplementedException();
        }

        public void RemoveCategory(CategoryDb item)
        {
            throw new NotImplementedException();
        }

        public void RemoveCompany(CompanyDb item)
        {
            throw new NotImplementedException();
        }

        public void RemoveConverter(ConverterDb item)
        {
            throw new NotImplementedException();
        }

        public void UpdateAnalysis(AnalysisDb item)
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(CategoryDb item)
        {
            throw new NotImplementedException();
        }

        public void UpdateCompany(CompanyDb item)
        {
            throw new NotImplementedException();
        }

        public void UpdateConverter(ConverterDb item)
        {
            throw new NotImplementedException();
        }
    }
}
