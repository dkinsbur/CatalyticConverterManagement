using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalyticConverterManagement
{

    public class CompanyDb : EntityBase
    {
        public string Name { get; set; }
    }

    class CategoryDb : EntityBase
    {
        public string Name { get; set; }
    }

    class ConverterDb : EntityBase
    {
        public int CompanyId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
    class AnalysisDb : EntityBase
    {
        public int AnalysisNum { get; set; }
        public UInt32 Platinum { get; set; } // parts per milion - grams per kilo

        public UInt32 Palladium { get; set; }

        public UInt32 Rhodium { get; set; }

        public Double Weight { get; set; } // . weight - kilo

        public UInt32 NumOfSamples { get; set; }

        public int ConverterId { get; set; }
    }

    class AllDb
    {
        public List<CompanyDb> Companies;
        public List<CategoryDb> Categories;
        public List<ConverterDb> Converters;
        public List<AnalysisDb> Analysis;
    }



    class Database
    {
        private IDatabase _db;



        public Database(IDatabase db)
        {
            _db = db;
        }

        public void AddConverter(Converter item)
        {

        }



        public List<Purchase> GetPurchases()
        {
            return null;
        }

    }

    interface IDatabase
    {
        void AddCompany(CompanyDb item);
        void AddConverter(ConverterDb item);
        void AddAnalysis(AnalysisDb item);

        void UpdateCompany(CompanyDb item);
        void UpdateConverter(ConverterDb item);
        void UpdateAnalysis(AnalysisDb item);

        void RemoveCompany(CompanyDb item);
        void RemoveConverter(ConverterDb item);
        void RemoveAnalysis(AnalysisDb item);

        List<CompanyDb> GetCompanies();
        List<ConverterDb> GetConverters();
        List<AnalysisDb> GetAnalysis();
    }

    public abstract class EntityBase

    {

        public Int64 Id { get; protected set; }

    }

    public interface IRepository<T> where T : EntityBase

    {

        List<T> GetAll();

        T GetById(Int64 id);

        void Create(T entity);

        void Delete(T entity);

        void Update(T entity);

    }

    class CacheRepository<T> : IRepository<T> where T : EntityBase
    {
        private IRepository<T> _repo;
        List<T> _cache;

        public CacheRepository(IRepository<T> baseRepo)
        {
            _repo = baseRepo;
            _cache = _repo.GetAll();
        }

        public void Create(T entity)
        {
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }

    class CompanyRepository : IRepository<CompanyDb>
    {
        private AllDb _db;

        public CompanyRepository(AllDb db)
        {
            _db = db;
        }
        public void Create(CompanyDb entity)
        {
        }

        public void Delete(CompanyDb entity)
        {
            throw new NotImplementedException();
        }

        public List<CompanyDb> GetAll()
        {
            throw new NotImplementedException();
        }

        public CompanyDb GetById(long id)
        {
            throw new NotImplementedException();
        }

        public void Update(CompanyDb entity)
        {
            throw new NotImplementedException();
        }
    }


}
